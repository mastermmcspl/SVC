Imports BusinesLayer
Imports System.Data
Imports DatabaseLayer
Partial Class Inventory_InventoryMaster
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Inventory_InventoryMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objDB As New DBHelper
    Private objclsModulePermission As New clsModulePermission
    Dim objINV As New clsInventoryMaster
    Private Shared sIMSave As String
    Dim objFasgnrl As New clsFASGeneral
    Dim objAllGnrl As New clsGeneralFunctions

    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Function SearchCustomers(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim dr As OleDb.OleDbDataReader
        Dim customers As List(Of String) = New List(Of String)
        Dim sSql As String
        Try
            sSql = "Select * from inventory_master where Inv_Description like '%" & prefixText & "%'"
            dr = objDB.SQLDataReader(sSession.AccessCode, sSql)
            While dr.Read
                customers.Add(dr("Inv_Description").ToString)
            End While
            Return customers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub LoadSearch()
        Try
            ddlSearch.Items.Add(New ListItem("Select Type", 0))
            ddlSearch.Items.Add(New ListItem("Code", 1))
            ddlSearch.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnGroup.ImageUrl = "~/Images/Group.png"
        imgbNewItem.ImageUrl = "~/Images/SubGroup.png"
        imgbtnHSNDescSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnHSTCodeSearch.ImageUrl = "~/Images/Search16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then


                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "INVM")
                imgbtnGroup.Visible = False : imgbtnSave.Visible = False : imgbtnRefresh.Visible = False : sIMSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnGroup.Visible = True
                        sIMSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnRefresh.Visible = True
                    End If
                End If
                LoadCommodity()
                REVSize.ValidationExpression = "^[0-9]\d*?$"
                REVSize.ErrorMessage = "Enter Only Numbers No Special Characters are allowed."

                lblNode.Text = "0"
                LoadTreeview()
                LoadSearch()

                RFVCode.ErrorMessage = "Enter Item Code."
                RFVDescription.ErrorMessage = "Enter Description."

                Me.txtAcode.Attributes.Add("Onblur", "return Code()")
                Me.txtSize.Attributes.Add("Onblur", "return Code()")
                Me.txtColor.Attributes.Add("Onblur", "return Code()")
                txtCreatedBy.Text = objAllGnrl.GetUserFullName(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                txtCreatedOn.Text = objFasgnrl.FormatDtForRDBMS(objAllGnrl.GetSQLDate(sSession.AccessCode), "D")
                lblNode.Text = "Item Code"
                loadcat()
                imgbtnUpdate.Visible = False
                imgbNewItem.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objINV.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadTreeview()
        Try
            tvCategory.DataSource = objINV.LoadInventoryTreeView(sSession.AccessCode, sSession.AccessCodeID)
            tvCategory.DataKeyField = "Inv_ID"
            tvCategory.DataParentField = "Inv_Parent"
            tvCategory.DataTextField = "Inv_Code"
            tvCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub tvCategory_NodeClick(ByVal sender As Object, ByVal args As PowerUp.Web.UI.WebTree.TreeNodeEventArgs) Handles tvCategory.NodeClick
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim lParent As Long
        Dim sNodeDesc As String
        Dim sPath As String = ""
        Dim sCurNodeDesc As String = ""
        Dim objInvStr As New clsInventoryMaster.Inventory
        Dim dtGST As New DataTable
        Try
            lblError.Text = ""
            txtSize.Text = "" : txtColor.Text = "" : txtAcode.Text = ""
            lblNode.Text = args.Node.DataKey
            txtCcode.Text = ""
            If sIMSave = "YES" Then
                imgbNewItem.Visible = True
            End If


            imgbtnGroup.Visible = False
            ClearHSNDetails()
            If args.Node.Depth = 0 Then
                lblCode.Text = "Group Code"

                If sIMSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False

            ElseIf args.Node.Depth = 1 Then
                lblCode.Text = "Item Code"
                If sIMSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False
            Else
                lblCode.Text = "Item Code"
                If sIMSave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                imgbtnSave.Visible = False
            End If
            dt = objINV.GetInventoryMasterDetails(sSession.AccessCode, sSession.AccessCodeID, args.Node.DataKey)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Inv_Code").ToString()) = False Then
                    txtCode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Code").ToString())
                End If
                If IsDBNull(dt.Rows(0)("Inv_Description").ToString()) = False Then
                    txtDescription.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                    sCurNodeDesc = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                End If
                If IsDBNull(dt.Rows(0)("Inv_CreatedBy").ToString()) = False Then
                    txtCreatedBy.Text = objAllGnrl.GetUserFullName(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("Inv_CreatedBy").ToString())
                    txtCreatedOn.Text = objFasgnrl.FormatDtForRDBMS(dt.Rows(0)("Inv_CreatedOn").ToString(), "D")
                End If
                If IsDBNull(dt.Rows(0)("Inv_Size")) = False Then
                    txtSize.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Size"))
                End If
                If IsDBNull(dt.Rows(0)("Inv_Color")) = False Then
                    txtColor.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Color"))
                End If
                If IsDBNull(dt.Rows(0)("Inv_Acode")) = False Then
                    txtAcode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Acode"))
                End If
                If IsDBNull(dt.Rows(0)("INV_Ccode")) = False Then
                    txtCcode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("INV_Ccode"))
                End If

                dtGST = objINV.GetGSTRatesDetails(sSession.AccessCode, sSession.AccessCodeID, args.Node.DataKey)
                If dtGST.Rows.Count > 0 Then
                    If IsDBNull(dtGST.Rows(0)("GST_ScheduleType")) = False Then
                        txtScheduleType.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_ScheduleType"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_GSTRate")) = False Then
                        txtGSTRate.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_GSTRate"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SlNo")) = False Then
                        txtSLNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SlNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_CHST")) = False Then
                        txtCHST.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_CHST"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Chapter")) = False Then
                        txtChapter.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Chapter"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Heading")) = False Then
                        txtHeading.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Heading"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SubHeading")) = False Then
                        txtSubHeading.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SubHeading"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Tarrif")) = False Then
                        txtTarrif.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Tarrif"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SubSlNo")) = False Then
                        txtSubSlNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SubSlNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_CESS")) = False Then
                        txtCESS.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_CESS"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_GoodDescription")) = False Then
                        txtGoodDescription.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_GoodDescription"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_NotificationNo")) = False Then
                        txtNotificationNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_NotificationNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_NotificationFromDate")) = False Then
                        txtNotificationDate.Text = objFasgnrl.FormatDtForRDBMS(dtGST.Rows(0)("GST_NotificationFromDate"), "D")
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_FileNo")) = False Then
                        txtFileNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_FileNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_FileFromDate")) = False Then
                        txtFileDate.Text = objFasgnrl.FormatDtForRDBMS(dtGST.Rows(0)("GST_FileFromDate"), "D")
                    End If
                End If

                lParent = dt.Rows(0)("Inv_Parent").ToString()
            End If
            i = lParent
            For i = 1 To lParent
                objInvStr = objINV.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
                If objInvStr.iInv_Parent <> 0 Or objInvStr.sInv_Description <> "" Then
                    lParent = objInvStr.iInv_Parent
                    sNodeDesc = objInvStr.sInv_Description
                    sPath = sNodeDesc & "/" & sPath
                End If
            Next
            sNodeDesc = sCurNodeDesc
            sPath = sPath & sNodeDesc
            lblPath.Text = objFasgnrl.ReplaceSafeSQL(sPath)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "tvCategory_NodeClick")
        End Try
    End Sub

    'Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
    '    Dim objInv As New clsInventoryMaster.Inventory
    '    Dim Arr() As String
    '    Try
    '        lblErrorUp.Text = "" : lblErrorDown.Text = ""
    '        If lblNode.Text = "0" Then
    '            objInv.iInv_Unit = 0
    '            objInv.iInv_AlterUnit = 0
    '            objInv.iInv_ID = 0
    '        Else
    '            objInv.iInv_Unit = 0
    '            objInv.iInv_AlterUnit = 0
    '            objInv.iInv_ID = lblNode.Text
    '        End If
    '        objInv.iInv_Parent = lblNode.Text
    '        objInv.sInv_Code = clsTRACeGeneral.SafeSQL(txtCode.Text)
    '        objInv.sInv_Description = clsTRACeGeneral.SafeSQL(txtDescription.Text)
    '        objInv.sInv_Flag = "X"
    '        objInv.sInv_Price = "0"
    '        objInv.sInv_Excise = "0"
    '        objInv.sInv_Vat = "0"
    '        objInv.iInv_PerPieces = 0
    '        objInv.iInv_CreatedBy = sSession.UserID
    '        objInv.dInv_CreatedOn = Date.Today
    '        objInv.sInv_Size = clsTRACeGeneral.SafeSQL(txtSize.Text)
    '        objInv.sInv_Color = clsTRACeGeneral.SafeSQL(txtColor.Text)
    '        objInv.sInv_Acode = clsTRACeGeneral.SafeSQL(txtAcode.Text)
    '        objInv.sInv_Ccode = clsTRACeGeneral.SafeSQL(txtCcode.Text)
    '        objInv.sINV_Operation = "U"
    '        objInv.sINV_IPAddress = sSession.IPAddress

    '        Arr = clsInventoryMaster.SaveInventoryMaster(sSession.AccessCode, sSession.AccessCodeID, objInv, 1)
    '        If Arr(0) = "2" Then
    '            lblErrorUp.Text = "Successfully Updated"
    '            lblErrorDown.Text = "Successfully Updated"
    '        ElseIf Arr(0) = "1" Then
    '            lblErrorUp.Text = "Code Is already exists"
    '            lblErrorDown.Text = "Code Is already exists"
    '        End If
    '        LoadTreeview()
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message : lblErrorDown.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdate_Click")
    '    End Try
    'End Sub
    'Protected Sub btnNewComodity_Click(sender As Object, e As EventArgs) Handles btnNewComodity.Click
    '    Try
    '        lblNode.Text = "0" : lblPath.Text = "" : txtCode.Text = "" : txtDescription.Text = ""
    '        txtSize.Text = "0" : txtColor.Text = "0" : lblCode.Text = "Group Code"
    '        txtAcode.Text = objINV.GetMaxIDofInventoryMaster(sSession.AccessCode, sSession.AccessCodeID)
    '        imgbtnSave.Visible = True
    '        imgbtnUpdate.Visible = False
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNew_Click")
    '    End Try
    'End Sub

    Protected Sub loadcat()
        Dim dt As New DataTable
        Dim flag As Integer = 0
        Try
            dt = objINV.loaddetails(sSession.AccessCode, sSession.AccessCodeID)
            'For i = 0 To dt.Rows.Count - 1
            '    If dt.Rows(i)("AS_Nonfood") = 1 Then
            '        flag = 1
            '        lblsize.Visible = True : lblColor.Visible = True : lblAcode.Visible = True
            '        txtSize.Visible = True : txtColor.Visible = True : txtAcode.Visible = True
            '        If dt.Rows(i)("AS_Food") = 1 Then
            '            flag = 0
            '        End If
            '        If dt.Rows(i)("AS_Tools") = 1 Then
            '            flag = 0
            '        End If
            '    End If
            'Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
    '    Dim i As Integer, j As Integer
    '    Dim sbret As String
    '    Dim sArray As String(), sArray1 As String()
    '    Try
    '        btnNew.Enabled = False : btnSave.Enabled = False : btnUpdate.Enabled = False
    '        If ("true") Then
    '            sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasCIM", "ALL")
    '            If sbret = "False" Or sbret = "" Then
    '                Response.Redirect("../Permissions/MasterPermission.aspx")
    '            Else
    '                sArray = sbret.Split(",")
    '                For i = 0 To sArray.Length - 1
    '                    If sArray(i) <> "" Then
    '                        sArray1 = sArray(i).Split(":")
    '                        For j = 0 To sArray1.Length - 1
    '                            Select Case sArray1(0)
    '                                Case "Save"
    '                                    btnSave.Enabled = True : btnNew.Enabled = True
    '                                Case "Update"
    '                                    btnUpdate.Enabled = True
    '                            End Select
    '                        Next
    '                    End If
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub btnsearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnsearch.Click
    '    Dim dt As New DataTable
    '    Dim i As Integer = 0
    '    Dim lParent As Long
    '    Dim sNodeDesc As String
    '    Dim sPath As String = "", sCurNodeDesc As String = ""
    '    Dim objInv As New clsInventoryMaster.Inventory
    '    Try
    '        txtPath.Text = "" : txtDescription.Text = ""
    '        txtCode.Text = "" : lblErrorUp.Text = "" : lblErrorDown.Text = ""
    '        If (txtSearch.Text <> "") Then
    '            ' btnUpdate.Enabled = True
    '            lblErrorDown.Text = ""
    '            lblErrorUp.Text = ""
    '            dt = clsInventoryMaster.SearchInventoryMasterDetails(sSession.AccessCode, sSession.AccessCodeID, txtSearch.Text)
    '            If dt.Rows.Count > 0 Then
    '                If IsDBNull(dt.Rows(0)("Inv_Code").ToString()) = False Then
    '                    txtCode.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_Code").ToString())
    '                Else
    '                    txtCode.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("Inv_Description").ToString()) = False Then
    '                    txtDescription.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
    '                    sCurNodeDesc = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
    '                End If

    '                If IsDBNull(dt.Rows(0)("Inv_Size").ToString()) = False Then
    '                    txtSize.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_Size").ToString())
    '                Else
    '                    txtSize.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("Inv_Color").ToString()) = False Then
    '                    txtColor.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_Color").ToString())
    '                Else
    '                    txtColor.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("Inv_ACode").ToString()) = False Then
    '                    txtAcode.Text = clsTRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Inv_ACode").ToString())
    '                Else
    '                    txtAcode.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("Inv_CreatedBy").ToString()) = False Then
    '                    lblCreatedBy.Text = clsTRACeGeneral.ReplaceSafeSQL(clsTRACeGeneral.GetUserFullName(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("Inv_CreatedBy").ToString()) & " On " & clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("Inv_CreatedOn").ToString(), "D"))
    '                End If
    '                lParent = dt.Rows(0)("Inv_Parent").ToString()

    '            End If
    '            i = lParent
    '            For i = 1 To lParent
    '                objInv = clsInventoryMaster.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
    '                If objInv.iInv_Parent <> 0 Or objInv.sInv_Description <> "" Then
    '                    lParent = objInv.iInv_Parent
    '                    sNodeDesc = objInv.sInv_Description
    '                    sPath = sNodeDesc & "/" & sPath
    '                End If
    '            Next
    '            sNodeDesc = sCurNodeDesc
    '            sPath = sPath & sNodeDesc
    '            txtPath.Text = sPath
    '        End If
    '        txtSearch.Text = ""
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
    '    Try
    '        lblSelectCommodity.Visible = True
    '        ddlCommodity.Visible = True
    '        LoadCommodity()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub LoadCommodity()
    '    Try
    '        ddlCommodity.DataSource = clsInventoryMaster.BindCommodity(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCommodity.DataTextField = "Inv_Description"
    '        ddlCommodity.DataValueField = "Inv_Id"
    '        ddlCommodity.DataBind()
    '        ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
    '    Try
    '        btnMove.Visible = True
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
    '    Dim sStr As String = ""
    '    Try
    '        sStr = clsInventoryMaster.UpdateEditedData(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lblNode.Text)
    '        lblErrorUp.Text = sStr : lblErrorDown.Text = sStr
    '        lblSelectCommodity.Visible = False
    '        ddlCommodity.SelectedIndex = 0
    '        ddlCommodity.Visible = False
    '        btnMove.Visible = False
    '        LoadTreeview()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Protected Sub txtAcode_TextChanged(sender As Object, e As EventArgs) Handles txtAcode.TextChanged

    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objInvS As New clsInventoryMaster.Inventory
        Dim Arr() As String
        Dim Check As String
        Dim iCommodityID As Integer
        Dim dNotificationFromDate As Date
        Dim iID As Integer
        Try

            If (lblPath.Text = "Selected path") Then
                lblUserMasterDetailsValidationMsg.Text = "Select New Group Button"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                Exit Sub
            Else
                lblError.Text = ""
                If lblNode.Text = "0" Then
                    objInvS.iInv_Unit = 0
                    objInvS.iInv_AlterUnit = 0
                    objInvS.iInv_ID = 0
                Else
                    objInvS.iInv_Unit = 0
                    objInvS.iInv_AlterUnit = 0
                    objInvS.iInv_ID = lblNode.Text
                End If
                objInvS.iInv_Parent = lblNode.Text
                objInvS.sInv_Code = objFasgnrl.SafeSQL(txtCode.Text)
                objInvS.sInv_Description = objFasgnrl.SafeSQL(txtDescription.Text)
                objInvS.sInv_Flag = "X"
                objInvS.sInv_Price = "0"
                objInvS.sInv_Excise = "0"
                objInvS.sInv_Vat = "0"
                objInvS.sInv_Size = 0
                objInvS.iInv_PerPieces = 0
                objInvS.iInv_CreatedBy = sSession.UserID
                objInvS.dInv_CreatedOn = Date.Today
                objInvS.sINV_Operation = "C"
                objInvS.sINV_IPAddress = sSession.IPAddress

                If Not txtColor.Text = "" Then
                    objInvS.sInv_Color = objFasgnrl.SafeSQL(txtColor.Text)
                Else
                    objInvS.sInv_Color = ""
                End If
                If Not txtSize.Text = "" Then
                    objInvS.sInv_Size = objFasgnrl.SafeSQL(txtSize.Text)
                Else
                    objInvS.sInv_Size = "0"
                End If

                If Not txtSize.Text = "" Then
                    objInvS.sInv_Acode = objFasgnrl.SafeSQL(txtAcode.Text)
                Else
                    objInvS.sInv_Acode = ""
                End If

                If Not txtCcode.Text = "" Then
                    objInvS.sInv_Ccode = objFasgnrl.SafeSQL(txtCcode.Text)
                Else
                    objInvS.sInv_Ccode = ""
                End If

                If (txtCode.Text <> "") Then
                    Check = objDB.SQLGetDescription(sSession.AccessCode, "select Inv_Description from inventory_master where Inv_Code='" & txtCode.Text & "'")
                    If (Check <> "") Then
                        lblError.Text = "Item Code Already Exist"
                        txtCode.Text = ""
                        Exit Sub
                        'iID = 0
                        'iCommodityID = 0
                    Else
                        ' Check = clsInventoryMaster.CheckExistOrNot(sSession.AccessCode, sSession.AccessCodeID, objInv, 0)
                        Arr = objINV.SaveInventoryMaster(sSession.AccessCode, sSession.AccessCodeID, objInvS, 0)
                        'iID = Arr(1)
                        'iCommodityID = objINV.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, Arr(1))
                        If Arr(0) = "2" Then
                            lblError.Text = "Successfully Updated"

                        ElseIf Arr(0) = "3" Then
                            lblError.Text = "Successfully Saved"

                        ElseIf Arr(0) = "1" Then
                            lblError.Text = "Code is already exists"
                        End If
                    End If
                End If
                LoadTreeview()

                'Save To GST Rate Table'

                'If iCommodityID > 0 And iID > 0 Then
                iCommodityID = objINV.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, Arr(1))

                objInvS.iGST_ID = 0
                If ddlGSTSchedule.SelectedIndex <> -1 And ddlGSTSchedule.SelectedIndex > 0 Then
                    If iCommodityID = 0 Then
                        lblError.Text = "HSN Details only for Goods,not for commodities."
                        ClearHSNDetails()
                        Exit Sub
                    Else
                        objInvS.iGST_ScheduleID = ddlGSTSchedule.SelectedValue
                        objInvS.iGST_CommodityID = iCommodityID
                        objInvS.iGST_ItemID = Arr(1)

                        If Not txtScheduleType.Text = "" Then
                            objInvS.iGST_ScheduleType = objFasgnrl.SafeSQL(txtScheduleType.Text)
                        Else
                            objInvS.iGST_ScheduleType = 0
                        End If

                        If Not txtGSTRate.Text = "" Then
                            objInvS.dGST_GSTRate = objFasgnrl.SafeSQL(txtGSTRate.Text)
                        Else
                            objInvS.dGST_GSTRate = 0
                        End If

                        If Not txtSLNo.Text = "" Then
                            objInvS.sGST_SlNo = objFasgnrl.SafeSQL(txtSLNo.Text)
                        Else
                            objInvS.sGST_SlNo = ""
                        End If

                        If Not txtCHST.Text = "" Then
                            objInvS.sGST_CHST = objFasgnrl.SafeSQL(txtCHST.Text)
                        Else
                            objInvS.sGST_CHST = ""
                        End If

                        If Not txtChapter.Text = "" Then
                            objInvS.sGST_Chapter = objFasgnrl.SafeSQL(txtChapter.Text)
                        Else
                            objInvS.sGST_Chapter = ""
                        End If

                        If Not txtHeading.Text = "" Then
                            objInvS.sGST_Heading = objFasgnrl.SafeSQL(txtHeading.Text)
                        Else
                            objInvS.sGST_Heading = ""
                        End If

                        If Not txtSubHeading.Text = "" Then
                            objInvS.sGST_SubHeading = objFasgnrl.SafeSQL(txtSubHeading.Text)
                        Else
                            objInvS.sGST_SubHeading = ""
                        End If

                        If Not txtTarrif.Text = "" Then
                            objInvS.sGST_Tarrif = objFasgnrl.SafeSQL(txtTarrif.Text)
                        Else
                            objInvS.sGST_Tarrif = ""
                        End If

                        If Not txtSubSlNo.Text = "" Then
                            objInvS.sGST_SubSlNo = objFasgnrl.SafeSQL(txtSubSlNo.Text)
                        Else
                            objInvS.sGST_SubSlNo = ""
                        End If

                        If Not txtCESS.Text = "" Then
                            objInvS.sGST_CESS = objFasgnrl.SafeSQL(txtCESS.Text)
                        Else
                            objInvS.sGST_CESS = 0
                        End If

                        If Not txtGoodDescription.Text = "" Then
                            objInvS.sGST_GoodDescription = objFasgnrl.SafeSQL(txtGoodDescription.Text)
                        Else
                            objInvS.sGST_GoodDescription = ""
                        End If

                        If Not txtNotificationNo.Text = "" Then
                            objInvS.sGST_NotificationNo = objFasgnrl.SafeSQL(txtNotificationNo.Text)
                        Else
                            objInvS.sGST_NotificationNo = ""
                        End If

                        If Not txtNotificationDate.Text = "" Then
                            objInvS.dGST_NotificationFromDate = DateTime.ParseExact(txtNotificationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objInvS.dGST_NotificationFromDate = "01/01/1900"
                        End If

                        'dNotificationFromDate = objINV.GetNotificationFromDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID, Arr(1))
                        'If dNotificationFromDate <> "01/01/1900" Or dNotificationFromDate <> "01-01-1900" Then
                        '    objInvS.dGST_NotificationToDate = dNotificationFromDate
                        'Else
                        '    objInvS.dGST_NotificationToDate = "01/01/1900"
                        'End If
                        objInvS.dGST_NotificationToDate = "01/01/1900"

                        If Not txtFileNo.Text = "" Then
                            objInvS.sGST_FileNo = objFasgnrl.SafeSQL(txtFileNo.Text)
                        Else
                            objInvS.sGST_FileNo = ""
                        End If

                        If Not txtFileDate.Text = "" Then
                            objInvS.dGST_FileFromDate = DateTime.ParseExact(txtFileDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objInvS.dGST_FileFromDate = "01/01/1900"
                        End If

                        objInvS.dGST_FileToDate = "01/01/1900"

                        objInvS.sGST_Status = "W"
                        objInvS.iGST_CompID = sSession.AccessCodeID
                        objInvS.iGST_YearID = sSession.YearID
                        objInvS.sGST_Operation = "C"
                        objInvS.sGST_IPAddress = sSession.IPAddress

                        objINV.SaveGSTRates(sSession.AccessCode, sSession.AccessCodeID, objInvS, 0)
                    End If
                End If
            End If
            'Save to GST Rate Table'

            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub
    Protected Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try

            imgbtnUpdate.Visible = False
            imgbNewItem.Visible = False
            If sIMSave = "YES" Then
                imgbtnSave.Visible = True
                imgbtnGroup.Visible = True
            End If

            lblError.Text = ""
            ' btnSave.Enabled = True : btnUpdate.Enabled = False
            txtCode.Text = "" : txtDescription.Text = "" : txtSearch.Text = ""
            txtAcode.Text = "" : txtColor.Text = "" : txtSize.Text = "" : txtCcode.Text = ""

            ClearHSNDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim objInvS As New clsInventoryMaster.Inventory
        Dim Arr() As String
        Dim iCommodityID As Integer
        Dim dNotificationFromDate As Date
        Try
            lblError.Text = ""
            If lblNode.Text = "0" Then
                objInvS.iInv_Unit = 0
                objInvS.iInv_AlterUnit = 0
                objInvS.iInv_ID = 0
            Else
                objInvS.iInv_Unit = 0
                objInvS.iInv_AlterUnit = 0
                objInvS.iInv_ID = lblNode.Text
            End If
            objInvS.iInv_Parent = lblNode.Text
            objInvS.sInv_Code = objFasgnrl.SafeSQL(txtCode.Text)
            objInvS.sInv_Description = objFasgnrl.SafeSQL(txtDescription.Text)
            objInvS.sInv_Flag = "X"
            objInvS.sInv_Price = "0"
            objInvS.sInv_Excise = "0"
            objInvS.sInv_Vat = "0"
            objInvS.iInv_PerPieces = 0
            objInvS.iInv_CreatedBy = sSession.UserID
            objInvS.dInv_CreatedOn = Date.Today
            objInvS.sInv_Size = objFasgnrl.SafeSQL(txtSize.Text)
            objInvS.sInv_Color = objFasgnrl.SafeSQL(txtColor.Text)
            objInvS.sInv_Acode = objFasgnrl.SafeSQL(txtAcode.Text)
            objInvS.sInv_Ccode = objFasgnrl.SafeSQL(txtCcode.Text)
            objInvS.sINV_Operation = "U"
            objInvS.sINV_IPAddress = sSession.IPAddress

            Arr = objINV.SaveInventoryMaster(sSession.AccessCode, sSession.AccessCodeID, objInvS, 1)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
            ElseIf Arr(0) = "1" Then
                lblError.Text = "Code Is already exists"
            End If
            LoadTreeview()

            'Save To GST Rate Table'

            If ddlGSTSchedule.Items.Count > 0 Then
                If ddlGSTSchedule.SelectedIndex > 0 Then

                    iCommodityID = objINV.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, Arr(1))
                    If iCommodityID = 0 Then
                        lblError.Text = "HSN Details only for Goods,not for commodities."
                        ClearHSNDetails()
                        Exit Sub
                    End If

                    objInvS.iGST_ID = 0
                    If ddlGSTSchedule.SelectedIndex <> -1 Or ddlGSTSchedule.SelectedIndex > 0 Then
                        objInvS.iGST_ScheduleID = ddlGSTSchedule.SelectedValue
                    Else
                        objInvS.iGST_ScheduleID = 0
                    End If
                    'objInvS.iGST_ScheduleID = ddlGSTSchedule.SelectedValue
                    objInvS.iGST_CommodityID = iCommodityID
                    objInvS.iGST_ItemID = Arr(1)

                    If Not txtScheduleType.Text = "" Then
                        objInvS.iGST_ScheduleType = objFasgnrl.SafeSQL(txtScheduleType.Text)
                    Else
                        objInvS.iGST_ScheduleType = 0
                    End If

                    If Not txtGSTRate.Text = "" Then
                        objInvS.dGST_GSTRate = objFasgnrl.SafeSQL(txtGSTRate.Text)
                    Else
                        objInvS.dGST_GSTRate = 0
                    End If

                    If Not txtSLNo.Text = "" Then
                        objInvS.sGST_SlNo = objFasgnrl.SafeSQL(txtSLNo.Text)
                    Else
                        objInvS.sGST_SlNo = ""
                    End If

                    If Not txtCHST.Text = "" Then
                        objInvS.sGST_CHST = objFasgnrl.SafeSQL(txtCHST.Text)
                    Else
                        objInvS.sGST_CHST = ""
                    End If

                    If Not txtChapter.Text = "" Then
                        objInvS.sGST_Chapter = objFasgnrl.SafeSQL(txtChapter.Text)
                    Else
                        objInvS.sGST_Chapter = ""
                    End If

                    If Not txtHeading.Text = "" Then
                        objInvS.sGST_Heading = objFasgnrl.SafeSQL(txtHeading.Text)
                    Else
                        objInvS.sGST_Heading = ""
                    End If

                    If Not txtSubHeading.Text = "" Then
                        objInvS.sGST_SubHeading = objFasgnrl.SafeSQL(txtSubHeading.Text)
                    Else
                        objInvS.sGST_SubHeading = ""
                    End If

                    If Not txtTarrif.Text = "" Then
                        objInvS.sGST_Tarrif = objFasgnrl.SafeSQL(txtTarrif.Text)
                    Else
                        objInvS.sGST_Tarrif = ""
                    End If

                    If Not txtSubSlNo.Text = "" Then
                        objInvS.sGST_SubSlNo = objFasgnrl.SafeSQL(txtSubSlNo.Text)
                    Else
                        objInvS.sGST_SubSlNo = ""
                    End If

                    If Not txtCESS.Text = "" Then
                        objInvS.sGST_CESS = objFasgnrl.SafeSQL(txtCESS.Text)
                    Else
                        objInvS.sGST_CESS = 0
                    End If

                    If Not txtGoodDescription.Text = "" Then
                        objInvS.sGST_GoodDescription = objFasgnrl.SafeSQL(txtGoodDescription.Text)
                    Else
                        objInvS.sGST_GoodDescription = ""
                    End If

                    If Not txtNotificationNo.Text = "" Then
                        objInvS.sGST_NotificationNo = objFasgnrl.SafeSQL(txtNotificationNo.Text)
                    Else
                        objInvS.sGST_NotificationNo = ""
                    End If

                    If Not txtNotificationDate.Text = "" Then
                        objInvS.dGST_NotificationFromDate = DateTime.ParseExact(txtNotificationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objInvS.dGST_NotificationFromDate = "01/01/1900"
                    End If

                    'dNotificationFromDate = objINV.GetNotificationFromDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID, Arr(1))
                    'If dNotificationFromDate <> "01/01/1900" Or dNotificationFromDate <> "01-01-1900" Then
                    '    objInvS.dGST_NotificationToDate = dNotificationFromDate
                    'Else
                    '    objInvS.dGST_NotificationToDate = "01/01/1900"
                    'End If
                    objInvS.dGST_NotificationToDate = "01/01/1900"

                    If Not txtFileNo.Text = "" Then
                        objInvS.sGST_FileNo = objFasgnrl.SafeSQL(txtFileNo.Text)
                    Else
                        objInvS.sGST_FileNo = ""
                    End If

                    If Not txtFileDate.Text = "" Then
                        objInvS.dGST_FileFromDate = DateTime.ParseExact(txtFileDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objInvS.dGST_FileFromDate = "01/01/1900"
                    End If

                    objInvS.dGST_FileToDate = "01/01/1900"

                    objInvS.sGST_Status = "W"
                    objInvS.iGST_CompID = sSession.AccessCodeID
                    objInvS.iGST_YearID = sSession.YearID
                    objInvS.sGST_Operation = "C"
                    objInvS.sGST_IPAddress = sSession.IPAddress

                    objINV.SaveGSTRates(sSession.AccessCode, sSession.AccessCodeID, objInvS, 0)
                End If
            End If
            'Save to GST Rate Table'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Public Sub ClearHSNDetails()
        Try
            ddlGSTSchedule.Items.Clear()
            txtScheduleType.Text = ""
            txtGSTRate.Text = ""
            txtSLNo.Text = ""
            txtCHST.Text = ""
            txtChapter.Text = ""
            txtHeading.Text = ""
            txtSubHeading.Text = ""
            txtTarrif.Text = ""
            txtSubSlNo.Text = ""
            txtCESS.Text = ""
            txtGoodDescription.Text = ""
            txtNotificationNo.Text = ""
            txtNotificationDate.Text = ""
            txtFileNo.Text = ""
            txtFileDate.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim lParent As Long
        Dim sNodeDesc As String
        Dim sPath As String = "", sCurNodeDesc As String = ""
        Dim objInvStruct As New clsInventoryMaster.Inventory
        Try
            lblPath.Text = "" : txtDescription.Text = ""
            txtCode.Text = "" : lblError.Text = ""
            If (txtSearch.Text <> "") Then
                ' btnUpdate.Enabled = True
                lblError.Text = ""

                dt = objINV.SearchInventoryMasterDetails(sSession.AccessCode, sSession.AccessCodeID, txtSearch.Text)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("Inv_Code").ToString()) = False Then
                        txtCode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Code").ToString())
                    Else
                        txtCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Inv_Description").ToString()) = False Then
                        txtDescription.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                        sCurNodeDesc = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                    End If

                    If IsDBNull(dt.Rows(0)("Inv_Size").ToString()) = False Then
                        txtSize.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Size").ToString())
                    Else
                        txtSize.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Inv_Color").ToString()) = False Then
                        txtColor.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Color").ToString())
                    Else
                        txtColor.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Inv_ACode").ToString()) = False Then
                        txtAcode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_ACode").ToString())
                    Else
                        txtAcode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Inv_CreatedBy").ToString()) = False Then
                        txtCreatedBy.Text = objFasgnrl.ReplaceSafeSQL(objAllGnrl.GetUserFullName(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("Inv_CreatedBy").ToString()))
                        txtCreatedOn.Text = objFasgnrl.FormatDtForRDBMS(dt.Rows(0)("Inv_CreatedOn").ToString(), "D")
                    End If
                    lParent = dt.Rows(0)("Inv_Parent").ToString()
                End If
                i = lParent
                For i = 1 To lParent
                    objInvStruct = objINV.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
                    If objInvStruct.iInv_Parent <> 0 Or objInvStruct.sInv_Description <> "" Then
                        lParent = objInvStruct.iInv_Parent
                        sNodeDesc = objInvStruct.sInv_Description
                        sPath = sNodeDesc & "/" & sPath
                    End If
                Next
                sNodeDesc = sCurNodeDesc
                sPath = sPath & sNodeDesc
                lblPath.Text = sPath
            End If
            txtSearch.Text = " "
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Public Sub BindGST(ByVal sSearch As String, ByVal sHSNCodeSearch As String)
        Dim dtGST As New DataTable
        Try
            dtGST = objINV.SearchGSTDetails(sSession.AccessCode, sSession.AccessCodeID, sSearch, sHSNCodeSearch)
            ddlGSTSchedule.DataSource = dtGST
            ddlGSTSchedule.DataTextField = "AGS_GoodDescription"
            ddlGSTSchedule.DataValueField = "AGS_ID"
            ddlGSTSchedule.DataBind()
            ddlGSTSchedule.Items.Insert(0, "--- Select GST Good Description ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnGroup_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnGroup.Click
        Try
            lblNode.Text = "0" : lblPath.Text = "" : txtCode.Text = "" : txtDescription.Text = ""
            txtSize.Text = "0" : txtColor.Text = "" : lblCode.Text = "Group Code"
            txtAcode.Text = objINV.GetMaxIDofInventoryMaster(sSession.AccessCode, sSession.AccessCodeID)

            If sIMSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnGroup_Click")
        End Try
    End Sub
    Private Sub imgbNewItem_Click(sender As Object, e As ImageClickEventArgs) Handles imgbNewItem.Click
        Try
            If sIMSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            imgbtnUpdate.Visible = False
            lblError.Text = ""
            ' btnSave.Enabled = True : btnUpdate.Enabled = False
            txtCode.Text = "" : txtDescription.Text = "" : txtSearch.Text = ""
            txtAcode.Text = "" : txtColor.Text = "" : txtSize.Text = "" : txtCcode.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbNewItem_Click")
        End Try
    End Sub
    Private Sub ddlGSTSchedule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGSTSchedule.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iCommodityID As Integer
        Try
            'iCommodityID = objINV.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, lblNode.Text)
            'If iCommodityID = 0 Then
            '    lblError.Text = "Save/Update the HSN Details only for Goods,not for commodities."
            '    Exit Sub
            'End If

            dt = objINV.GetGSTDetails(sSession.AccessCode, sSession.AccessCodeID, ddlGSTSchedule.SelectedValue)
            If dt.Rows.Count > 0 Then
                txtScheduleType.Text = dt.Rows(0)("AGS_Schedule_Type")
                txtGSTRate.Text = dt.Rows(0)("AGS_GSTRate")
                txtSLNo.Text = dt.Rows(0)("AGS_SlnoOfSchedule")
                txtCHST.Text = dt.Rows(0)("AGS_CHST")
                txtChapter.Text = dt.Rows(0)("AGS_Chapter")
                txtHeading.Text = dt.Rows(0)("AGS_Heading")
                txtSubHeading.Text = dt.Rows(0)("AGS_SubHeading")
                txtTarrif.Text = dt.Rows(0)("AGS_Tarrif")
                txtSubSlNo.Text = ""
                txtCESS.Text = ""
                txtGoodDescription.Text = dt.Rows(0)("AGS_GoodDescription")
                txtNotificationNo.Text = dt.Rows(0)("AGS_NotificationNo")
                txtNotificationDate.Text = objFasgnrl.FormatDtForRDBMS(dt.Rows(0)("AGS_NotificationDate"), "D")
                txtFileNo.Text = dt.Rows(0)("AGS_FileNo")
                txtFileDate.Text = objFasgnrl.FormatDtForRDBMS(dt.Rows(0)("AGS_FileDate"), "D")

                'txtScheduleType.Text = dt.Rows(0)("Inv_ScheduleType")
                'txtGSTRate.Text = dt.Rows(0)("Inv_GSTRate")
                'txtSLNo.Text = dt.Rows(0)("Inv_SlNo")
                'txtCHST.Text = dt.Rows(0)("Inv_CHST")
                'txtChapter.Text = dt.Rows(0)("Inv_Chapter")
                'txtHeading.Text = dt.Rows(0)("Inv_Heading")
                'txtSubHeading.Text = dt.Rows(0)("Inv_SubHeading")
                'txtTarrif.Text = dt.Rows(0)("Inv_Tarrif")
                'txtSubSlNo.Text = dt.Rows(0)("Inv_SubSlNo")
                'txtCESS.Text = dt.Rows(0)("Inv_CESS")
                'txtGoodDescription.Text = dt.Rows(0)("Inv_GoodDescription")
                'txtNotificationNo.Text = dt.Rows(0)("Inv_NotificationNo")
                'txtNotificationDate.Text = dt.Rows(0)("Inv_NotificationDate")
                'txtFileNo.Text = dt.Rows(0)("Inv_FileNo")
                'txtFileDate.Text = dt.Rows(0)("Inv_FileDate")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGSTSchedule_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnHSNDescSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnHSNDescSearch.Click
        Try
            BindGST(txtHSNDesc.Text, "")
            txtHSNDesc.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnHSNDescSearch_Click")
        End Try
    End Sub
    Private Sub imgbtnHSTCodeSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnHSTCodeSearch.Click
        Try
            BindGST("", txtHSNCode.Text)
            txtHSNCode.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnHSTCodeSearch_Click")
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            If ddlCommodity.SelectedIndex > 0 Then
                lblError.Text = "" : lblPath.Text = ""
                txtCode.Text = "" : txtDescription.Text = "" : txtSearch.Text = ""
                txtAcode.Text = "" : txtColor.Text = "" : txtSize.Text = "" : txtCcode.Text = ""
                ClearHSNDetails()

                BindDescription(ddlCommodity.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer)
        Try
            lstBoxDescription.DataSource = objINV.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            lstBoxDescription.DataTextField = "INV_Description"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim lParent As Long
        Dim sNodeDesc As String
        Dim sPath As String = ""
        Dim sCurNodeDesc As String = ""
        Dim objInvStr As New clsInventoryMaster.Inventory
        Dim dtGST As New DataTable
        Try
            lblError.Text = ""
            txtSize.Text = "" : txtColor.Text = "" : txtAcode.Text = ""
            lblNode.Text = lstBoxDescription.SelectedValue
            txtCcode.Text = ""
            If sIMSave = "YES" Then
                imgbNewItem.Visible = True
            End If

            imgbtnUpdate.Visible = True
            imgbtnSave.Visible = False

            imgbtnGroup.Visible = False
            ClearHSNDetails()

            dt = objINV.GetInventoryMasterDetails(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Inv_Code").ToString()) = False Then
                    txtCode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Code").ToString())
                End If
                If IsDBNull(dt.Rows(0)("Inv_Description").ToString()) = False Then
                    txtDescription.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                    sCurNodeDesc = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Description").ToString())
                End If
                If IsDBNull(dt.Rows(0)("Inv_CreatedBy").ToString()) = False Then
                    txtCreatedBy.Text = objAllGnrl.GetUserFullName(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("Inv_CreatedBy").ToString())
                    txtCreatedOn.Text = objFasgnrl.FormatDtForRDBMS(dt.Rows(0)("Inv_CreatedOn").ToString(), "D")
                End If
                If IsDBNull(dt.Rows(0)("Inv_Size")) = False Then
                    txtSize.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Size"))
                End If
                If IsDBNull(dt.Rows(0)("Inv_Color")) = False Then
                    txtColor.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Color"))
                End If
                If IsDBNull(dt.Rows(0)("Inv_Acode")) = False Then
                    txtAcode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("Inv_Acode"))
                End If
                If IsDBNull(dt.Rows(0)("INV_Ccode")) = False Then
                    txtCcode.Text = objFasgnrl.ReplaceSafeSQL(dt.Rows(0)("INV_Ccode"))
                End If

                dtGST = objINV.GetGSTRatesDetails(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                If dtGST.Rows.Count > 0 Then
                    If IsDBNull(dtGST.Rows(0)("GST_ScheduleType")) = False Then
                        txtScheduleType.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_ScheduleType"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_GSTRate")) = False Then
                        txtGSTRate.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_GSTRate"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SlNo")) = False Then
                        txtSLNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SlNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_CHST")) = False Then
                        txtCHST.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_CHST"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Chapter")) = False Then
                        txtChapter.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Chapter"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Heading")) = False Then
                        txtHeading.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Heading"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SubHeading")) = False Then
                        txtSubHeading.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SubHeading"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_Tarrif")) = False Then
                        txtTarrif.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_Tarrif"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_SubSlNo")) = False Then
                        txtSubSlNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_SubSlNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_CESS")) = False Then
                        txtCESS.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_CESS"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_GoodDescription")) = False Then
                        txtGoodDescription.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_GoodDescription"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_NotificationNo")) = False Then
                        txtNotificationNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_NotificationNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_NotificationFromDate")) = False Then
                        txtNotificationDate.Text = objFasgnrl.FormatDtForRDBMS(dtGST.Rows(0)("GST_NotificationFromDate"), "D")
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_FileNo")) = False Then
                        txtFileNo.Text = objFasgnrl.ReplaceSafeSQL(dtGST.Rows(0)("GST_FileNo"))
                    End If
                    If IsDBNull(dtGST.Rows(0)("GST_FileFromDate")) = False Then
                        txtFileDate.Text = objFasgnrl.FormatDtForRDBMS(dtGST.Rows(0)("GST_FileFromDate"), "D")
                    End If
                End If

                lParent = dt.Rows(0)("Inv_Parent").ToString()
            End If
            i = lParent
            For i = 1 To lParent
                objInvStr = objINV.GetPath(sSession.AccessCode, sSession.AccessCodeID, lParent)
                If objInvStr.iInv_Parent <> 0 Or objInvStr.sInv_Description <> "" Then
                    lParent = objInvStr.iInv_Parent
                    sNodeDesc = objInvStr.sInv_Description
                    sPath = sNodeDesc & "/" & sPath
                End If
            Next
            sNodeDesc = sCurNodeDesc
            sPath = sPath & sNodeDesc
            lblPath.Text = objFasgnrl.ReplaceSafeSQL(sPath)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "tvCategory_NodeClick")
        End Try
    End Sub
End Class
