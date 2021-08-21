Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class FixedAsset_AssetTransaction
    Inherits System.Web.UI.Page
    Private sFormName As String = "FixedAsset_AssetTransaction"
    Private objerrorclass As New BusinesLayer.Components.ErrorClass
    Dim objAsstTrn As New ClsAssetTransactionAddition
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Dim objGen As New clsFASGeneral
    Private objclsAttachments As New clsAttachments
    Private Shared sSession As AllSession
    Public dtFixedAssetTrn As New DataTable
    Private Shared dtAttach As New DataTable
    Private Shared iDocID As Integer
    Private Shared iAttachID As Integer
    Private Shared iStatus As Integer
    Private Shared sWFDelete As String
    Private Shared sINWView As String
    Private Shared sINWDownload As String
    Dim objClsFASGnrl As New clsFASGeneral
    Private Shared sUMBackStatus As String
    Private Shared iAdd As Integer = 0
    Dim dt As New DataTable
    Private Shared sIKBBackStatus As String
    Private Shared iMID As Integer
    Private Shared sSelectedChecksIDs As String = ""
    Private Shared sSelId As String
    Private Shared sFOLDER As String = ""
    Private Shared iFolID As Integer = 0
    Private Shared irefnoid As Integer = 0
    Private objIndex As New clsIndexing
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnsave.ImageUrl = "~/Images/Save24.png"
        imgbtnDADD.ImageUrl = "~/Images/Add24.png"
        imgbtnOtherCADD.ImageUrl = "~/Images/Add24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"

        ImgBtnBack.ImageUrl = "~/Images/Backward24.png"
        ImgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        Imgbtnphyvrfn.ImageUrl = "~/Images/Update24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterID As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'txtbxAstCode.Text = objAsstTrn.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
                sSelectedChecksIDs = ""
                Session("Attachment1") = Nothing
                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")
                Session("Attachment1") = dt
                Session("dtAssetPayment") = Nothing
                Session("dtFixedAssetTrn") = Nothing
                BindHeadofAccounts() : LoadZone() : TransactionType()
                LoadSubGL() : loadAssetType() : AssetTransfer() : LoadSuppliers()
                LoadCurrency()
                loadExistingTRnNo()
                'Me.imgbtnsave.Attributes.Add("OnClick", "javascript:return Validate();")

                RFVExtno.InitialValue = "Select ExistingTransactionNo" : RFVExtno.ErrorMessage = "Select ExistingTransactionNo"
                RFVAssetTrnfr.InitialValue = "Select Asset Transfer" : RFVAssetTrnfr.ErrorMessage = "Select Asset Transfer"
                '  RFVddlCurencyType.InitialValue = "Select Currency" : RFVddlCurencyType.ErrorMessage = "Select Currency"
                'RFVtCurency.ErrorMessage = "Enter Amount."
                'REVtCurency.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                'REVtCurency.ErrorMessage = "Enter only numbers"
                RFVAccZone.InitialValue = "Select Zone" : RFVAccZone.ErrorMessage = "Select Zone"
                RFVAccRgn.InitialValue = "Select Region" : RFVAccRgn.ErrorMessage = "Select Region"
                RFVAccArea.InitialValue = "Select Area" : RFVAccArea.ErrorMessage = "Select Area"
                RFVAccBrnch.InitialValue = "Select Branch" : RFVAccBrnch.ErrorMessage = "Select Branch"
                RFVLocation.ErrorMessage = "Enter Location"
                RFVTRType.InitialValue = "Select Transaction Type" : RFVAccBrnch.ErrorMessage = "Select Transaction Type"
                RFVSupplierName.InitialValue = "Select Supplier" : RFVSupplierName.ErrorMessage = "Select Supplier"
                RFVdrpAstype.InitialValue = "Select AssetType" : RFVdrpAstype.ErrorMessage = "Select AssetType"
                RFVAstRefNo.ErrorMessage = "Enter Asset RefNo" : RFVtxtbxDteofPurchase.ErrorMessage = "Enter Date Of Purchase"
                RFVtxtbxDteofPurchase1.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                RFVtxtbxDteofPurchase1.ErrorMessage = "Enter Valid Date"

                RFVtxtDtAddtn.ErrorMessage = "Enter Date Of Purchase"
                REVtxtDtAddtn.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVtxtDtAddtn.ErrorMessage = "Enter Valid Date"

                REVamount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                REVamount.ErrorMessage = "Enter Valid Amount"
                RFVamount.ErrorMessage = "Enter Amount"
                RFVtxtbxDteCmmunictn1.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                RFVtxtbxDteCmmunictn1.ErrorMessage = "Enter Valid Date"
                RFVtxtbxDteCmmunictn.ErrorMessage = "Enter Date Of Commission"


                RFVddlDrOtherHead.InitialValue = "Select Head of Account" : RFVddlDrOtherHead.ErrorMessage = "Select Head Of Account"
                RFVddlDbOtherGL.InitialValue = "Select GL Code" : RFVddlDbOtherGL.ErrorMessage = "Select GL Code"
                'RFVddlDbOtherSubGL.InitialValue = "Select SubGL Code" : RFVddlDbOtherSubGL.ErrorMessage = "Select SubGL Code"
                RFVtxtOtherDAmount.ErrorMessage = "Enter Amount"

                RFVddlCrOtherHead.InitialValue = "Select Head of Account" : RFVddlCrOtherHead.ErrorMessage = "Select Head of Account"
                RFVddlCrOtherGL.InitialValue = "Select GL Code" : RFVddlCrOtherGL.ErrorMessage = "Select GL Code"
                ' RFVddlCrOtherSubGL.InitialValue = "Select SubGL Code" : RFVddlCrOtherSubGL.ErrorMessage = "Select SubGL Code"
                RFVCRAmount.ErrorMessage = "Enter Amount"


                '  imgView.ImageUrl = "~/Images/NoImage.jpg"
                sINWView = "YES" : sINWDownload = "YES" : sWFDelete = "YES"
                iAttachID = 0
                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                'imgbtnAttach.Attributes.Add("OnClick", "$('#myModalAttch').modal('show');return false;")
                Imgbtnphyvrfn.Attributes.Add("OnClick", "$('#myModalPhyvrn').modal('show');return false;")

                '  BindAllAttachments(sSession.AccessCode, iAttachID)

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    iMID = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExtTrnNo.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExtTrnNo_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sUMBackStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                'irefnoid = 0 'For redirecting page to Additional Details
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload")
        End Try
    End Sub
    Private Sub loadExistingTRnNo()
        Try
            ddlExtTrnNo.DataSource = objAsstTrn.ExistingTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlExtTrnNo.DataTextField = "AFAA_AssetNo"
            ddlExtTrnNo.DataValueField = "AFAA_ID"
            ddlExtTrnNo.DataBind()
            ddlExtTrnNo.Items.Insert(0, "Select ExistingTransactionNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingTRnNo")
        End Try
    End Sub
    Private Sub loadExistingItemCode()
        Try
            txtbxItmCode.DataSource = objAsstTrn.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, sSession.YearID)
            txtbxItmCode.DataTextField = "AFAM_ItemCode"
            txtbxItmCode.DataValueField = "AFAM_ItemCode"
            txtbxItmCode.DataBind()
            txtbxItmCode.Items.Insert(0, "Select Itemcode")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingItemCode")
        End Try
    End Sub
    Private Sub LoadCurrency()
        Dim dCurrencyDt As New DataTable
        Try
            dCurrencyDt = objAsstTrn.LoadCurrency(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurencyType.DataSource = dCurrencyDt
            ddlCurencyType.DataTextField = "CUR_CountryName"
            ddlCurencyType.DataValueField = "CUR_ID"
            ddlCurencyType.DataBind()
            ddlCurencyType.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCurrency")
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objAsstTrn.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSuppliers")
        End Try
    End Sub
    Public Sub TransactionType()
        Try
            ddlTrTypes.Items.Insert(0, "Select Transaction Type")
            ddlTrTypes.Items.Insert(1, "Addition")
            ddlTrTypes.Items.Insert(2, "Transfers")
            ddlTrTypes.Items.Insert(3, "Revaluation")
            ddlTrTypes.Items.Insert(4, "Foreign Exchange")
            ddlTrTypes.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TransactionType")
        End Try
    End Sub
    Public Sub AssetTransfer()
        Try
            ddlAssetTrnfr.Items.Insert(0, "Select Asset Transfer")
            ddlAssetTrnfr.Items.Insert(1, "Local")
            ddlAssetTrnfr.Items.Insert(2, "Imported")
            ddlAssetTrnfr.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AssetTransfer")
        End Try
    End Sub

    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAsstTrn.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadZone")
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            dt = objAsstTrn.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID)
            drpAstype.DataTextField = "GL_Desc"
            drpAstype.DataValueField = "GL_ID"
            drpAstype.DataSource = dt
            drpAstype.DataBind()
            drpAstype.Items.Insert(0, "Select AssetType")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType")
        End Try
    End Sub
    Public Sub BindHeadofAccounts()
        Try
            ddlDrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlDrOtherHead.Items.Insert(1, "Asset")
            ddlDrOtherHead.Items.Insert(2, "Income")
            ddlDrOtherHead.Items.Insert(3, "Expenditure")
            ddlDrOtherHead.Items.Insert(4, "Liabilities")
            ddlDrOtherHead.SelectedIndex = 0

            ddlCrOtherHead.Items.Insert(0, "Select Head of Account")
            ddlCrOtherHead.Items.Insert(1, "Asset")
            ddlCrOtherHead.Items.Insert(2, "Income")
            ddlCrOtherHead.Items.Insert(3, "Expenditure")
            ddlCrOtherHead.Items.Insert(4, "Liabilities")
            ddlCrOtherHead.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeadofAccounts")
        End Try
    End Sub
    Private Sub ddlCrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrOtherHead.SelectedIndex > 0 Then
                ddlCrOtherGL.DataSource = objAsstTrn.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrOtherHead.SelectedIndex)
                ddlCrOtherGL.DataTextField = "GlDesc"
                ddlCrOtherGL.DataValueField = "gl_Id"
                ddlCrOtherGL.DataBind()
                ddlCrOtherGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrOtherHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadSubGL()
        Try
            ddlCrOtherSubGL.DataSource = objAsstTrn.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCrOtherSubGL.DataTextField = "GlDesc"
            ddlCrOtherSubGL.DataValueField = "gl_Id"
            ddlCrOtherSubGL.DataBind()
            ddlCrOtherSubGL.Items.Insert(0, "Select SubGL Code")

            ddlDbOtherSubGL.DataSource = objAsstTrn.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlDbOtherSubGL.DataTextField = "GlDesc"
            ddlDbOtherSubGL.DataValueField = "gl_Id"
            ddlDbOtherSubGL.DataBind()
            ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubGL")
        End Try
    End Sub
    Private Sub ddlDrOtherHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDrOtherHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDrOtherHead.SelectedIndex > 0 Then
                ddlDbOtherGL.DataSource = objAsstTrn.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDrOtherHead.SelectedIndex)
                ddlDbOtherGL.DataTextField = "GlDesc"
                ddlDbOtherGL.DataValueField = "gl_Id"
                ddlDbOtherGL.DataBind()
                ddlDbOtherGL.Items.Insert(0, "Select GL Code")
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDrOtherHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnDADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dDebit As Double = 0
        Dim dtDetails As New DataTable
        Try
            If ddlDbOtherSubGL.Items.Count > 1 Then
                If ddlDbOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Debit."
                    Exit Sub
                End If
            End If

            If IsNothing(Session("dtFixedAssetTrn")) Then
                dtFixedAssetTrn = dtDetails
            Else
                dtFixedAssetTrn = Session("dtFixedAssetTrn")
            End If

            dtCOA = objAsstTrn.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlDbOtherGL.SelectedIndex > 0 Then
                iGL = ddlDbOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlDbOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlDbOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherDAmount.Text <> "" Then
                dDebit = txtOtherDAmount.Text
            Else
                dDebit = 0.00
            End If

            dtFixedAssetTrn = objAsstTrn.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDrOtherHead.SelectedIndex, iGL, iSubGL, dDebit, 1, dtFixedAssetTrn, dtCOA)
            Session("dtFixedAssetTrn") = dtFixedAssetTrn
            dgPaymentDetails.DataSource = dtFixedAssetTrn
            dgPaymentDetails.DataBind()

            LoadSubGL()
            ddlDrOtherHead.SelectedIndex = 0 : ddlDbOtherGL.Items.Clear() : ddlDbOtherSubGL.SelectedIndex = 0 : txtOtherDAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDADD_Click")
        End Try
    End Sub
    Protected Sub dgPaymentDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPaymentDetails.ItemCommand
        Dim dt As New DataTable
        Dim lblId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "DELETE" Then

                If lblstatus.Text = "Activated" Then
                    lblError.Text = "This Payment has been Approved, you can not delete transactions."
                    Exit Sub
                End If

                dt = Session("dtFixedAssetTrn")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                If dt.Rows.Count > 0 Then
                    Session("dtFixedAssetTrn") = dt
                Else
                    Session("dtFixedAssetTrn") = Nothing
                End If
            End If

            dgPaymentDetails.DataSource = dt
            dgPaymentDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPaymentDetails_ItemCommand")
        End Try
    End Sub
    Private Sub dgPaymentDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPaymentDetails.ItemDataBound
        Dim imgbtnDelete1 As New ImageButton, imgbtnEdit As New ImageButton
        Try
            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete1 = CType(e.Item.FindControl("imgbtnDelete1"), ImageButton)
                imgbtnDelete1.ImageUrl = "~/Images/Trash16.png"

                If lblstatus.Text = "Waiting for Approval" Then
                    imgbtnDelete1.Enabled = True
                Else
                    imgbtnDelete1.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPaymentDetails_ItemDataBound")
        End Try
    End Sub

    Private Sub imgbtnOtherCADD_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnOtherCADD.Click
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Dim dtCOA As New DataTable
        Dim dCredit As Double = 0
        Dim dtDetails As New DataTable
        Try

            If ddlCrOtherSubGL.Items.Count > 1 Then
                If ddlCrOtherSubGL.SelectedIndex > 0 Then
                Else
                    lblError.Text = "Select the Sub General Ledger for Credit."
                    Exit Sub
                End If
            End If

            If IsNothing(Session("dtFixedAssetTrn")) Then
                dtFixedAssetTrn = dtDetails
            Else

                dtFixedAssetTrn = Session("dtFixedAssetTrn")
            End If

            dtCOA = objAsstTrn.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

            'Debit
            If ddlCrOtherGL.SelectedIndex > 0 Then
                iGL = ddlCrOtherGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlCrOtherSubGL.SelectedIndex > 0 Then
                iSubGL = ddlCrOtherSubGL.SelectedValue
            Else
                iSubGL = 0
            End If

            If txtOtherCAmount.Text <> "" Then
                dCredit = txtOtherCAmount.Text
            Else
                dCredit = 0.00
            End If
            dtFixedAssetTrn = objAsstTrn.LoadPaymentsMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrOtherHead.SelectedIndex, iGL, iSubGL, dCredit, 2, dtFixedAssetTrn, dtCOA)
            Session("dtAssetPayment") = dtFixedAssetTrn
            dgPaymentDetails.DataSource = dtFixedAssetTrn
            dgPaymentDetails.DataBind()

            LoadSubGL()
            ddlCrOtherHead.SelectedIndex = 0 : ddlCrOtherGL.Items.Clear() : ddlCrOtherSubGL.SelectedIndex = 0 : txtOtherCAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnOtherCADD_Click")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objAsstTrn.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "Select Region")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objAsstTrn.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "Select Area")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAsstTrn.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub drpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpAstype.SelectedIndexChanged
        Dim iCount As Integer
        Dim AssetLen As String
        Dim ilen As Integer : Dim increment As Integer = 0
        Try
            If drpAstype.SelectedIndex > 0 Then
                loadExistingItemCode()
                iCount = objAsstTrn.GetGLID(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, sSession.YearID)
                txtbxDscrptn.Text = "" : txtbxItmDecrtn.Text = "" : txtAstNOSup.Text = "" : txtbxQty.Text = "" : txtbxDteofPurchase.Text = "" : txtbxDteCmmunictn.Text = "" : txtbxamount.Text = ""
                If iCount > 0 Then
                    AssetLen = objAsstTrn.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue)
                    txtAssetNo.Text = AssetLen & iCount.ToString()
                Else
                    AssetLen = objAsstTrn.LoadAssetNo(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue)
                    ilen = AssetLen.Length
                    If ilen = 9 Then
                        increment = increment + 1
                        txtAssetNo.Text = AssetLen & increment.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "drpAstype_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnsave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnsave.Click
        Dim Arr As Array
        Dim iMasterID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0, dSum As Double = 0, dSDebit As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dGridDebit As Double = 0 : Dim dGridCredit As Double = 0
        Try
            lblError.Text = ""
            If rboNew.Checked = False And rboOld.Checked = False Then
                lblError.Text = "Select New or Old"
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtbxDteofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Purchase (" & txtbxDteofPurchase.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteofPurchase.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtbxDteofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date Of Purchase (" & txtbxDteofPurchase.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteofPurchase.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Commission (" & txtbxDteCmmunictn.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Commission (" & txtbxDteCmmunictn.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Commission (" & txtbxDteCmmunictn.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtDtAddtn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Commission (" & txtDtAddtn.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtDtAddtn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Commission (" & txtDtAddtn.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblAssetAdditionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If
            If dgPaymentDetails.Items.Count = 0 Then
                lblError.Text = "Add Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
                Exit Sub
            End If

            For i = 0 To dgPaymentDetails.Items.Count - 1
                dGridDebit = dGridDebit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                dGridCredit = dGridCredit + Convert.ToDouble(dgPaymentDetails.Items(i).Cells(10).Text)
            Next
            If dGridDebit <> dGridCredit Then
                lblError.Text = "Debit And Credit Amount not matching."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit And Credit Amount not matching','', 'success');", True)
                Exit Sub
            End If
            For i = 0 To dgPaymentDetails.Items.Count - 1
                dSDebit = Convert.ToDouble(dgPaymentDetails.Items(i).Cells(9).Text)
                dSum = dSum + dSDebit
            Next

            If ddlExtTrnNo.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_ID = ddlExtTrnNo.SelectedValue
            Else
                objAsstTrn.iAFAA_ID = 0
            End If

            If rboNew.Checked = True Then
                objAsstTrn.sAFAA_AddnType = "N"
            ElseIf rboOld.Checked = True Then
                objAsstTrn.sAFAA_AddnType = "O"
            Else
                objAsstTrn.sAFAA_AddnType = ""
            End If

            objAsstTrn.sAFAA_DelnType = ""

            If ddlAssetTrnfr.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_AssetTrType = ddlAssetTrnfr.SelectedIndex
            Else
                objAsstTrn.iAFAA_AssetTrType = 0
            End If
            If ddlCurencyType.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_CurrencyType = ddlCurencyType.SelectedValue
            Else
                objAsstTrn.iAFAA_CurrencyType = 0
            End If
            If txtCurency.Text <> "" Then
                objAsstTrn.dAFAA_CurrencyAmnt = txtCurency.Text
            Else
                objAsstTrn.dAFAA_CurrencyAmnt = "0.00"
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_Zone = ddlAccZone.SelectedValue
            Else
                objAsstTrn.iAFAA_Zone = 0
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_Region = ddlAccRgn.SelectedValue
            Else
                objAsstTrn.iAFAA_Region = 0
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_Area = ddlAccArea.SelectedValue
            Else
                objAsstTrn.iAFAA_Area = 0
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_Branch = ddlAccBrnch.SelectedValue
            Else
                objAsstTrn.iAFAA_Branch = 0
            End If
            If txtLocID.Text <> "" Then
                objAsstTrn.sAFAA_ActualLocn = txtLocID.Text
            Else
                objAsstTrn.sAFAA_ActualLocn = "0.00"
            End If
            If ddlSupplier.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_SupplierName = ddlSupplier.SelectedValue
            Else
                objAsstTrn.iAFAA_SupplierName = 0
            End If
            'If txtSprCode.Text <> "" Then
            '    objAsstTrn.iAFAA_SupplierCode = txtSprCode.Text
            'Else
            '    objAsstTrn.iAFAA_SupplierCode = 0
            'End If
            If txtSprCode.Text <> "" Then
                objAsstTrn.iAFAA_SupplierCode = ddlSupplier.SelectedValue
            Else
                objAsstTrn.iAFAA_SupplierCode = 0
            End If
            If ddlTrTypes.SelectedIndex > 0 Then
                objAsstTrn.iAFAA_TrType = ddlTrTypes.SelectedIndex
            Else
                objAsstTrn.iAFAA_TrType = 0
            End If

            If drpAstype.SelectedIndex > 0 Then
                objAsstTrn.sAFAA_AssetType = drpAstype.SelectedValue
            Else
                objAsstTrn.sAFAA_AssetType = 0
            End If

            If txtAssetNo.Text <> "" Then
                objAsstTrn.sAFAA_AssetNo = txtAssetNo.Text
            Else
                objAsstTrn.sAFAA_AssetNo = ""
            End If

            If txtAstNOSup.Text <> "" Then
                objAsstTrn.sAFAA_AssetRefNo = txtAstNOSup.Text
            Else
                objAsstTrn.sAFAA_AssetRefNo = ""
            End If

            If txtbxDscrptn.Text <> "" Then
                objAsstTrn.sAFAA_Description = txtbxDscrptn.Text
            Else
                objAsstTrn.sAFAA_Description = ""
            End If

            If txtbxItmCode.Text <> "" Then
                objAsstTrn.sAFAA_ItemCode = txtbxItmCode.Text
            Else
                objAsstTrn.sAFAA_ItemCode = ""
            End If

            If txtbxItmDecrtn.Text <> "" Then
                objAsstTrn.sAFAA_ItemDescription = txtbxItmDecrtn.Text
            Else
                objAsstTrn.sAFAA_ItemDescription = ""
            End If
            If txtbxQty.Text <> "" Then
                objAsstTrn.iAFAA_Quantity = txtbxQty.Text
            Else
                objAsstTrn.iAFAA_Quantity = 0
            End If

            If txtbxDteofPurchase.Text <> "" Then
                objAsstTrn.dAFAA_PurchaseDate = Date.ParseExact(txtbxDteofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAsstTrn.dAFAA_PurchaseDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtbxDteCmmunictn.Text <> "" Then
                objAsstTrn.dAFAA_CommissionDate = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAsstTrn.dAFAA_CommissionDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtDtAddtn.Text <> "" Then
                objAsstTrn.dAFAA_AddtnDate = Date.ParseExact(txtDtAddtn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAsstTrn.dAFAA_AddtnDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtbxAstAge.Text <> "" Then
                    objAsstTrn.dAFAA_AssetAge = txtbxAstAge.Text
                Else
                    objAsstTrn.dAFAA_AssetAge = "0"
            End If
            If txtbxamount.Text <> "" Then
                objAsstTrn.dAFAA_AssetAmount = txtbxamount.Text
            Else
                objAsstTrn.dAFAA_AssetAmount = ""
            End If
            objAsstTrn.iAFAA_AssetDelID = 0
            objAsstTrn.dAFAA_AssetDelDate = Nothing
            objAsstTrn.dAFAA_AssetDeletionDate = Nothing
            objAsstTrn.dAFAA_Assetvalue = "0.00"
            objAsstTrn.sAFAA_AssetDesc = ""
            objAsstTrn.iAFAA_CreatedBy = sSession.UserID
            objAsstTrn.dAFAA_CreatedOn = DateTime.Today
            objAsstTrn.iAFAA_UpdatedBy = sSession.UserID
            objAsstTrn.dAFAA_UpdatedOn = DateTime.Today
            objAsstTrn.sAFAA_Delflag = "W"
            objAsstTrn.sAFAA_Status = "C"
            objAsstTrn.sAFAA_Operation = "U"
            objAsstTrn.sAFAA_IPAddress = sSession.IPAddress
            objAsstTrn.iAFAA_YearID = sSession.YearID
            objAsstTrn.iAFAA_CompID = sSession.AccessCodeID
            objAsstTrn.dAFAA_Depreciation = txtDeprcn.Text

            Arr = objAsstTrn.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objAsstTrn)
            iMasterID = Arr(1)

            If dgPaymentDetails.Items.Count > 0 Then
                For i = 0 To dgPaymentDetails.Items.Count - 1

                    objAsstTrn.iFXATD_TrType = 10
                    objAsstTrn.iFXATD_BillId = iMasterID
                    objAsstTrn.iFXATD_PaymentType = 0
                    objAsstTrn.iFXATD_DbOrCr = dgPaymentDetails.Items(i).Cells(12).Text
                    objAsstTrn.iFXATD_Head = dgPaymentDetails.Items(i).Cells(1).Text
                    objAsstTrn.iFXATD_GL = dgPaymentDetails.Items(i).Cells(2).Text
                    objAsstTrn.iFXATD_SubGL = dgPaymentDetails.Items(i).Cells(3).Text
                    If objAsstTrn.iFXATD_DbOrCr = 1 Then
                        dDebit = dgPaymentDetails.Items(i).Cells(9).Text
                        objAsstTrn.dFXATD_Debit = dDebit
                        objAsstTrn.dFXATD_Credit = 0.00
                    Else
                        dCredit = dgPaymentDetails.Items(i).Cells(10).Text
                        objAsstTrn.dFXATD_Debit = 0.00
                        objAsstTrn.dFXATD_Credit = dCredit
                    End If

                    objAsstTrn.iFXATD_CreatedBy = sSession.UserID
                    objAsstTrn.sFXATD_Status = "A"
                    objAsstTrn.iFXATD_YearID = sSession.YearID
                    objAsstTrn.sFXATD_Operation = "C"
                    objAsstTrn.sFXATD_IPAddress = sSession.IPAddress
                    objAsstTrn.iFXATD_CompID = sSession.AccessCodeID
                    'objAsstTrn.dFXATD_TransactionDate = txtbxDteofPurchase.Text
                    Arr = objAsstTrn.SaveFixedAssetTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, 0, objAsstTrn)
                Next
            End If
            'Dim AFAMID As Integer = 0
            'AFAMID = objAsstTrn.GetAFAMID(sSession.AccessCode, sSession.AccessCodeID, txtbxItmCode.SelectedValue)
            '  objAsstTrn.UpdateFixedMasterLoan(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtloanWhome.Text, txtloanAmount.Text, txtloanAgrmnt.Text, txtloandate.Text, ddlCurrencytypeloan.SelectedValue, txtLoanExcngDate.Text, txtbxItmCode.SelectedValue)
            If Arr(0) = "2" Then
                lblAssetAdditionValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                imgbtnsave.ImageUrl = "~/Images/Add24.png"
            ElseIf Arr(0) = "3" Then
                lblAssetAdditionValidationMsg.Text = "Successfully Saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
            End If
            loadExistingTRnNo()
            ddlExtTrnNo.SelectedValue = iMasterID
            ddlExtTrnNo_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnsave_Click")
        End Try
    End Sub
    Private Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSupplier.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlSupplier.SelectedIndex > 0 Then
                txtSprCode.Text = objAsstTrn.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSupplier_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlAssetTrnfr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetTrnfr.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlAssetTrnfr.SelectedIndex = 1 Then
                ddlCurencyType.Enabled = False
                txtCurency.Enabled = False
            Else
                ddlCurencyType.Enabled = True
                txtCurency.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssetTrnfr_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
    '    Dim ds As New DataSet
    '    Try
    '        lblError.Text = ""
    '        dgAttach.CurrentPageIndex = 0
    '        ds = objAsstTrn.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
    '        If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
    '            dgAttach.AllowPaging = True
    '        Else
    '            dgAttach.AllowPaging = False
    '        End If
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            divcollapseAttachments.Visible = True
    '        Else
    '            divcollapseAttachments.Visible = False
    '        End If
    '        dgAttach.PageSize = 1000
    '        dgAttach.DataSource = ds
    '        dgAttach.DataBind()
    '        lblBadgeCount.Text = dgAttach.Items.Count
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachments")
    '    End Try
    'End Sub
    'Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
    '    Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
    '    Dim dRow As DataRow
    '    Dim sFilesNames As String
    '    Dim i As Integer = 0
    '    Dim sTempPath As String = ""
    '    Dim lSize As Long
    '    Dim dt As New DataTable
    '    Try
    '        lblMsg.Text = ""
    '        dtAttach.Columns.Add("FilePath")
    '        dtAttach.Columns.Add("FileName")
    '        lblError.Text = "" : iDocID = 0

    '        Dim hfc As HttpFileCollection = Request.Files
    '        If hfc.Count > 0 Then
    '            For i = 0 To hfc.Count - 1
    '                Dim hpf As HttpPostedFile = hfc(i)
    '                If hpf.ContentLength > 0 Then
    '                    lSize = CType(hpf.ContentLength, Integer)
    '                    If (sSession.FileSize * 1024 * 1024) < lSize Then
    '                        lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
    '                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
    '                        Exit Sub
    '                    End If
    '                    dRow = dtAttach.NewRow()
    '                    sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                    sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")

    '                    If sTempPath.EndsWith("\") = True Then
    '                        sTempPath = sTempPath & "Temp\Attachment\"
    '                    Else
    '                        sTempPath = sTempPath & "Temp\Attachment\"
    '                    End If

    '                    objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sTempPath)
    '                    hpf.SaveAs(sTempPath & sFilesNames)
    '                    dRow("FilePath") = sTempPath & sFilesNames
    '                    dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) & "." & System.IO.Path.GetExtension(hpf.FileName)
    '                    If System.IO.File.Exists(dRow("FilePath")) = True Then
    '                        iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, dRow("FilePath"), sSession.UserID, iAttachID)
    '                        If iAttachID > 0 Then
    '                            BindAllAttachments(sSession.AccessCode, iAttachID)
    '                        End If
    '                    Else
    '                        lblMsg.Text = "No file to Attach."
    '                    End If
    '                    dtAttach.Rows.Add(dRow)
    '                End If
    '            Next
    '        End If
    '        If dtAttach.Rows.Count = 0 Then
    '            lblMsg.Text = "No file to Attach."
    '        End If

    '        dtAttach = dt
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAttch_Click")
    '    End Try
    'End Sub
    'Public Sub ViewContent(ByVal sPath As String, ByVal sExtn As String)
    '    Try
    '        If UCase(sExtn) = "JPG" Or UCase(sExtn) = "PNG" Or UCase(sExtn) = "JPEG" Or UCase(sExtn) = "GIF" Or UCase(sExtn) = "BMP" Then
    '            Dim bytes As Byte() = System.IO.File.ReadAllBytes(sPath)
    '            Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '            Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '            imgView.ImageUrl = imageDataURL1
    '        Else
    '            imgView.ImageUrl = "~/Images/NoImage.jpg"
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ViewContent")
    '    End Try
    'End Sub
    'Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
    '    Dim file As System.IO.FileInfo
    '    Try
    '        file = New System.IO.FileInfo(pstrFileNameAndPath)
    '        If file.Exists Then
    '            Response.Clear()
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
    '            Response.AddHeader("Content-Length", file.Length.ToString())
    '            Response.ContentType = "application/octet-stream"
    '            Response.WriteFile(file.FullName)
    '            Response.End()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile")
    '    End Try
    'End Sub
    'Private Sub dgAttach_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAttach.ItemCommand
    '    Dim sPaths As String, sDestFilePath As String
    '    Dim lblAtchDocID As New Label, lblFDescription As New Label
    '    Dim sExtn As String = ""
    '    Try
    '        lblError.Text = "" : lblMsg.Text = ""
    '        If e.CommandName = "OPENPAGE" Or e.CommandName = "VIEW" Then
    '            lblAtchDocID = e.Item.FindControl("lblAtchDocID")
    '            iDocID = Val(lblAtchDocID.Text)
    '            sPaths = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
    '            If sPaths.EndsWith("\") = True Then
    '                sPaths = sPaths & "Temp\Attachment\"
    '            Else
    '                sPaths = sPaths & "\Temp\Attachment\"
    '            End If
    '            If e.CommandName = "VIEW" Then
    '                Dim oImgFilePath As New Object, oDocumentID As New Object, oFileID As New Object, oInwrdID As Object, oStatus As Object, oBackToFormID As Object

    '                sDestFilePath = objclsAttachments.GetOriginalDocumentPathNew(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
    '                oImgFilePath = HttpUtility.UrlEncode(objGen.EncryptQueryString(sDestFilePath))
    '                oDocumentID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iAttachID))
    '                oFileID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iDocID))
    '                oStatus = HttpUtility.UrlDecode(objGen.EncryptQueryString(iStatus))
    '                oBackToFormID = HttpUtility.UrlDecode(objGen.EncryptQueryString(2))

    '                sExtn = objAsstTrn.GetExtension(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
    '                ViewContent(sDestFilePath, sExtn)
    '            ElseIf e.CommandName = "OPENPAGE" Then
    '                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
    '                DownloadMyFile(sDestFilePath)
    '            End If
    '        End If
    '        If e.CommandName = "REMOVE" Then
    '            txtDescription.Text = ""
    '            lblAtchDocID = e.Item.FindControl("lblAtchDocID")
    '            iDocID = Val(lblAtchDocID.Text)
    '            objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
    '            BindAllAttachments(sSession.AccessCode, iAttachID)
    '        End If
    '        If e.CommandName = "ADDDESC" Then
    '            lblAtchDocID = e.Item.FindControl("lblAtchDocID")
    '            iDocID = Val(lblAtchDocID.Text)
    '            lblFDescription = e.Item.FindControl("lblFDescription")
    '            lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
    '            txtDescription.Text = lblFDescription.Text
    '            txtDescription.Focus()
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand")
    '    End Try
    'End Sub
    'Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
    '    Dim lblExt As New Label, lblFile As New Label
    '    Dim File As New LinkButton
    '    Dim imgbtnView As New ImageButton, imgbtnAdd As New ImageButton, imgbtnDownload As New ImageButton, imgbtnRemove As New ImageButton
    '    Try
    '        If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
    '            imgbtnView = CType(e.Item.FindControl("imgbtnView"), ImageButton)
    '            imgbtnView.ImageUrl = "~/Images/View16.png"
    '            imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
    '            imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
    '            imgbtnDownload = CType(e.Item.FindControl("imgbtnDownload"), ImageButton)
    '            imgbtnDownload.ImageUrl = "~/Images/Download16.png"
    '            imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
    '            imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
    '            lblFile = CType(e.Item.FindControl("lblFile"), Label)
    '            File = CType(e.Item.FindControl("File"), LinkButton)
    '            lblExt = CType(e.Item.FindControl("lblExt"), Label)
    '            lblExt.Text = UCase(lblExt.Text)

    '            dgAttach.Columns(4).Visible = False : dgAttach.Columns(6).Visible = False : dgAttach.Columns(7).Visible = False

    '            If sINWDownload = "YES" Then
    '                dgAttach.Columns(6).Visible = True
    '            End If

    '            If sINWView = "YES" Then
    '                dgAttach.Columns(4).Visible = True
    '            End If

    '            If sWFDelete = "YES" Then
    '                dgAttach.Columns(7).Visible = True
    '            End If

    '            If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "BRK" Or lblExt.Text = "CAL" Or lblExt.Text = "PDF" Or
    '                lblExt.Text = "CLP" Or lblExt.Text = "DCX" Or lblExt.Text = "EPS" Or lblExt.Text = "ICO" Or lblExt.Text = "IFF" Or lblExt.Text = "IMT" Or
    '                lblExt.Text = "ICA" Or lblExt.Text = "PCT" Or lblExt.Text = "PCX" Or lblExt.Text = "PNG" Or lblExt.Text = "PSD" Or lblExt.Text = "RAS" Or
    '                lblExt.Text = "SGI" Or lblExt.Text = "TGA" Or lblExt.Text = "XBM" Or lblExt.Text = "XPM" Or lblExt.Text = "XWD" Or lblExt.Text = "TIF" Or lblExt.Text = "TIFF" Or lblExt.Text = "TXT") Then
    '                imgbtnView.Enabled = True
    '                lblFile.Visible = True
    '                File.Visible = False
    '            Else
    '                imgbtnView.Enabled = False
    '                lblFile.Visible = False
    '                File.Visible = True
    '            End If

    '            If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "PNG") Then
    '                lblFile.Visible = True : File.Visible = False
    '            Else
    '                lblFile.Visible = False : File.Visible = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound")
    '    End Try
    'End Sub

    'Private Sub btnAddDesc_Click(sender As Object, e As EventArgs) Handles btnAddDesc.Click
    '    Try
    '        lblError.Text = "" : lblMsg.Text = ""
    '        If txtDescription.Text.Trim.Length > 1000 Then
    '            lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
    '            txtDescription.Focus()
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
    '            Exit Try
    '        End If
    '        objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
    '        lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
    '        iDocID = 0
    '        BindAllAttachments(sSession.AccessCode, iAttachID)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
    '    Catch ex As Exception
    '        lblMsg.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click")
    '    End Try
    'End Sub
    Private Sub ddlExtTrnNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExtTrnNo.SelectedIndexChanged
        Dim dt As New DataTable, dtTrans, dt1 As New DataTable
        Try
            If ddlExtTrnNo.SelectedIndex > 0 Then
                dt = objAsstTrn.showDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExtTrnNo.SelectedValue)
                irefnoid = Val(dt.Rows(0)("AFAA_ID"))
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If IsDBNull(dt.Rows(i)("AFAA_AssetTrType").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetTrType").ToString() = "" Then
                                ddlAssetTrnfr.SelectedIndex = 0
                            Else
                                ddlAssetTrnfr.SelectedIndex = dt.Rows(i)("AFAA_AssetTrType").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_CurrencyType").ToString()) = False Then
                            If dt.Rows(i)("AFAA_CurrencyType").ToString() = 0 Then
                                ddlCurencyType.SelectedIndex = 0
                            Else
                                ddlCurencyType.SelectedValue = dt.Rows(i)("AFAA_CurrencyType").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_CurrencyAmnt").ToString()) = False Then
                            If dt.Rows(i)("AFAA_CurrencyAmnt").ToString() = "" Then
                                txtCurency.Text = ""
                            Else
                                txtCurency.Text = dt.Rows(i)("AFAA_CurrencyAmnt").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AddtnDate").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AddtnDate").ToString() = "" Then
                                txtDtAddtn.Text = ""
                            Else
                                txtDtAddtn.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(i)("AFAA_AddtnDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_Zone").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Zone").ToString() = "" Then
                                ddlAccZone.SelectedValue = 0
                            Else
                                ddlAccZone.SelectedValue = dt.Rows(i)("AFAA_Zone").ToString()
                                LoadRegion(ddlAccZone.SelectedValue)
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_Region").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Region").ToString() = "" Then
                                ddlAccRgn.SelectedValue = 0
                            Else
                                ddlAccRgn.SelectedValue = dt.Rows(i)("AFAA_Region").ToString()
                                LoadArea(ddlAccRgn.SelectedValue)
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_Area").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Area").ToString() = "" Then
                                ddlAccArea.SelectedValue = 0
                            Else
                                ddlAccArea.SelectedValue = dt.Rows(i)("AFAA_Area").ToString()
                                LoadAccBrnch(ddlAccArea.SelectedValue)
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAA_Branch").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Branch").ToString() = "" Then
                                ddlAccBrnch.SelectedValue = 0
                            Else
                                ddlAccBrnch.SelectedValue = dt.Rows(i)("AFAA_Branch").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAA_ActualLocn").ToString()) = False Then
                            If dt.Rows(i)("AFAA_ActualLocn").ToString() = "" Then
                                txtLocID.Text = ""
                            Else
                                txtLocID.Text = dt.Rows(i)("AFAA_ActualLocn").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_SupplierName").ToString()) = False Then
                            If dt.Rows(i)("AFAA_SupplierName").ToString() = "" Then
                                ddlSupplier.SelectedValue = 0
                            Else
                                ddlSupplier.SelectedValue = dt.Rows(i)("AFAA_SupplierName").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_SupplierCode").ToString()) = False Then
                            If dt.Rows(i)("AFAA_SupplierCode").ToString() = "" Then
                                txtSprCode.Text = ""
                            Else
                                txtSprCode.Text = objAsstTrn.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)("AFAA_SupplierCode"))
                                'txtSprCode.Text = dt.Rows(i)("AFAA_SupplierCode").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_TrType").ToString()) = False Then
                            If dt.Rows(i)("AFAA_TrType").ToString() = "" Then
                                ddlTrTypes.SelectedIndex = 0
                            Else
                                ddlTrTypes.SelectedIndex = dt.Rows(i)("AFAA_TrType").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AssetType").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetType").ToString() = "" Then
                                drpAstype.SelectedValue = 0
                            Else
                                drpAstype.SelectedValue = dt.Rows(i)("AFAA_AssetType").ToString()
                                drpAstype_SelectedIndexChanged(sender, e)
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AssetNo").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetNo").ToString() = "" Then
                                txtAssetNo.Text = ""
                            Else
                                txtAssetNo.Text = dt.Rows(i)("AFAA_AssetNo").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AssetRefNo").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetRefNo").ToString() = "" Then
                                txtAstNOSup.Text = ""
                            Else
                                txtAstNOSup.Text = dt.Rows(i)("AFAA_AssetRefNo").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_Description").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Description").ToString() = "" Then
                                txtbxDscrptn.Text = ""
                            Else
                                txtbxDscrptn.Text = dt.Rows(i)("AFAA_Description").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAA_ItemCode").ToString()) = False Then
                            If dt.Rows(i)("AFAA_ItemCode").ToString() = "" Then
                                txtbxItmCode.Text = ""
                            Else
                                txtbxItmCode.SelectedValue = dt.Rows(i)("AFAA_ItemCode").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_ItemDescription").ToString()) = False Then
                            If dt.Rows(i)("AFAA_ItemDescription").ToString() = "" Then
                                txtbxItmDecrtn.Text = ""
                            Else
                                txtbxItmDecrtn.Text = dt.Rows(i)("AFAA_ItemDescription").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_Quantity").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Quantity").ToString() = "" Then
                                txtbxQty.Text = ""
                            Else
                                txtbxQty.Text = dt.Rows(i)("AFAA_Quantity").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_PurchaseDate").ToString()) = False Then
                            If dt.Rows(i)("AFAA_PurchaseDate").ToString() = "" Then
                                txtbxDteofPurchase.Text = ""
                            Else
                                txtbxDteofPurchase.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(i)("AFAA_PurchaseDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_CommissionDate").ToString()) = False Then
                            If dt.Rows(i)("AFAA_CommissionDate").ToString() = "" Then
                                txtbxDteCmmunictn.Text = ""
                            Else

                                txtbxDteCmmunictn.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(i)("AFAA_CommissionDate").ToString(), "D")
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AssetAge").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetAge").ToString() = "" Then
                                txtbxAstAge.Text = ""
                            Else
                                txtbxAstAge.Text = dt.Rows(i)("AFAA_AssetAge").ToString()
                            End If
                        End If
                        If IsDBNull(dt.Rows(i)("AFAA_AssetAmount").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AssetAmount").ToString() = "" Then
                                txtbxamount.Text = ""
                            Else
                                txtbxamount.Text = dt.Rows(i)("AFAA_AssetAmount").ToString()
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAA_Depreciation").ToString()) = False Then
                            If dt.Rows(i)("AFAA_Depreciation").ToString() = "" Then
                                txtDeprcn.Text = ""
                            Else
                                txtDeprcn.Text = dt.Rows(i)("AFAA_Depreciation")
                            End If
                        End If

                        If IsDBNull(dt.Rows(i)("AFAA_AddnType").ToString()) = False Then
                            If dt.Rows(i)("AFAA_AddnType").ToString() = "O" Then
                                rboOld.Checked = True
                            ElseIf dt.Rows(i)("AFAA_AddnType").ToString() = "N" Then
                                rboNew.Checked = True
                            End If
                        End If

                        If dt.Rows(i)("AFAA_Delflag") = "W" Then
                            lblstatus.Text = "Waiting For Approval"
                        ElseIf dt.Rows(i)("AFAA_Delflag") = "A" Then
                            lblstatus.Text = "Approved"
                        ElseIf dt.Rows(i)("AFAA_Delflag") = "X" Then
                            lblstatus.Text = "Transaction Deleted"
                            If dt.Rows(i)("AFAA_DelnType") = "FD" Or dt.Rows(i)("AFAA_AssetDelID") = 1 Or dt.Rows(i)("AFAA_AssetDelID") = 4 Then
                                ImgbtnActivate.Visible = False
                                ImgbtnActivate.Enabled = False
                            Else
                                ImgbtnActivate.Visible = True
                                ImgbtnActivate.Enabled = True
                            End If
                            imgbtnsave.Enabled = False ' : imgbtnAttachment.Enabled = False
                            'imgbtnAttach.Enabled = False
                            imgbtnRefresh.Enabled = False
                            imgbtnWaiting.Enabled = False
                        ElseIf dt.Rows(i)("AFAA_Delflag") = "Y" Then
                                lblstatus.Text = "Recalled for Approval"
                            imgbtnsave.Enabled = False
                            'imgbtnAttachment.Enabled = False : imgbtnAttach.Enabled = False
                            imgbtnRefresh.Enabled = False
                                ImgbtnActivate.Enabled = True
                                imgbtnWaiting.Enabled = True
                            ElseIf dt.Rows(i)("AFAA_Delflag") = "D" Then
                                lblstatus.Text = "Transaction Deleted"
                            imgbtnsave.Enabled = False ' : imgbtnAttachment.Enabled = False : imgbtnAttach.Enabled = False
                            imgbtnRefresh.Enabled = False
                            ImgbtnActivate.Visible = True
                            imgbtnWaiting.Enabled = False
                        End If
                        imgbtnAttachment.Enabled = True

                    Next
                    dtTrans = objAsstTrn.LoadSavedTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExtTrnNo.SelectedValue)
                    dgPaymentDetails.DataSource = dtTrans
                    dgPaymentDetails.DataBind()
                    Session("dtAssetPayment") = dtTrans
                    'Dim icabinetID, iSubCabinet, iFolder As Integer
                    'icabinetID = 0 : iSubCabinet = 0 : iFolder = 0
                    'icabinetID = objAsstTrn.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    'iSubCabinet = objAsstTrn.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Fixed Asset")
                    'Dim sTrnDesc As String = objAsstTrn.GetDesc(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedValue)
                    'iFolder = objAsstTrn.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, sTrnDesc)
                    'dt1 = objAsstTrn.showDetailsAttachment(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iFolder)
                    'lblBadgeCount.Text = dt1.Rows.Count
                    'gvattach1.DataSource = dt1
                    'gvattach1.DataBind()
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
                    'imgbtnAttach_Click(sender, e)
                    GetAttachFile(ddlExtTrnNo.SelectedItem.Text)
                    lblBadgeCount.Text = Convert.ToString(objAsstTrn.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedItem.Text))

                Else
                    lblAssetAdditionValidationMsg.Text = "No Data"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                    Exit Sub
                End If
            End If

            'dt = objAsstTrn.getSuppliersLedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedValue)
            'If dt.Rows.Count > 0 Then
            '    ddlDrOtherHead.SelectedIndex = objAsstTrn.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CSM_GL").ToString())
            '    ddlDrOtherHead_SelectedIndexChanged(sender, e)

            '    ddlDbOtherGL.SelectedValue = dt.Rows(0)("CSM_GL").ToString()
            '    ddlDbOtherGL_SelectedIndexChanged(sender, e)

            '    If dt.Rows(0)("CSM_SubGL").ToString() = "0" Then
            '        ddlDbOtherSubGL.SelectedIndex = -1
            '    Else
            '        ddlDbOtherSubGL.SelectedValue = dt.Rows(0)("CSM_SubGL").ToString()
            '    End If
            'End If

        Catch ex As Exception
            lblMsg.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExtTrnNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            lblError.Text = ""
            lblstatus.Text = Nothing
            ddlAssetTrnfr.SelectedIndex = 0 : ddlCurencyType.SelectedIndex = 0 : txtCurency.Text = "" : ddlAccZone.SelectedIndex = 0 : ddlExtTrnNo.SelectedIndex = 0
            ddlAccRgn.SelectedIndex = 0 : ddlAccArea.SelectedIndex = 0 : ddlAccBrnch.SelectedIndex = 0 : txtLocID.Text = "" : ddlSupplier.SelectedIndex = 0 : txtSprCode.Text = ""
            ddlTrTypes.SelectedIndex = 0 : drpAstype.SelectedIndex = 0 : txtAssetNo.Text = "" : txtAstNOSup.Text = "" : txtbxDscrptn.Text = "" : txtbxDscrptn.Text = ""
            txtbxItmCode.Text = "" : txtbxItmDecrtn.Text = "" : txtbxQty.Text = "" : txtbxDteofPurchase.Text = "" : txtbxDteCmmunictn.Text = "" : txtbxAstAge.Text = "" : txtbxamount.Text = ""
            ddlDrOtherHead.SelectedIndex = 0 : ddlDbOtherGL.SelectedIndex = -1 : ddlDbOtherSubGL.SelectedIndex = 0 : txtOtherDAmount.Text = ""
            ddlCrOtherHead.SelectedIndex = 0 : ddlCrOtherGL.SelectedIndex = -1 : ddlCrOtherSubGL.SelectedIndex = 0 : txtOtherCAmount.Text = ""
            gvattach.DataSource = Nothing : gvattach.DataBind()
            Session("dtAssetPayment") = Nothing : Session("dtFixedAssetTrn") = Nothing
            dgPaymentDetails.DataSource = Nothing : dgPaymentDetails.DataBind()
            rboNew.Checked = False : rboOld.Checked = False
        Catch ex As Exception
            lblMsg.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sStatus As String = ""
        Dim iAppBy As Integer
        Try
            iAppBy = sSession.UserID
            If ddlExtTrnNo.SelectedIndex > 0 Then
                sStatus = objAsstTrn.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExtTrnNo.SelectedValue)
                If sStatus = "A" Then
                    lblError.Text = "This Transaction Already Approved." : lblAssetAdditionValidationMsg.Text = "This Transaction Already Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                ElseIf sStatus = "W" Then
                    objAsstTrn.StatusCheck(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExtTrnNo.SelectedValue, "A", "A", iAppBy)
                    lblAssetAdditionValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                ElseIf sStatus = "Y" Then
                    objAsstTrn.StatusCheck(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExtTrnNo.SelectedValue, "AR", "A", iAppBy)
                    lblAssetAdditionValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                End If
            Else
                lblError.Text = "Select Existing Transaction No."
            End If
        Catch ex As Exception
            lblMsg.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    'Private Sub btnAddAttch1_Click(sender As Object, e As EventArgs) Handles btnAddAttch1.Click
    '    Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
    '    Dim dRow As DataRow
    '    Dim sFilesNames As String
    '    Dim i As Integer = 0
    '    Dim hfc As HttpFileCollection = Request.Files

    '    Try
    '        lblError.Text = "" : iDocID = 0
    '        If hfc.Count > 0 Then
    '            For i = 0 To hfc.Count - 1
    '                Dim hpf As HttpPostedFile = hfc(i)
    '                If hpf.ContentLength > 0 Then
    '                    dRow = dt.NewRow()
    '                    sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                    dt = Session("Attachment1")
    '                    If dt.Rows.Count = 0 Then
    '                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                        hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
    '                        dRow = dt.NewRow()
    '                        dRow("FilePath1") = Server.MapPath(".") & "/Images/" & sFilesNames
    '                        dRow("FileName1") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                        dRow("Extension1") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedOn1") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)

    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "FileName1 Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment1") = dt
    '                    ElseIf dt.Rows.Count > 0 Then
    '                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                        hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
    '                        dRow = dt.NewRow()
    '                        dRow("FilePath1") = Server.MapPath(".") & "/Images/" & sFilesNames
    '                        dRow("FileName1") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                        dRow("Extension1") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedOn1") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)
    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "FileName1 Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment1") = dt
    '                    End If
    '                End If
    '            Next
    '        End If
    '        If dt.Rows.Count = 0 Then
    '            lblError.Text = "No file to Attach."
    '        End If

    '        Session("Attachment1") = dt
    '        gvattach1.DataSource = dt
    '        gvattach1.DataBind()
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub chkSelectAll_CheckedChanged1(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        lblError.Text = ""
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To gvattach1.Rows.Count - 1
    '                chkField = gvattach1.Rows(iIndx).FindControl("chkSelect1")
    '                chkField.Checked = True
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
    '            Next
    '        Else
    '            For iIndx = 0 To gvattach1.Rows.Count - 1
    '                chkField = gvattach1.Rows(iIndx).FindControl("chkSelect1")
    '                chkField.Checked = False
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
    '            Next
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
    '    End Try
    'End Sub
    'Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
    '    Dim sImagePath As String
    '    Dim sExt As String
    '    Try
    '        sExt = System.IO.Path.GetExtension(sFilePath)
    '        If sFileInDB = "FALSE" Then
    '            sImagePath = objAsstTrn.GetImagePath(sSession.AccessCode)
    '            sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
    '            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
    '            sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
    '            If System.IO.File.Exists(sImagePath) = False Then
    '                FileCopy(sFilePath, sImagePath)
    '                FilePageInEdict = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FilePageInEdict")
    '    End Try
    'End Function
    'Private Sub gvattach1_PreRender(sender As Object, e As EventArgs) Handles gvattach1.PreRender
    '    Try
    '        If gvattach1.Rows.Count > 0 Then
    '            gvattach1.UseAccessibleHeader = True
    '            gvattach1.HeaderRow.TableSection = TableRowSection.TableHeader
    '            gvattach1.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach1_PreRender")
    '    End Try
    'End Sub
    'Private Sub btnRemoteIndex_Click(sender As Object, e As EventArgs) Handles btnRemoteIndex.Click
    '    Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0
    '    Dim iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
    '    Dim chkSelect As New CheckBox
    '    Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
    '    Dim Arr() As String
    '    Dim txtKeywords As New TextBox, txtValues As New TextBox
    '    Dim lblPath As New Label, lblDescriptorID As New Label
    '    Try

    '        If (ddlAccBrnch.SelectedIndex = 0 Or ddlAccBrnch.SelectedValue = "") Then
    '            lblError.Text = "Select Branch." : lblAssetAdditionValidationMsg.Text = "Select Branch."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
    '            ddlAccBrnch.Focus()
    '            Exit Sub
    '        Else
    '            icabinetID = objAsstTrn.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
    '        End If


    '        iSubCabinet = objAsstTrn.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Fixed Asset")

    '        If ddlExtTrnNo.SelectedIndex = 0 Then
    '            lblError.Text = "Select Transaction No." : lblAssetAdditionValidationMsg.Text = "Select Transaction No.."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
    '            ddlAccBrnch.Focus()
    '            Exit Sub
    '        Else

    '            Dim sTrnDesc As String = objAsstTrn.GetDesc(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedValue)
    '            iFolder = objAsstTrn.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, sTrnDesc)
    '        End If

    '        If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 Then
    '            If gvattach1.Rows.Count > 0 Then
    '                For i = 0 To gvattach1.Rows.Count - 1
    '                    chkSelect = gvattach1.Rows(i).FindControl("chkSelect1")
    '                    lblPath = gvattach1.Rows(i).FindControl("lblPath1")
    '                    If chkSelect.Checked = True Then
    '                        sPageExt = UCase(gvattach1.Rows(i).Cells(3).Text)
    '                        sFilePath = lblPath.Text
    '                        sFileName = gvattach1.Rows(i).Cells(2).Text
    '                        objAsstTrn.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
    '                        objAsstTrn.iPGEFOLDER = iFolder
    '                        objAsstTrn.iPGECABINET = icabinetID
    '                        objAsstTrn.iPGEDOCUMENTTYPE = 0
    '                        'objAsstTrn.sPGETITLE = ""
    '                        objAsstTrn.sPGETITLE = txtAstNOSup.Text + "" + "Attachment"
    '                        objAsstTrn.dPGEDATE = Date.Today
    '                        If iPageDetailsid = 0 Then
    '                            iPageDetailsid = objAsstTrn.iPGEBASENAME
    '                            objAsstTrn.iPgeDETAILSID = iPageDetailsid
    '                        End If
    '                        objAsstTrn.iPgeCreatedBy = sSession.UserID
    '                        objAsstTrn.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
    '                        objAsstTrn.sPGEEXT = sPageExt
    '                        objAsstTrn.sPGEKeyWORD = ""
    '                        objAsstTrn.sPGEOCRText = ""
    '                        objAsstTrn.iPGESIZE = 0
    '                        objAsstTrn.iPGECURRENT_VER = 0
    '                        Select Case UCase(sPageExt)
    '                            Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
    '                                objAsstTrn.sPGEOBJECT = "IMAGE"
    '                            Case Else
    '                                objAsstTrn.sPGEOBJECT = "OLE"
    '                        End Select
    '                        objAsstTrn.sPGESTATUS = "A"
    '                        objAsstTrn.iPGESubCabinet = iSubCabinet
    '                        objAsstTrn.iPgeUpdatedBy = sSession.UserID

    '                        objAsstTrn.spgeDelflag = "A"
    '                        objAsstTrn.iPGEQCUsrGrpId = 0
    '                        objAsstTrn.sPGEFTPStatus = "F"
    '                        objAsstTrn.iPGEbatchname = objAsstTrn.iPGEBASENAME
    '                        objAsstTrn.spgeOrignalFileName = objclsFASGeneral.SafeSQL(sFileName)
    '                        objAsstTrn.iPGEBatchID = 0
    '                        objAsstTrn.iPGEOCRDelFlag = 0
    '                        objAsstTrn.iPgeCompID = sSession.AccessCodeID
    '                        Arr = objAsstTrn.SavePage(sSession.AccessCode, sSession.AccessCodeID, objAsstTrn)
    '                        sISDB = objAsstTrn.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
    '                        FilePageInEdict(objAsstTrn.iPGEBASENAME, sFilePath, UCase(sISDB))
    '                        objAsstTrn.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objAsstTrn.iPGEBASENAME, iPageID)
    '                    Else
    '                        lblError.Text = "Select the checkBox."
    '                        Exit Sub
    '                    End If
    '                Next
    '                If Arr(0) = "3" Then
    '                    lblError.Text = "Successfully Indexed." : lblAssetAdditionValidationMsg.Text = "Successfully Indexed."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
    '                    gvattach1.DataSource = Nothing
    '                    gvattach1.DataBind()
    '                    gvattach1.Visible = False
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnRemoteIndex_Click")
    '    End Try
    'End Sub
    Private Sub ddlDbOtherGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDbOtherGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlDbOtherGL.SelectedIndex > 0 Then
                ddlDbOtherSubGL.DataSource = objAsstTrn.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlDbOtherGL.SelectedValue)
                ddlDbOtherSubGL.DataTextField = "GlDesc"
                ddlDbOtherSubGL.DataValueField = "gl_Id"
                ddlDbOtherSubGL.DataBind()
                ddlDbOtherSubGL.Items.Insert(0, "Select SubGL Code")
            Else
                ddlDbOtherSubGL.DataSource = dt
                ddlDbOtherSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDbOtherGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ImgBtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/FixedAsset/AssetAdditionDashBoard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click")
        End Try
    End Sub
    Private Sub ImgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnActivate.Click
        Try
            ActivateStatus(ddlExtTrnNo.SelectedValue, "R")
            ddlExtTrnNo_SelectedIndexChanged(sender, e)
            lblAssetAdditionValidationMsg.Text = "Reactivated Sucessfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
            ImgbtnActivate.Visible = False
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnActivate_Click")
        End Try
    End Sub
    Private Sub ActivateStatus(ByVal iTrnId As Integer, ByVal sStatus As String)
        Try
            objAsstTrn.UpdateDeletionStatus(sSession.AccessCode, sSession.AccessCodeID, iTrnId, sStatus)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ActivateStatus")
        End Try
    End Sub
    'Private Sub imgbtnAttach_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAttach.Click
    '    Dim dt As New DataTable
    '    Dim icabinetID, iFolder, iSubCabinet As Integer
    '    Try
    '        If ddlExtTrnNo.SelectedIndex > 0 Then
    '            icabinetID = 0 : iSubCabinet = 0 : iFolder = 0
    '            icabinetID = objAsstTrn.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
    '            iSubCabinet = objAsstTrn.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Fixed Asset")
    '            Dim sTrnDesc As String = objAsstTrn.GetDesc(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedValue)
    '            iFolder = objAsstTrn.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, sTrnDesc)


    '            dt = objAsstTrn.showDetailsAttachment(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iFolder)
    '            lblBadgeCount.Text = dt.Rows.Count
    '            gvattach1.DataSource = dt
    '            gvattach1.DataBind()
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
    '        ElseIf ddlExtTrnNo.SelectedIndex = 0 Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttch').modal('show');", True)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAttach_Click")
    '    End Try
    'End Sub
    Private Sub btnUpdatePhyvrn_Click(sender As Object, e As EventArgs) Handles btnUpdatePhyvrn.Click
        Try
            lblError.Text = ""
            objAsstTrn.UpdatePhysicalverificationdtls(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtVrfdby.Text, txtVerfiedon.Text, txtappedby.Text, txtapprovedon.Text, txtvrfremark.Text, txtAppremarks.Text, ddlExtTrnNo.SelectedValue)
            lblError.Text = "Successfully Verified." : lblAssetAdditionValidationMsg.Text = "Successfully Verified."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdatePhyvrn_Click")
        End Try
    End Sub
    Private Sub AdditionalDtls_Click(sender As Object, e As EventArgs) Handles AdditionalDtls.Click
        Dim sAssetRefNo As String = ""
        Try
            sAssetRefNo = HttpUtility.UrlEncode(objGen.EncryptQueryString(irefnoid))
            Response.Redirect(String.Format("~/FixedAsset/AssetAddlnDtls.aspx?AssetRefNo={0}", sAssetRefNo), False)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AdditionalDtls_Click")
        End Try
    End Sub

    Protected Sub txtbxItmCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtbxItmCode.SelectedIndexChanged
        Dim dt As New DataTable, dt1 As New DataTable
        Try

            If txtbxItmCode.SelectedValue <> "Select ItemCode" Then
                dt = objAsstTrn.GetItemDescription(sSession.AccessCode, sSession.AccessCodeID, txtbxItmCode.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("AFAM_Description").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Description").ToString() = "" Then
                            txtbxDscrptn.Text = ""
                        Else
                            txtbxDscrptn.Text = dt.Rows(0)("AFAM_Description").ToString()
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_ItemDescription").ToString()) = False Then
                        If dt.Rows(0)("AFAM_ItemDescription").ToString() = "" Then
                            txtbxItmDecrtn.Text = ""
                        Else
                            txtbxItmDecrtn.Text = dt.Rows(0)("AFAM_ItemDescription").ToString()
                        End If
                    End If
                    Dim delQty As Double = 0
                    delQty = objAsstTrn.getDelQty(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, txtbxItmCode.SelectedValue)

                    If IsDBNull(dt.Rows(0)("AFAM_Quantity").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Quantity").ToString() = "" Then
                            txtbxQty.Text = ""
                        Else
                            If delQty = 0 Then
                                txtbxQty.Text = dt.Rows(0)("AFAM_Quantity").ToString()
                            Else
                                txtbxQty.Text = delQty
                            End If
                        End If
                        End If
                    If IsDBNull(dt.Rows(0)("AFAM_Purchasedate").ToString()) = False Then
                        If dt.Rows(0)("AFAM_Purchasedate").ToString() = "" Then
                            txtbxDteofPurchase.Text = ""
                        Else
                            txtbxDteofPurchase.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("AFAM_Purchasedate").ToString(), "D")
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_CommissionDate").ToString()) = False Then
                        If dt.Rows(0)("AFAM_CommissionDate").ToString() = "" Then
                            txtbxDteCmmunictn.Text = ""
                        Else
                            txtbxDteCmmunictn.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("AFAM_CommissionDate").ToString(), "D")
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("AFAM_PurchaseAmount").ToString()) = False Then
                        If dt.Rows(0)("AFAM_PurchaseAmount").ToString() = "" Then
                            txtbxamount.Text = ""
                        Else
                            txtbxamount.Text = dt.Rows(0)("AFAM_PurchaseAmount").ToString()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "drpAstype_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub lnkBtnPrvsTrans_Click(sender As Object, e As EventArgs) Handles lnkBtnPrvsTrans.Click
        Dim dt As New DataTable
        Dim dTotalDebit, dTotalCredit As Double
        Try
            If drpAstype.SelectedIndex > 0 And txtbxItmCode.Text <> "" Then
                dt = objAsstTrn.LoadPrevTransDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtbxItmCode.SelectedValue, drpAstype.SelectedValue)
                If dt.Columns.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dTotalDebit = dTotalDebit + dt.Rows(i)("Debit")
                        dTotalCredit = dTotalDebit + dt.Rows(i)("Credit")
                    Next
                End If
                dgPrevTransDetails.DataSource = dt
                dgPrevTransDetails.DataBind()
            Else
                lblError.Text = "Select Asset type and Item code"
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnPrvsTrans_Click")
        End Try
    End Sub
    'Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
    '    Dim objBatch As clsIndexing.BatchScan
    '    Dim Arr() As String
    '    Try
    '        If gvattach.Rows.Count > 0 Then
    '            AutomaticIndexing()
    '            GetAttachFile(ddlExtTrnNo.SelectedItem.Text)
    '            gvattach.Visible = True
    '            '  gvattach.DataBind()
    '            lblBadgeCount.Text = Convert.ToString(objAsstTrn.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedItem.Text))
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
    '        Else
    '            lblError.Text = "Add the files before index"
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub AutomaticIndexing()
    '    Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
    '    Dim chkSelect As New CheckBox
    '    Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
    '    Dim Arr() As String
    '    Dim dDate As Date
    '    Dim txtKeywords As New TextBox, txtValues As New TextBox
    '    Dim lblPath As New Label, lblDescriptorID As New Label
    '    'Dim iCabinet As Integer
    '    'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
    '    Dim bCheckCabinet As Boolean

    '    Try
    '        If ddlExtTrnNo.SelectedIndex = 0 Then
    '            lblError.Text = "Select Asset No."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
    '            ddlAssetNo.Focus()
    '            Exit Sub
    '        Else
    '            icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlRefno.SelectedItem.Text)
    '        End If

    '        iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Asset Additional Details")

    '        If ddlRefno.SelectedIndex = 0 Then
    '            lblError.Text = "Select Asset Reference Code."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
    '            ddlRefno.Focus()
    '            Exit Sub

    '        Else
    '            iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlAssetNo.SelectedItem.Text)
    '        End If

    '        iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

    '        'If ddlType.SelectedIndex = 0 Then
    '        '    lblModelError.Text = "Select Type."
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
    '        '    ddlType.Focus()
    '        '    Exit Sub
    '        'Else
    '        '    iType = ddlType.SelectedValue
    '        'End If

    '        If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
    '            If gvattach.Rows.Count > 0 Then
    '                For i = 0 To gvattach.Rows.Count - 1
    '                    iPageDetailsid = 0
    '                    chkSelect = gvattach.Rows(i).FindControl("chkSelect")
    '                    lblPath = gvattach.Rows(i).FindControl("lblPath")
    '                    If chkSelect.Checked = True Then
    '                        sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
    '                        sFilePath = lblPath.Text
    '                        sFileName = gvattach.Rows(i).Cells(2).Text
    '                        objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
    '                        objIndex.iPGEFOLDER = iFolder
    '                        objIndex.iPGECABINET = icabinetID
    '                        objIndex.iPGEDOCUMENTTYPE = iType
    '                        objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
    '                        dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                        objIndex.dPGEDATE = dDate
    '                        If iPageDetailsid = 0 Then
    '                            iPageDetailsid = objIndex.iPGEBASENAME
    '                            objIndex.iPgeDETAILSID = iPageDetailsid
    '                        End If
    '                        objIndex.iPgeCreatedBy = sSession.UserID
    '                        objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
    '                        objIndex.sPGEEXT = sPageExt
    '                        If gvKeywords.Rows.Count > 0 Then

    '                            For k = 0 To gvKeywords.Rows.Count - 1
    '                                txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
    '                                If txtKeywords.Text <> "" Then
    '                                    sKeywords = sKeywords & "," & txtKeywords.Text
    '                                End If
    '                            Next
    '                        End If
    '                        If sKeywords.StartsWith(",") = True Then
    '                            sKeywords = sKeywords.Remove(0, 1)
    '                        End If
    '                        If sKeywords.EndsWith(",") = True Then
    '                            sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
    '                        End If
    '                        objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
    '                        objIndex.sPGEOCRText = ""
    '                        objIndex.iPGESIZE = 0
    '                        objIndex.iPGECURRENT_VER = 0
    '                        Select Case UCase(sPageExt)
    '                            Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
    '                                objIndex.sPGEOBJECT = "IMAGE"
    '                            Case Else
    '                                objIndex.sPGEOBJECT = "OLE"
    '                        End Select
    '                        objIndex.sPGESTATUS = "A"
    '                        objIndex.iPGESubCabinet = iSubCabinet
    '                        objIndex.iPgeUpdatedBy = sSession.UserID

    '                        objIndex.spgeDelflag = "A"
    '                        objIndex.iPGEQCUsrGrpId = 0
    '                        objIndex.sPGEFTPStatus = "F"
    '                        objIndex.iPGEbatchname = objIndex.iPGEBASENAME
    '                        objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
    '                        objIndex.iPGEBatchID = 0
    '                        objIndex.iPGEOCRDelFlag = 0
    '                        objIndex.iPgeCompID = sSession.AccessCodeID
    '                        Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
    '                        sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
    '                        FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
    '                        objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

    '                        If gvDocumentType.Rows.Count > 0 Then
    '                            For j = 0 To gvDocumentType.Rows.Count - 1
    '                                lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
    '                                txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
    '                                If objIndex.iPGEBASENAME = iPageDetailsid Then
    '                                    objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
    '                                End If
    '                            Next
    '                        End If
    '                    End If
    '                Next

    '                If Arr(0) = "3" Then
    '                    lblError.Text = "Successfully Indexed."
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

    '                    gvattach.DataSource = Nothing
    '                    gvattach.DataBind()
    '                    gvattach.Visible = False
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
    '    Dim sImagePath As String
    '    Dim sExt As String
    '    Try
    '        sExt = System.IO.Path.GetExtension(sFilePath)
    '        If sFileInDB = "FALSE" Then
    '            sImagePath = objIndex.GetImagePath(sSession.AccessCode)
    '            sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
    '            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
    '            sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
    '            If System.IO.File.Exists(sImagePath) = False Then
    '                FileCopy(sFilePath, sImagePath)
    '                FilePageInEdict = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
    '    Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
    '    Dim dRow As DataRow
    '    Dim sFilesNames As String
    '    Dim i As Integer = 0
    '    Try
    '        lblError.Text = "" : iDocID = 0

    '        If ddlAssetNo.SelectedIndex > 0 Then
    '        Else
    '            lblError.Text = "Select Asset Item Code."
    '            ddlAssetNo.Focus()
    '            Exit Sub
    '        End If

    '        Dim hfc As HttpFileCollection = Request.Files

    '        If hfc.Count > 0 Then
    '            For i = 0 To hfc.Count - 1
    '                Dim hpf As HttpPostedFile = hfc(i)
    '                If hpf.ContentLength > 0 Then
    '                    dRow = dt.NewRow()
    '                    sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                    dt = Session("Attachment")
    '                    If dt.Rows.Count = 0 Then
    '                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                        hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
    '                        dRow = dt.NewRow()
    '                        dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
    '                        dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                        dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)

    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "FileName Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment") = dt
    '                    ElseIf dt.Rows.Count > 0 Then
    '                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                        hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
    '                        dRow = dt.NewRow()
    '                        dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
    '                        dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                        dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
    '                        dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
    '                        dt.Rows.Add(dRow)
    '                        Dim dvAttach As New DataView(dt)
    '                        dvAttach.Sort = "FileName Desc"
    '                        dt = dvAttach.ToTable
    '                        Session("Attachment") = dt
    '                    End If
    '                End If
    '            Next
    '        End If

    '        If dt.Rows.Count = 0 Then
    '            lblError.Text = "No file to Attach."
    '        End If

    '        Session("Attachment") = dt
    '        gvattach.DataSource = dt
    '        gvattach.DataBind()

    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
    '    Try
    '        If gvattach.Rows.Count > 0 Then
    '            gvattach.UseAccessibleHeader = True
    '            gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
    '            gvattach.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Public Sub GetAttachFile(ByVal sTrNo As String)
    '    Dim dRow As DataRow
    '    Dim dt, dt1 As New DataTable
    '    Try
    '        dt.Columns.Add("FilePath")
    '        dt.Columns.Add("FileName")
    '        dt.Columns.Add("Extension")
    '        dt.Columns.Add("CreatedOn")

    '        dt1 = objAsstAddnDtls.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                dRow("FilePath") = ""
    '                dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
    '                dRow("Extension") = dt1.Rows(i)("pge_ext")
    '                dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
    '                dt.Rows.Add(dRow)
    '            Next
    '        End If

    '        gvattach.DataSource = dt
    '        gvattach.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
    '    Dim iCabinetID, iSubCabinetID, iFolderID As Integer
    '    Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
    '    Dim sSelectedChecksIDs As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        If ddlAssetNo.SelectedIndex > 0 Then
    '            If gvattach.Rows.Count > 0 Then
    '                iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAssetNo.SelectedItem.Text)
    '                iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Asset Additional Details")
    '                iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlRefno.SelectedItem.Text)

    '                dt = objAsstAddnDtls.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
    '                If dt.Rows.Count > 0 Then
    '                    For i = 0 To dt.Rows.Count - 1
    '                        sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
    '                    Next
    '                End If

    '                If sSelectedChecksIDs.StartsWith(",") Then
    '                    sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
    '                End If
    '                If sSelectedChecksIDs.EndsWith(",") Then
    '                    sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
    '                End If

    '                oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
    '                oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
    '                oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
    '                oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
    '                oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

    '                Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
    '            Else
    '                lblError.Text = "No Attachments to view"
    '                Exit Sub
    '            End If
    '        Else
    '            lblError.Text = "Select Existing Asset Item COde No"
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To gvattach.Rows.Count - 1
    '                chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To gvattach.Rows.Count - 1
    '                chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
    '    Catch ex As Exception
    '    End Try
    'End Sub




    '''New attach codes
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                GetAttachFile(ddlExtTrnNo.SelectedItem.Text)
                gvattach.Visible = True
                '  gvattach.DataBind()
                lblBadgeCount.Text = Convert.ToString(objAsstTrn.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlExtTrnNo.SelectedItem.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean

        Try
            If ddlExtTrnNo.SelectedIndex = 0 Then
                lblError.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExtTrnNo.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, txtAstNOSup.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Asset Addition")

            If ddlExtTrnNo.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Reference Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExtTrnNo.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlExtTrnNo.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        iPageDetailsid = 0
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblError.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            If ddlExtTrnNo.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Asset Item Code."
                ddlExtTrnNo.Focus()
                Exit Sub
            End If

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GetAttachFile(ByVal sTrNo As String)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objAsstTrn.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlExtTrnNo.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, txtAstNOSup.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Asset Addition")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlExtTrnNo.SelectedItem.Text)

                    dt = objAsstTrn.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                        Next
                    End If

                    If sSelectedChecksIDs.StartsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    If sSelectedChecksIDs.EndsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                    End If

                    oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
                    oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
                    oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

                    Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
                Else
                    lblError.Text = "No Attachments to view"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Asset Item COde No"
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
        End Try
    End Sub
End Class
