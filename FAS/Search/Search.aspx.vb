Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports System.Net
Partial Class Search_Search
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Search.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsSearch As New clsSearch
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsView As New clsView

    Private Shared sSession As AllSession
    Private Shared dtCab As New DataTable
    Private Shared dtSubCab As New DataTable
    Private Shared dtFolder As New DataTable
    Private Shared dtDocType As New DataTable
    Private Shared dtKeyWord As New DataTable
    Private Shared dtDesc As New DataTable
    Private Shared dtFormat As New DataTable
    Private Shared dtUsers As New DataTable
    Private Shared dtParam As New DataTable
    Private Shared dtSearch As New DataTable
    Private Shared dtSearchResult As New DataTable
    Private Shared dtCopyofParam As New DataTable
    Private Shared sSelId As String
    Private Shared sSelName As String
    Private Shared iRet As Integer
    Private Shared sSelectedChecksIDs As String = ""
    Private Shared bCondation As Boolean = False
    Private Shared sDetailsId As String = ""
    Private Shared iColId As Integer
    Private Shared sFOLDER As String = ""

    Private Shared iFolID As Integer = 0
    Private Shared iSelectedIndexID As Integer = 0
    Private Shared sSelectedCabID As String = ""
    Private Shared sSelectedSubCabID As String = ""
    Private Shared sSelectedFolID As String = ""
    Private Shared sSelectedDocTypeID As String = ""
    Private Shared sSelectedKWID As String = ""
    Private Shared sSelectedDescID As String = ""
    Private Shared sSelectedFrmtID As String = ""
    Private Shared sSelectedCrByID As String = ""
    Private objclsModulePermission As New clsModulePermission
    Private Shared sPTAoD As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iCollationID As Integer = 0, iImageViewID As Integer = 0, iSearchIndexID As Integer = 0
        Dim sDescIDs As String = "", sDescNames As String = "", sFOLDERName As String = ""
        Dim sCABINET As String = "", sSUBCABINET As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SRCH")
                sPTAoD = "NO" : sPTAP = "NO" : sPTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SearchPermission.aspx", False) 'Permissions/SearchPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                        imgbtnView.Visible = False : sSelectedChecksIDs = ""
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPTAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sPTED = "YES"
                    End If
                End If


                dtParam = objclsSearch.GetFolderNames()
                dtCopyofParam = dtParam.Copy
                dtParam = objclsSearch.SetRows(dtParam, 14)
                dgParam.DataSource = dtParam
                dgParam.DataBind()
                ddlIndex.SelectedValue = 4
                ddlIndex_SelectedIndexChanged(sender, e)

                'BindSelectedDataToGrid()
                If Request.QueryString("PGE_CABINET") IsNot Nothing Then
                    sSUBCABINET = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PGE_CABINET")))
                End If

                If Request.QueryString("PGE_FOLDER") IsNot Nothing Then
                    sFOLDER = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PGE_FOLDER")))
                    sFOLDERName = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PGE_FOLDER")))
                    iFolID = 1
                    ddlIndex.SelectedValue = 3
                    ddlIndex_SelectedIndexChanged(sender, e)
                    sSelId = sFOLDER : sSelName = sFOLDERName
                    BindDescIDNameToParamGridFromView()
                    btnAddQuery_Click(sender, e)
                    iFolID = 0
                End If

                If Request.QueryString("SearchIndexID") IsNot Nothing Then
                    iSearchIndexID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SearchIndexID")))
                    ddlIndex.SelectedValue = iSearchIndexID
                    ddlIndex_SelectedIndexChanged(sender, e)
                End If

                If Request.QueryString("ImageViewID") IsNot Nothing Then
                    iImageViewID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ImageViewID")))
                    sSelectedChecksIDs = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedChecksIDs")))
                    sSelId = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
                    iSelectedIndexID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                    ddlIndex.SelectedValue = iSelectedIndexID
                    ddlIndex_SelectedIndexChanged(sender, e)
                    If iImageViewID = 2 Then
                        If sSession.dtDocoImageViewID.Rows.Count > 0 Then
                            dtSearch = sSession.dtDocoImageViewID.Copy
                            For i = 0 To dtSearch.Rows.Count - 1
                                ' sDescIDs = sDescIDs & "," & dtSearch.Rows(i)("DocTypeID")
                                sDescNames = sDescNames & "," & dtSearch.Rows(i)("Title")
                            Next
                            If sDescNames.StartsWith(",") Then
                                sDescNames = sDescNames.Remove(0, 1)
                            End If
                            If sDescNames.EndsWith(",") = True Then
                                sDescNames = sDescNames.Remove(Len(sSelId) - 1, 1)
                            End If
                            'sSelId = sDescIDs & ","
                            'If sSelId.StartsWith(",") Then
                            '    sSelId = sSelId.Remove(0, 1)
                            'End If
                            'If sSelId.EndsWith(",") = True Then
                            '    sSelId = sSelId.Remove(Len(sSelId) - 1, 1)
                            'End If
                            'sSelName = sDescNames & ","
                            'If sSelName.StartsWith(",") Then
                            '    sSelName = sSelName.Remove(0, 1)
                            'End If
                            'If sSelName.EndsWith(",") = True Then
                            '    sSelName = sSelName.Remove(Len(sSelName) - 1, 1)
                            'End If
                            BindDescIDNameToParamGridFromView()
                            btnSearchLinkMsgOk_Click(sender, e)
                        End If
                    End If
                Else
                    sSession.dtDocoImageViewID = Nothing
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub ddlIndex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIndex.SelectedIndexChanged
        Try
            lblError.Text = ""
            dgViewSearchData.DataSource = Nothing
            dgViewSearchData.DataBind()
            dgSelectedData.DataSource = Nothing
            dgSelectedData.DataBind()
            imgbtnView.Visible = False
            dtSearch.Clear()
            If ddlIndex.SelectedIndex > 0 Then
                BindSelectedDataToGrid()
            Else
                dtParam = objclsSearch.GetFolderNames()
                dtParam = objclsSearch.SetRows(dtParam, 14)
                dgParam.DataSource = dtParam
                dgParam.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlIndex_SelectedIndexChanged")
        End Try
    End Sub
    Private Function GetParameterName(ByVal iParamId As String, ByVal ScCellId As Integer, ByVal retCellId As Integer) As String
        Dim txtField As New TextBox
        Dim sStr As String = ""
        Try
            For i = 0 To dgParam.Rows.Count - 1
                If IsDBNull(dgParam.Rows(i).Cells(ScCellId).Text) = False Then
                    If (UCase(dgParam.Rows(i).Cells(ScCellId).Text) = iParamId) Then
                        If dgParam.ID = "dgParam" Then
                            If IsDBNull(dgParam.Rows(i).Cells(retCellId).Text) = False Then
                                Return dgParam.Rows(i).Cells(retCellId).Text
                            End If
                        End If
                        txtField = dgParam.Rows(i).FindControl("txtParam")
                        If Not txtField Is Nothing Then
                            Return txtField.Text = sStr
                        End If
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function LoadExistingParam(ByVal sParam As String, ByVal sParamType As String, ByVal iConNmeInd As Integer, ByVal iColIDInd As Integer) As Object
        Dim sArr() As String
        Dim i, j, a As Integer
        Dim dRow As DataGridItem
        Dim chkField As New CheckBox
        Try
            sArr = Split(sParam, ",")
            'If (sParamType = ddlIndex.SelectedItem.Text) Then
            '    For i = 0 To UBound(sArr)
            '        sArr(i) = Trim(sArr(i))
            '        sArr(i) = RTrim(sArr(i))

            '        For a = 0 To dgName.Items.Count - 1
            '            If (LTrim(RTrim(dgName.Items(a).Cells(iConNmeInd).Text)) = sArr(i)) Then
            '                j = InStr(sSelId & ";", ";" & dgName.Items(a).Cells(iColIDInd).Text & ";")
            '                If (j <= 0) Then
            '                    sSelName = sSelName & "," & dgName.Items(a).Cells(iConNmeInd).Text
            '                    sSelId = sSelId & ";" & dgName.Items(a).Cells(iColIDInd).Text
            '                End If
            '                chkField = dgName.Items(a).FindControl("chkSelect")
            '                chkField.Checked = True
            '            End If
            '        Next

            '        For Each dRow In dgName.Items
            '            If (LTrim(RTrim(dRow.Cells(iConNmeInd).Text)) = sArr(i)) Then
            '                j = InStr(sSelId & ";", ";" & dRow.Cells(iColIDInd).Text & ";")
            '                If (j <= 0) Then
            '                    sSelName = sSelName & "," & dRow.Cells(iConNmeInd).Text
            '                    sSelId = sSelId & ";" & dRow.Cells(iColIDInd).Text
            '                End If
            '                chkField = dRow.Cells(1).FindControl("chkSelect")
            '                chkField.Checked = True
            '            End If
            '        Next
            '    Next
            'Else
            '    For i = 0 To UBound(sArr)
            '        sArr(i) = Trim(sArr(i))
            '        sArr(i) = RTrim(sArr(i))
            '        For Each dRow In dgName.Items
            '            If dRow.Cells(iConNmeInd).Text.Trim = sArr(i) Then
            '                j = InStr(sSelId & ";", ";" & dRow.Cells(iColIDInd).Text & ";")
            '                If (j <= 0) Then
            '                    sSelName = sSelName & "," & dRow.Cells(iConNmeInd).Text
            '                    sSelId = sSelId & ";" & dRow.Cells(iColIDInd).Text
            '                End If
            '                chkField = dRow.Cells(1).FindControl("chkSelect")
            '                chkField.Checked = True
            '            End If
            '        Next
            '    Next
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CopyDataNewCol(ByVal RefDt As DataTable, ByVal ColName As String, ByVal ContColName As String) As DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn(ColName, GetType(String))
            RefDt.Columns.Add(dc)
            For Each dr As DataRow In RefDt.Rows
                dr.BeginEdit()
                dr(ColName) = dr(ContColName)
                dr.EndEdit()
                dr.AcceptChanges()
            Next
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub BindSelectedDataToGrid()
        Dim sParam As String = ""
        Try
            lblError.Text = ""
            If ddlIndex.SelectedIndex > 0 Then
                If ddlIndex.SelectedValue = 1 Then
                    LoadCabinets()
                ElseIf ddlIndex.SelectedValue = 2 Then
                    LoadSubCabinets()
                ElseIf ddlIndex.SelectedValue = 3 Then
                    LoadFolders()
                ElseIf ddlIndex.SelectedValue = 4 Then
                    LoadDocTypes()
                ElseIf ddlIndex.SelectedValue = 5 Then
                    LoadKeyWords()
                ElseIf ddlIndex.SelectedValue = 6 Then
                    LoadDescriptors()
                    LoadExistingDesc()
                ElseIf ddlIndex.SelectedValue = 7 Then
                    LoadFormat()
                ElseIf ddlIndex.SelectedValue = 8 Then
                    LoadUsers()
                End If
                If ddlIndex.SelectedValue <> 6 Then
                    sParam = GetParameterName(UCase(ddlIndex.SelectedItem.Text), 0, 1)
                    If (sParam.Length > 0) Then
                        LoadExistingParam(sParam, UCase(ddlIndex.SelectedItem.Text), 0, 2)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Cabinets
    Private Sub LoadCabinets()
        Try
            dtCab = objclsSearch.LoadCabinets(sSession.AccessCode, sSession.UserID, 0, "SRH")
            dtCab = CopyDataNewCol(dtCab, "OrgName", "CBN_Name")
            lstDesc.DataSource = dtCab
            lstDesc.DataTextField = "CBN_Name"
            lstDesc.DataValueField = "ID"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'SubCabinets
    Private Sub LoadSubCabinets()
        Try
            dtSubCab = objclsSearch.LoadSubCabinets(sSession.AccessCode, sSession.UserID, "SRH")
            If dtCab.Rows.Count > 0 Then
                dtCab = objclsSearch.LoadCabinets(sSession.AccessCode, sSession.UserID, 0, "SRH")
                dtCab = CopyDataNewCol(dtCab, "OrgName", "CBN_Name")
            End If
            dtSubCab = CopyDataNewCol(dtSubCab, "OrgName", "CBN_Name")
            lstDesc.DataSource = dtSubCab
            lstDesc.DataTextField = "CBN_Name"
            lstDesc.DataValueField = "ID"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Folders
    Private Sub LoadFolders()
        Try
            If dtCab.Rows.Count > 0 Then
                dtCab = objclsSearch.LoadCabinets(sSession.AccessCode, sSession.UserID, 0, "SRH")
                dtCab = CopyDataNewCol(dtCab, "OrgName", "CBN_NAME")
            End If
            If dtSubCab.Rows.Count > 0 Then
                dtSubCab = objclsSearch.LoadSubCabinets(sSession.AccessCode, sSession.UserID)
                dtSubCab = CopyDataNewCol(dtSubCab, "OrgName", "CBN_NAME")
            End If

            dtFolder = objclsSearch.LoadFolForSrh(sSession.AccessCode, sSession.UserID, sSession.AccessCodeID, "SRH")
            dtFolder = CopyDataNewCol(dtFolder, "OrgName", "FOL_Name")
            lstDesc.DataSource = AttachCabName(sSession.AccessCode, dtFolder, "FD")
            lstDesc.DataTextField = "FOL_Name"
            lstDesc.DataValueField = "ID"
            lstDesc.DataBind()
            For j = 0 To lstDesc.Items.Count - 1
                If sFOLDER.Contains(lstDesc.Items(j).Value) = True Then
                    lstDesc.Items(j).Selected = True
                End If
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function AttachCabName(ByVal sNameSpace As String, ByVal RefDt As DataTable, ByVal sCabSC As String) As DataTable
        Dim dRow As DataRow
        Try
            For Each dRow In RefDt.Rows
                dRow.BeginEdit()
                dRow("OrgName") = dRow("FOL_Name")
                dRow("FOL_Name") = dRow("FOL_Name") & " (" & GetCabOrSCName(dtCab, dtSubCab, dRow("CBN_Node"), "ID", "OrgName") & ")"
                dRow.EndEdit()
                dRow.AcceptChanges()
            Next
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetCabOrSCName(ByVal dtCab As DataTable, ByVal dtSCab As DataTable, ByVal iSUbOrCabId As Integer, ByVal sColID As String, ByVal sColName As String) As String
        Dim iRow, iTotal As Integer
        Dim iCab As Integer
        Dim sSCabName, sCabName As String
        Dim iFlag As Integer = 0
        Try
            For iRow = 0 To dtSCab.Rows.Count - 1
                If dtSCab.Rows(iRow).Item(sColID) = iSUbOrCabId Then
                    sSCabName = dtSCab.Rows(iRow).Item(sColName)
                    iCab = dtSCab.Rows(iRow).Item("CabPar")
                    For iTotal = 0 To dtCab.Rows.Count - 1
                        If iCab = dtCab.Rows(iTotal).Item(sColID) Then
                            sCabName = dtCab.Rows(iTotal).Item(sColName)
                        End If
                    Next
                    sCabName = sCabName & "|" & sSCabName
                    sCabName = sSCabName
                    Return sCabName
                    iFlag = 1
                End If
            Next
            If (iFlag = 0) Then
                For iRow = 0 To dtCab.Rows.Count - 1
                    If dtCab.Rows(iRow).Item(sColID) = iSUbOrCabId Then
                        Return dtCab.Rows(iRow).Item(sColName)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub BindDescIDNameToParamGrid()
        Try
            sSelId = "" : sSelName = ""
            For j = 0 To lstDesc.Items.Count - 1
                If lstDesc.Items(j).Selected = True Then
                    GetSelectedDescIDs()
                    GetSelectedDescNames()
                End If
                AddCritToGrid(ddlIndex.SelectedItem.Text, sSelName, sSelId)
            Next
            DeletePrevDesc()
            AddDocTypeDescriptors(sSelId)
            dtParam = objclsSearch.SetRows(dtParam, 18)
            dgParam.DataSource = dtParam
            dgParam.DataBind()
            LoadSearchDetails()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescIDNameToParamGridFromView()
        Try
            AddCritToGrid(ddlIndex.SelectedItem.Text, sSelName, sSelId)
            DeletePrevDesc()
            AddDocTypeDescriptors(sSelId)
            dtParam = objclsSearch.SetRows(dtParam, 18)
            dgParam.DataSource = dtParam
            dgParam.DataBind()
            LoadSearchDetails()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function GetSelectedDescIDs() As String
        Dim i As Integer
        Dim sDescIDs As String = ""
        Try
            For i = 0 To lstDesc.Items.Count - 1
                If lstDesc.Items(i).Selected = True Then
                    sDescIDs = sDescIDs & "," & lstDesc.Items(i).Value
                End If
            Next
            sSelId = sDescIDs & ","
            If sSelId.StartsWith(",") Then
                sSelId = sSelId.Remove(0, 1)
            End If
            If sSelId.EndsWith(",") = True Then
                sSelId = sSelId.Remove(Len(sSelId) - 1, 1)
            End If
            Return sSelId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetSelectedDescNames() As String
        Dim i As Integer
        Dim sDescIDs As String = "", sDescNames As String = ""
        Try
            For i = 0 To lstDesc.Items.Count - 1
                If lstDesc.Items(i).Selected = True Then
                    sDescNames = sDescNames & "," & lstDesc.Items(i).Text
                End If
            Next
            sSelName = sDescNames & ","
            If sSelName.StartsWith(",") Then
                sSelName = sSelName.Remove(0, 1)
            End If
            If sSelName.EndsWith(",") = True Then
                sSelName = sSelName.Remove(Len(sSelName) - 1, 1)
            End If
            Return sSelName
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Document Type
    Private Sub LoadDocTypes()
        Dim sFolID As String
        Try
            sFolID = objclsSearch.LoadIndexedFolders(sSession.AccessCode)
            If Not sFolID Is Nothing Then
                If (sFolID.Length > 0) Then
                    If (sFolID.Chars(0).ToString = ",") Then
                        sFolID = sFolID.Remove(0, 1)
                    End If
                    dtDocType = objclsSearch.LoadDocTypes(sSession.AccessCode, sSession.UserID, "SRH", sFolID)
                    dtDocType = CopyDataNewCol(dtDocType, "OrgName", "DOT_DOCName")
                    'dtDocType = AttachGrpName(dtDocType)
                    lstDesc.DataSource = dtDocType
                    lstDesc.DataTextField = "DOT_DOCName"
                    lstDesc.DataValueField = "DOT_DOCTYPEID"
                    lstDesc.DataBind()
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function AttachGrpName(ByVal dtName As DataTable) As DataTable
        Dim dRow As DataRow
        Try
            For Each dRow In dtName.Rows
                dRow.BeginEdit()
                If IsDBNull(dRow("OrgName")) = False Then
                    dRow("OrgName") = dRow("DOT_DOCName")
                    dRow("DOT_DOCName") = dRow("OrgName") & "(" & dRow("Mas_Description") & ")"
                    dRow.EndEdit()
                    dRow.AcceptChanges()
                End If
            Next
            Return dtName
        Catch ex As Exception
            Throw
        End Try
    End Function

    'KeyWords
    Private Sub LoadKeyWords()
        Try
            dtKeyWord = objclsSearch.LoadKeyWords(sSession.AccessCode)
            lstDesc.DataSource = dtKeyWord
            lstDesc.DataTextField = "Name"
            lstDesc.DataValueField = "Id"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Descriptors
    Private Sub LoadDescriptors()
        Try
            dtDesc = AddDescriptors()
            lstDesc.DataSource = dtDesc
            lstDesc.DataTextField = "Name"
            lstDesc.DataValueField = "Id"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function AddDescriptors(Optional ByVal sDocTypeID As String = "") As DataTable
        Dim dtdesc As DataTable
        Dim dRow, drIndexType As DataRow
        Try
            dtdesc = objclsSearch.GetDescUnion(sSession.AccessCode, sDocTypeID)
            If (sDocTypeID = "") Then
                Return dtdesc
            Else
                If (dtdesc.Rows.Count > 0) Then
                    For Each dRow In dtdesc.Rows
                        drIndexType = dtParam.NewRow
                        drIndexType("Id") = "ADC"
                        drIndexType("Fields") = "+" & dRow("Name")
                        drIndexType("SelectedID") = dRow("Id")
                        dtParam.Rows.Add(drIndexType)
                    Next
                End If
                dgParam.DataSource = dtParam
                dgParam.DataBind()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub LoadExistingDesc()
        Dim i, j As Integer
        Dim sDesc As String = ""
        Dim dRow As DataGridItem
        Dim chkField As New CheckBox
        Dim sArr() As String
        Try
            For i = 0 To dgParam.Rows.Count - 1
                If (IsDBNull(dgParam.Rows(i).Cells(0).Text) = False) Then
                    If (dgParam.Rows(i).Cells(0).Text = "ADC") Then
                        sDesc = sDesc & "," & dgParam.Rows(i).Cells(1).Text
                    End If
                End If
            Next
            If (sDesc.Length > 0) Then
                sArr = Split(sDesc, ",")
                For Each dRow In dgParam.Rows
                    For i = 0 To UBound(sArr)
                        If (dRow.Cells(2).Text = Val(sArr(i))) Then
                            j = InStr(sSelId & ";", ";" & dRow.Cells(2).Text & ";")
                            If (j <= 0) Then
                                sSelId = sSelId & ";" & dRow.Cells(2).Text
                                sSelName = sSelName & "," & dRow.Cells(0).Text
                            End If
                            chkField = dRow.Cells(1).FindControl("chkSelect")
                            chkField.Checked = True
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Format
    Private Sub LoadFormat()
        Try
            dtFormat = objclsSearch.LoadFormat(sSession.AccessCode)
            lstDesc.DataSource = dtFormat
            lstDesc.DataTextField = "Name"
            lstDesc.DataValueField = "Id"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Users
    Private Sub LoadUsers()
        Try
            dtUsers = objclsSearch.LoadUsers(sSession.AccessCode)
            lstDesc.DataSource = dtUsers
            lstDesc.DataTextField = "Name"
            lstDesc.DataValueField = "Id"
            lstDesc.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub dgParam_PreRender(sender As Object, e As EventArgs) Handles dgParam.PreRender
        Dim dt As New DataTable
        Try
            If dgParam.Rows.Count > 0 Then
                dgParam.UseAccessibleHeader = True
                dgParam.HeaderRow.TableSection = TableRowSection.TableHeader
                dgParam.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgParam_PreRender")
        End Try
    End Sub
    Private Sub dgSelectedData_PreRender(sender As Object, e As EventArgs) Handles dgSelectedData.PreRender
        Dim dt As New DataTable
        Try
            If dgSelectedData.Rows.Count > 0 Then
                dgSelectedData.UseAccessibleHeader = True
                dgSelectedData.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSelectedData.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSelectedData_PreRender")
        End Try
    End Sub

    Private Sub dgViewSearchData_PreRender(sender As Object, e As EventArgs) Handles dgViewSearchData.PreRender
        Dim dt As New DataTable
        Try
            If dgViewSearchData.Rows.Count > 0 Then
                dgViewSearchData.UseAccessibleHeader = True
                dgViewSearchData.HeaderRow.TableSection = TableRowSection.TableHeader
                dgViewSearchData.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewSearchData_PreRender")
        End Try
    End Sub

    'Private Sub dgCollation_PreRender(sender As Object, e As EventArgs) Handles dgCollation.PreRender
    '    Dim dt As New DataTable
    '    Try
    '        If dgCollation.Rows.Count > 0 Then
    '            dgCollation.UseAccessibleHeader = True
    '            dgCollation.HeaderRow.TableSection = TableRowSection.TableHeader
    '            dgCollation.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCollation_PreRender")
    '    End Try
    'End Sub
    Private Sub dgParam_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgParam.RowDataBound
        Dim txtField As New TextBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > -1 And e.Row.RowIndex > 0 And dgParam.Rows.Count > 0 Then
                    If IsDBNull(dtParam.Rows(e.Row.RowIndex)("Id")) = True Then
                        txtField = CType(e.Row.FindControl("txtParam"), TextBox)
                        txtField.Visible = False
                    ElseIf dtParam.Rows(e.Row.RowIndex - 1)("Id") = "CB" Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "SC" _
                        Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "FD" Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "DC" Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "FT" _
                        Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "DT" Or dtParam.Rows(e.Row.RowIndex - 1)("Id") = "CR" Then
                        txtField = CType(e.Row.FindControl("txtParam"), TextBox)
                        txtField.Enabled = False
                    ElseIf dtParam.Rows(e.Row.RowIndex - 1)("Id") = "DE" Then
                        txtField = dgParam.Rows(e.Row.RowIndex - 1).FindControl("txtParam")
                        txtField.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgParam_RowDataBound")
        End Try
    End Sub
    Public Sub AddCritToGrid(ByVal sCritName As String, ByVal sSelName As String, ByVal sSelIdL As String)
        Dim sName As String = ""
        Dim sCabNme As String, sSCNme As String, sID As String, sSCID As String
        Dim sArr() As String, sFol() As String
        Try
            sCabNme = "" : sSCNme = ""
            Select Case UCase(sCritName)
                Case "CABINETS"
                    dtParam.BeginInit()
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "CB") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
                Case "SUBCABINETS"
                    dtParam.BeginInit()
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "CB") Then
                                If (IsDBNull(dr("SelectedID")) = False And IsDBNull(dr("SelectedName")) = False) Then
                                    sID = dr("SelectedID").ToString
                                    sCabNme = dr("SelectedName")
                                End If
                            End If
                        End If
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "SC") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                sArr = Split(sSelIdL, ";")
                                For i = 1 To UBound(sArr)
                                    If (sArr(i).Length > 0) Then
                                        iRet = objclsSearch.GetCabOrSC(sSession.AccessCode, "SC", sArr(i), sSession.UserID, dtCab, dtSubCab, dtFolder)
                                        If (InStr(";" & sID & ";", ";" & iRet & ";") <= 0) Then
                                            sCabNme = sCabNme & "," & objclsSearch.GetTableContents(dtCab, "ID", "OrgName", iRet)
                                            sID = sID & ";" & iRet
                                        End If
                                        dr("SelectedName") = sName
                                        dr("SelectedID") = sSelIdL
                                        dtParam.EndInit()
                                        dtParam.AcceptChanges()
                                    End If
                                Next
                                If (sArr.Length <= 1) Then
                                    dr("SelectedName") = sName
                                    dr("SelectedID") = sSelIdL
                                    dtParam.EndInit()
                                    dtParam.AcceptChanges()
                                End If
                            End If
                        End If
                    Next
                    'Add Cabinet Name and ID for respective SubCabinets
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "CB") Then
                                If (sCabNme.Length > 0) Then
                                    If (sCabNme.Chars(0).ToString = ",") Then
                                        sCabNme = sCabNme.Remove(0, 1)
                                    End If
                                    dr.BeginEdit()
                                    dr("SelectedName") = sCabNme
                                    dr("SelectedID") = sID
                                    dr.EndEdit()
                                    dr.AcceptChanges()
                                End If
                            End If
                        End If
                    Next
                Case "FOLDERS"
                    dtParam.BeginInit()
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "CB") Then
                                If (IsDBNull(dr("SelectedID")) = False And IsDBNull(dr("SelectedName")) = False) Then
                                    sID = dr("SelectedID").ToString
                                    sCabNme = dr("SelectedName")
                                End If
                            End If
                            If (UCase(dr("ID")) = "SC") Then
                                If (IsDBNull(dr("SelectedID")) = False And IsDBNull(dr("SelectedName")) = False) Then
                                    sSCID = dr("SelectedID").ToString
                                    sSCNme = dr("SelectedName")
                                End If
                            End If
                            If (UCase(dr("ID")) = "FD") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                sArr = Split(sSelIdL, ";")
                                For i = 1 To UBound(sArr)
                                    If (sArr(i).Length > 0) Then
                                        iRet = objclsSearch.GetCabOrSC(sSession.AccessCode, "FD", sArr(i), sSession.UserID, dtCab, dtSubCab, dtFolder)
                                        sFol = Split(iRet, "|")
                                        If (sFol.Length > 1) Then
                                            'The folder is in sub cabinet and in turn cabinet
                                            If (InStr(";" & sSCID & ";", ";" & sFol(0) & ";") <= 0) Then
                                                sSCNme = sSCNme & "," & objclsSearch.GetTableContents(dtSubCab, "ID", "OrgName", sFol(0))
                                                sSCID = sSCID & ";" & sFol(0)
                                            End If
                                            If (InStr(";" & sID & ";", ";" & sFol(1) & ";") <= 0) Then
                                                sCabNme = sCabNme & "," & objclsSearch.GetTableContents(dtCab, "ID", "OrgName", sFol(1))
                                                sID = sID & ";" & sFol(1)
                                            End If
                                        Else
                                            'The folder is in cabinet
                                            If (sFol(0) = "NPC") Then
                                                lblSearchValidationMsg.Text = "The cabinet to which current folder belong do not have permissions to be searched." : lblError.Text = "The cabinet to which current folder belong do not have permissions to be searched."
                                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalSearchValidation').modal('show');", True)
                                            ElseIf (sFol(0) = "NPSC") Then
                                                lblSearchValidationMsg.Text = "The Sub Cabinet to which current folder belong do not have permissions to be searched." : lblError.Text = "The Sub Cabinet to which current folder belong do not have permissions to be searched."
                                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalSearchValidation').modal('show');", True)
                                            Else
                                                If (InStr(";" & sID & ";", ";" & sFol(0) & ";") <= 0) Then
                                                    sCabNme = sCabNme & "," & objclsSearch.GetTableContents(dtCab, "ID", "OrgName", sFol(0))
                                                    sID = sID & ";" & sFol(0)
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
                    For Each dr As DataRow In dtParam.Rows
                        'Add SubCabinet Name and ID for respective Folders
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "SC") Then
                                If (sSCNme.Length > 0) Then
                                    If (sSCNme.Chars(0).ToString = ",") Then
                                        sSCNme = sSCNme.Remove(0, 1)
                                    End If
                                    dr.BeginEdit()
                                    dr("SelectedName") = sSCNme
                                    dr("SelectedID") = sSCID
                                    dr.EndEdit()
                                    dr.AcceptChanges()
                                End If
                            End If
                            If (UCase(dr("ID")) = "CB") Then
                                If (sCabNme.Length > 0) Then
                                    If (sCabNme.Chars(0).ToString = ",") Then
                                        sCabNme = sCabNme.Remove(0, 1)
                                    End If
                                    dr.BeginEdit()
                                    dr("SelectedName") = sCabNme
                                    dr("SelectedID") = sID
                                    dr.EndEdit()
                                    dr.AcceptChanges()
                                End If
                            End If
                        End If
                    Next
                Case "DOCUMENTTYPES"
                    dtParam.BeginInit()
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "DT") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
                Case "KEYWORDS"
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "KW") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
                Case "DESCRIPTORS"
                    DeletePrevDesc()
                    Dim sArrNme(), sArrID() As String
                    Dim drCrit As DataRow
                    sArrNme = Split(sSelName, ",")
                    sArrID = Split(sSelIdL, ";")
                    For i = 1 To UBound(sArrID)
                        For Each drCrit In dtParam.Rows
                            If IsDBNull(drCrit("SelectedID")) = False Then
                                If (drCrit("SelectedID") = sArrID(i)) Then
                                    GoTo ForLP
                                End If
                            End If
                        Next
                        drCrit = dtParam.NewRow
                        drCrit("SelectedName") = "+" & objclsSearch.GetTableContents(dtDesc, "ID", "Name", sArrID(i))
                        drCrit("SelectedID") = sArrID(i)
                        drCrit("ID") = "ADC"
                        dtParam.Rows.Add(drCrit)
ForLP:              Next
                Case "FORMAT"
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "FT") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
                Case "CREATED BY"
                    For Each dr As DataRow In dtParam.Rows
                        If IsDBNull(dr("ID")) = False Then
                            If (UCase(dr("ID")) = "CR") Then
                                If (sSelName.Length > 0) Then
                                    If (sSelName.Chars(0).ToString = ",") Then
                                        sName = sSelName.Remove(0, 1)
                                    Else
                                        sName = sSelName
                                    End If
                                End If
                                dr("SelectedName") = sName
                                dr("SelectedID") = sSelIdL
                                dtParam.EndInit()
                                dtParam.AcceptChanges()
                            End If
                        End If
                    Next
            End Select
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub DeletePrevDesc()
        Dim RefCritdt As New DataTable
        Dim drCrit As DataRow
        Dim i, j As Integer
        Dim dc As DataColumn
        Try
            For i = 0 To dtParam.Columns.Count - 1
                dc = New DataColumn(dtParam.Columns(i).ColumnName, dtParam.Columns(i).DataType)
                RefCritdt.Columns.Add(dc)
            Next
            RefCritdt = CopyRowsToFirstDT(dtParam, RefCritdt)
            dtParam.Clear()
            For i = 0 To RefCritdt.Rows.Count - 1
                If (IsDBNull(RefCritdt.Rows(i).Item("Id")) = False) Then
                    If (RefCritdt.Rows(i).Item("Id") <> "ADC") Then
                        drCrit = dtParam.NewRow
                        For j = 0 To RefCritdt.Columns.Count - 1
                            drCrit(RefCritdt.Columns(j).ColumnName) = RefCritdt.Rows(i).Item(RefCritdt.Columns(j).ColumnName)
                        Next
                        dtParam.Rows.Add(drCrit)
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function AddDocTypeDescriptors(Optional ByVal sDocTypeID As String = "") As DataTable
        Dim dtdesc As DataTable
        Dim dRow, drCrit As DataRow
        Try
            dtdesc = objclsSearch.GetDescUnion(sSession.AccessCode, sDocTypeID)
            If (sDocTypeID = "") Then
                dgParam.DataSource = dtParam
                dgParam.DataBind()
                Return dtdesc
            Else
                If (dtdesc.Rows.Count > 0) Then
                    For Each dRow In dtdesc.Rows
                        drCrit = dtParam.NewRow
                        drCrit("Fields") = "+" & dRow("Name")
                        drCrit("SelectedID") = dRow("Id")
                        drCrit("Id") = "ADC"
                        dtParam.Rows.Add(drCrit)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CopyRowsToFirstDT(ByVal dt1 As DataTable, ByVal dt2 As DataTable) As DataTable
        Dim dr1, dr2 As DataRow
        Dim i As Integer
        Try
            For Each dr1 In dt1.Rows
                dr2 = dt2.NewRow
                For i = 0 To dt1.Columns.Count - 1
                    dr2(dt1.Columns(i).ColumnName) = dr1(dt1.Columns(i).ColumnName)
                Next
                dt2.Rows.Add(dr2)
            Next
            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnAddQuery_Click(sender As Object, e As EventArgs) Handles btnAddQuery.Click
        Dim sSrh As String
        Dim sArr() As String
        Dim i As Integer, iCheckSelected As Integer = 0
        Dim sCab As String = "", sSubCab As String = "", sFol As String = "", sDocType As String = "", sKW As String = "", sDesc As String = ""
        Dim sOCRTxt As String = "", sAnyDesc As String = "", sFrmt As String = "", sCrby As String = "", sTitle As String = ""
        Dim sFDate As String = "", sTDate As String = ""
        Dim txtField As New TextBox
        Try
            sSelName = String.Empty : sSelId = String.Empty : sSelectedChecksIDs = String.Empty
            sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
            sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
            sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty

            lblError.Text = ""
            dgViewSearchData.DataSource = Nothing
            dgViewSearchData.DataBind()
            dgViewSearchData.Visible = False
            BindDescIDNameToParamGrid()
            'Seggregate the indivisual contents 
            bCondation = False
            If Not ValidateSearchCrit() Then
                Exit Sub
            End If
            For iIndxGrd = 0 To dgParam.Rows.Count - 1
                If IsDBNull(dtParam.Rows(iIndxGrd)("ID")) = False Then
                    If dtParam.Rows(iIndxGrd)("Id") <> "DE" Then
                        If dtParam.Rows(iIndxGrd)("Id") = "CB" Or dtParam.Rows(iIndxGrd)("Id") = "SC" Or dtParam.Rows(iIndxGrd)("Id") = "FD" Or dtParam.Rows(iIndxGrd)("Id") = "DC" _
                             Or dtParam.Rows(iIndxGrd)("Id") = "FT" Or dtParam.Rows(iIndxGrd)("Id") = "CR" Or dtParam.Rows(iIndxGrd)("Id") = "KW" _
                              Or dtParam.Rows(iIndxGrd)("Id") = "TT" Or dtParam.Rows(iIndxGrd)("Id") = "FT" Or dtParam.Rows(iIndxGrd)("Id") = "OC" Then
                            txtField = dgParam.Rows(iIndxGrd).FindControl("txtParam")
                            dtParam.Rows(iIndxGrd)("SelectedName") = txtField.Text
                        End If
                    End If
                End If
            Next
            sSrh = BuildSearchContents("ID")  'To build Search Criteria
            sArr = Split(sSrh, "|")
            For i = 0 To UBound(sArr)
                If (sArr(i).Length > 2) Then
                    Select Case sArr(i).Substring(0, 2)
                        Case "CB"
                            sCab = GetSrhContentID(sArr(i), "CB")
                            If sCab <> Nothing Then
                                bCondation = True
                            End If
                        Case "SC"
                            sSubCab = GetSrhContentID(sArr(i), "SC")
                            If sSubCab <> Nothing Then
                                bCondation = True
                            End If
                        Case "FD"
                            sFol = GetSrhContentID(sArr(i), "FD")
                            If sFol <> Nothing Then
                                bCondation = True
                            End If
                        Case "DT"
                            sDocType = GetSrhContentID(sArr(i), "DT")
                            If sDocType <> Nothing Then
                                bCondation = True
                            End If
                        Case "TT"
                            sTitle = GetSrhContentID(sArr(i), "TT")
                            If sTitle <> Nothing Then
                                bCondation = True
                            End If
                        Case "KW"
                            sKW = GetSrhContentID(sArr(i), "KW")
                            If sKW <> Nothing Then
                                bCondation = True
                            End If
                        Case "DC"
                            sDesc = sArr(i)
                            sDesc = sDesc.Remove(0, 3)
                            If sDesc <> Nothing Then
                                bCondation = True
                            End If
                        Case "OC"
                            sOCRTxt = GetSrhContentID(sArr(i), "OC")
                            If sOCRTxt <> Nothing Then
                                bCondation = True
                            End If
                        Case "AD"
                            sAnyDesc = GetSrhContentID(sArr(i), "AD")
                            If sAnyDesc <> Nothing Then
                                bCondation = True
                            End If
                        Case "FT"
                            sFrmt = GetSrhContentID(sArr(i), "FT")
                            If sFrmt <> Nothing Then
                                bCondation = True
                            End If
                        Case "CR"
                            sCrby = GetSrhContentID(sArr(i), "CR")
                            If sCrby <> Nothing Then
                                bCondation = True
                            End If
                    End Select
                End If
            Next
            If bCondation = True Then
                If sFDate <> String.Empty Then
                    sFDate = clsGeneralFunctions.FormatMyDate(sFDate)
                End If
                If sTDate <> String.Empty Then
                    sTDate = clsGeneralFunctions.FormatMyDate(sTDate)
                End If

                Select Case ddlIndex.SelectedValue
                    Case 1
                        sSelectedCabID = sCab
                    Case 2
                        sSelectedSubCabID = sSubCab
                    Case 3
                        sSelectedFolID = sFol
                    Case 4
                        sSelectedDocTypeID = sDocType
                    Case 5
                        sSelectedKWID = sKW
                    Case 6
                        sSelectedDescID = sDesc
                    Case 7
                        sSelectedFrmtID = sFrmt
                    Case 8
                        sSelectedCrByID = sCrby
                End Select

                dtSearch = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.UserID, sCab, sSubCab, sFol, sDocType, sKW, sDesc, sFDate, sTDate, sOCRTxt, sAnyDesc, sFrmt, "", sCrby, , sTitle)
                If dtSearch.Rows.Count > 0 Then
                    'To check the permission for one Document Permission for selected Folder
                    'dtSearch = objclsSearch.ChkFolDocTypePerm(sSession.AccessCode, dtSearch, sSession.UserID)
                    If dtSearch.Rows.Count > 0 Then
                        lblSearchLinkValidationMsg.Text = dtSearch.Rows.Count & " documents found, Do you want to see the documents?" : lblError.Text = dtSearch.Rows.Count & " documents found, Do you want to see the documents?"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').addClass('alert alert-success');$('#ModalSearchLinkValidation').modal('show');", True)
                        iRet = dtSearch.Rows.Count
                    End If
                Else
                    dgSelectedData.DataSource = Nothing
                    dgSelectedData.DataBind()
                    dgViewSearchData.DataSource = Nothing
                    dgViewSearchData.DataBind()
                    lblSearchValidationMsg.Text = "No documents found." : lblError.Text = "No documents found."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalSearchValidation').modal('show');", True)
                End If
            Else
                dgSelectedData.DataSource = Nothing
                dgSelectedData.DataBind()
                dgViewSearchData.DataSource = Nothing
                dgViewSearchData.DataBind()
                lblSearchValidationMsg.Text = "Enter valid parameters." : lblError.Text = "Enter valid parameters."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddQuery_Click")
        End Try
    End Sub
    Private Function ValidateSearchCrit() As Boolean
        Dim sStr As String = ""
        Dim dRow As DataRow
        Try
            For Each dRow In dtParam.Rows
                If IsDBNull(dRow("ID")) = False Then
                    If (dRow("ID") = "ADC") Then
                        If IsDBNull(dRow("SelectedName")) = False Then
                            If Trim(dRow("SelectedName")) <> String.Empty Then
                                sStr = ValidationForm(dRow("SelectedID"), dRow("SelectedName"))
                                If (UCase(sStr) <> "TRUE") Then
                                    lblError.Text = sStr & " in " & dRow("Fields") & " field "
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function ValidationForm(ByVal iDescID As Integer, ByVal sStrVal As String, Optional ByVal RetType As Integer = 0) As String
        Dim sName As String, sChkType As String
        Try
            sName = objclsSearch.GetDescType(sSession.AccessCode, iDescID)
            If (RetType = 1) Then
                Return sName
            End If
            If sName = "Number" Then
                If Not IsNothing(sStrVal) Then
                    sChkType = sStrVal
                End If
                If IsMyInteger(sChkType) = True Then
                    Return "TRUE"
                Else
                    lblSearchLinkValidationMsg.Text = "Please Enter Only Numbers." : lblError.Text = "Please Enter Only Numbers."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').addClass('alert alert-warning');$('#ModalSearchLinkValidation').modal('show');", True)
                    Return "Please Enter Only Numbers "
                End If
            ElseIf sName = "Char" Then
                Return "TRUE"
            ElseIf sName = "Date" Then
                sChkType = sStrVal
                If IsDate(sChkType) = True Then
                    Return "TRUE"
                Else
                    lblSearchLinkValidationMsg.Text = "Please Enter valid Date." : lblError.Text = "Please Enter valid Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').addClass('alert alert-warning');$('#ModalSearchLinkValidation').modal('show');", True)
                    Return "Please Enter valid Date"
                End If
            Else
                Return "TRUE"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function IsMyInteger(ByVal str As String) As Boolean
        Dim i As Byte, Count As Byte
        Dim CharAscii As Integer
        For i = 1 To Len(str)
            CharAscii = Asc(Mid(str, i, 1))
            If (CharAscii > 47 And CharAscii < 58) Then
                Count = Count + 1
            Else
                If i = 1 Then
                    If CharAscii <> 43 And CharAscii <> 45 Then
                        IsMyInteger = False
                        Exit For
                    End If
                Else
                    IsMyInteger = False
                    Exit For
                End If
                Count = Count + 1
            End If
        Next
        If Count = Len(str) Then
            If Val(str) > 99999999999 Or Val(str) < -99999999999 Then
                IsMyInteger = False
            Else
                IsMyInteger = True
            End If
        End If
    End Function
    Private Function BuildSearchContents(ByVal IdOrName As String) As String
        Dim dr As DataRow
        Dim sStr As String = ""
        Try
            For Each dr In dtParam.Rows
                If IsDBNull(dr("Id")) = False Then
                    Select Case dr("Id")
                        Case "CB"
                            sStr = sStr & "CB-"
                        Case "SC"
                            sStr = sStr & "SC-"
                        Case "KW"
                            sStr = sStr & "KW-"
                        Case "DT"
                            sStr = sStr & "DT-"
                        Case "TT"
                            sStr = sStr & "TT-"
                        Case "FD"
                            sStr = sStr & "FD-"
                        Case "OC"
                            sStr = sStr & "OC-"
                        Case "AD"
                            sStr = sStr & "AD-"
                        Case "FT"
                            sStr = sStr & "FT-"
                        Case "CR"
                            sStr = sStr & "CR-"
                        Case "DE"
                            sStr = sStr & "DE-"
                    End Select
                    If (dr("Id") <> "ADC") Then
                        If (dr("Id") = "KW") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        ElseIf (dr("Id") = "OC") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        ElseIf (dr("Id") = "AD") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        ElseIf (dr("Id") = "TT") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        ElseIf (dr("Id") = "FT") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        ElseIf (dr("Id") = "DE") Then
                            If IsDBNull(dr("SelectedName")) = False Then
                                sStr = sStr & "+"
                                sStr = sStr & dr("SelectedName")
                            End If
                        Else
                            If (IdOrName = "BOTH") Then
                                If IsDBNull(dr("SelectedName")) = False Then
                                    sStr = sStr & dr("SelectedName")
                                End If
                                If IsDBNull(dr("SelectedID")) = False Then
                                    sStr = sStr & "+" & dr("SelectedID")
                                End If
                            ElseIf (IdOrName = "ID") Then
                                If IsDBNull(dr("SelectedID")) = False Then
                                    sStr = sStr & "+" & dr("SelectedID")
                                End If
                            ElseIf (IdOrName = "NAME") Then
                                If IsDBNull(dr("SelectedName")) = False Then
                                    sStr = sStr & dr("SelectedName")
                                End If
                            End If
                        End If
                        sStr = sStr & "|"
                    End If
                End If
            Next
            'Add Descriptors and their values to the ongoing string
            sStr = sStr & "DC-"
            For Each dr In dtParam.Rows
                If IsDBNull(dr("Id")) = False Then
                    If (dr("Id") = "ADC") Then
                        If (IsDBNull(dr("SelectedName")) <> True) Then
                            If (dr("SelectedName").ToString.Length > 0) Then
                                sStr = sStr & "$" & dr("SelectedID") & "," & dr("SelectedName")
                            End If
                        End If
                    End If
                End If
            Next
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetSrhContentID(ByVal sStr As String, ByVal sLvlType As String) As String
        Dim sCont() As String
        Dim sVal As String
        Dim i As Integer
        Try
            sCont = Split(sStr, "+")
            If (sLvlType <> "KW" And sLvlType <> "OC" And sLvlType <> "FT") Then
                If (sCont.Length > 1) Then
                    sVal = sCont(1)
                    sCont = Split(sVal, ";")
                    sVal = String.Empty
                    For i = 0 To UBound(sCont)
                        If (sCont(i).Length > 0) Then
                            sVal = sVal & "," & sCont(i)
                        End If
                    Next
                Else
                    Return ""
                End If
            ElseIf (sLvlType = "FT") Then
                If (sCont.Length > 1) Then
                    sVal = sCont(1)
                    sCont = Split(sVal, ",")
                    sVal = String.Empty
                    For i = 0 To UBound(sCont)
                        If (sCont(i).Length > 0) Then
                            sVal = sVal & ",'" & sCont(i) & "'"
                        End If
                    Next
                Else
                    Return ""
                End If
            ElseIf (sLvlType = "KW") Then
                If (sCont.Length > 0) Then
                    If (sCont.Length > 1) Then
                        sVal = sCont(1)
                        sCont = Split(sVal, ",")
                        sVal = String.Empty
                        For i = 0 To UBound(sCont)
                            If (sCont(i).Length > 0) Then
                                sCont(i) = Trim(sCont(i))
                                If (i > 0) Then
                                    sVal = sVal & " or PGE_KeyWord like "
                                End If
                                sVal = sVal & "'%;" & sCont(i) & ";%' or PGE_KeyWord like '%" & sCont(i) & ";%' or PGE_KeyWord like '%;" & sCont(i) & "%'  or PGE_KeyWord like '%" & sCont(i) & "%' or PGE_KeyWord='" & sCont(i) & "'"
                            End If
                        Next
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            ElseIf (sLvlType = "OC") Then
                If (sCont.Length > 0) Then
                    If (sCont.Length > 1) Then
                        sVal = sCont(1)
                        sCont = Split(sVal, ",")
                        sVal = String.Empty
                        For i = 0 To UBound(sCont)
                            If (i > 0) Then
                                If (sCont(i).Length > 0) Then  'To check Last ","
                                    sVal = sVal & " or PGE_OcrText like "
                                End If
                            End If
                            If (sCont(i).Length > 0) Then
                                sVal = sVal & "'%" & sCont(i) & "%' or PGE_OcrText like '" & sCont(i) & "%'  or PGE_OcrText like '%" & sCont(i) & "' or PGE_OcrText='" & sCont(i) & "'"
                            End If
                        Next
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            End If

            If (sVal.Length > 0) Then
                If (sVal.Chars(0).ToString = ",") Then
                    sVal = sVal.Remove(0, 1)
                End If
            End If
            Return sVal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub btnSearchLinkMsgOk_Click(sender As Object, e As EventArgs) Handles btnSearchLinkMsgOk.Click
        Dim oiRet As New Object
        Try
            lblError.Text = ""
            imgbtnView.Visible = True : dgViewSearchData.Visible = True
            LoadSearchDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearchLinkMsgOk_Click")
        End Try
    End Sub
    Private Sub LoadSearchDetails()
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dc As DataColumn
        Dim dRow As DataRow
        Try
            dc = New DataColumn("DetailsId", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("BaseID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("Title", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CabName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubCabName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("FolName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DocType", GetType(String))
            dt.Columns.Add(dc)

            If dtSearch.Rows.Count > 0 Then
                For i = 0 To dtSearch.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("DetailsId") = dtSearch.Rows(i).Item("DetailsID")
                    dRow("BaseID") = dtSearch.Rows(i).Item("BaseName")
                    dRow("Title") = dtSearch.Rows(i).Item("Title")
                    dRow("CabName") = dtSearch.Rows(i).Item("CabName")
                    dRow("SubCabName") = dtSearch.Rows(i).Item("SubCabName")
                    dRow("FolName") = dtSearch.Rows(i).Item("FolName")
                    dRow("DocType") = dtSearch.Rows(i).Item("DocType")
                    dt.Rows.Add(dRow)
                Next
                BindSearchData(dtSearch.Rows(0).Item("BaseName"), dtSearch.Rows(0).Item("DetailsID"))
            End If
            dtSearchResult = dt.Copy
            Dim DVZRBADetails As New DataView(dtSearchResult)
            DVZRBADetails.Sort = "Title ASC"
            dtDetails = DVZRBADetails.ToTable
            dtSearchResult = dtDetails.Copy
            dtSearchResult = objclsSearch.SetRows(dtSearchResult, 18)
            dgViewSearchData.DataSource = dtSearchResult
            dgViewSearchData.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgViewSearchData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgViewSearchData.RowCommand
        Dim lblDetailsID As New Label, lblBaseName As New Label
        Dim sImagePath As String = ""
        Dim oImagePath As Object, oSelectedChecksIDs As Object
        Dim oSelectedCabID As Object, oSelectedSubCabID As Object, oSelectedFolID As Object, oSelectedDocTypeID As Object, oSelectedKWID As Object, oSelectedDescID As Object
        Dim oSelectedFrmtID As Object, oSelectedCrByID As Object, oSelectedIndexID As Object, oSelId As Object
        Try
            lblError.Text = ""
            If e.CommandName.Equals("Select") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblBaseName = DirectCast(clickedRow.FindControl("lblBaseName"), Label)
                lblDetailsID = DirectCast(clickedRow.FindControl("lblDetailsID"), Label)
                BindSearchData(lblBaseName.Text, lblDetailsID.Text)

                sImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, Val(lblDetailsID.Text), sSession.UserID)
                oImagePath = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sImagePath))
                oSelectedChecksIDs = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(Val(lblDetailsID.Text)))
                oSelId = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelId))

                oSelectedCabID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedCabID))
                oSelectedSubCabID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedSubCabID))
                oSelectedFolID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedFolID))
                oSelectedDocTypeID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedDocTypeID))
                oSelectedKWID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedKWID))
                oSelectedDescID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedDescID))
                oSelectedFrmtID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedFrmtID))
                oSelectedCrByID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedCrByID))
                oSelectedIndexID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(ddlIndex.SelectedValue))

                Response.Redirect(String.Format("~/Search/ImageView.aspx?SelId={0}&SelectedChecksIDs={1}&SelectedCabID={2}&SelectedSubCabID={3}&SelectedFolID={4}&SelectedDocTypeID={5}&SelectedKWID={6}&SelectedDescID={7}&SelectedFrmtID={8}&SelectedCrByID={9}&SelectedIndexID={10}&ImgFilePath={11}&DocumentID={12}&FileID={13}&DocumentSelectedID={14}&FileSelectedID={15}&BackToFormID={16}", oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID, oImagePath, "", "", "", "", ""), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewSearchData_ItemCommand")
        End Try
    End Sub
    Private Function BindSearchData(ByVal iBaseNameID As Integer, ByVal iDetailsID As Integer) As Integer
        Dim iRow As Integer, iArrlst As Integer, iTotal As Integer, iCount As Integer
        Dim sDescName As String, sKwords As String, sScanDt As String
        Dim arrDesc As Array, arrKWords As Array, arrScanDt As Array
        Dim dtResult As New DataTable
        Dim dRow As DataRow
        Try
            iCount = 0
            dtResult = BuildResultTable()
            LoadDescriptorsDetails(iBaseNameID)
            For iRow = 0 To dtSearch.Rows.Count - 1
                If IsDBNull(dtSearch.Rows(iRow).Item("DetailsId")) = False Then
                    If dtSearch.Rows(iRow).Item("DetailsId") = iDetailsID Then
                        dRow = dtResult.NewRow
                        dRow("Details") = "CreatedBy : " & objclsSearch.GetCrBy(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)

                        dRow = dtResult.NewRow
                        dRow("Details") = "CreatedOn : " & objclsSearch.GetCrOn(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)

                        dRow = dtResult.NewRow
                        dRow("Details") = "Modified By : " & objclsSearch.GetModBy(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)
                        dRow = dtResult.NewRow
                        dRow("Details") = "Modified On : " & objclsSearch.GetModOn(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)

                        dRow = dtResult.NewRow
                        dRow("Details") = "Status : " & objclsSearch.GetStatus(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)

                        dRow = dtResult.NewRow
                        dRow("Details") = "Total Pages :  " & objclsSearch.GetTotalPage(sSession.AccessCode, iDetailsID)
                        dtResult.Rows.Add(dRow)

                        dRow = dtResult.NewRow
                        dRow("Details") = "File Size : " & objclsSearch.GetFileSize(sSession.AccessCode, iDetailsID) & "KB"
                        dtResult.Rows.Add(dRow)
                        Exit For
                    End If
                End If
            Next
            sDescName = objclsSearch.getDescName(sSession.AccessCode, iDetailsID)
            arrDesc = Split(sDescName, "'")
            sKwords = objclsSearch.GetKWords(sSession.AccessCode, iDetailsID)
            arrKWords = Split(sKwords, ";")

            If arrDesc.Length > arrKWords.Length Then
                iArrlst = arrDesc.Length
            Else
                iArrlst = arrKWords.Length
            End If

            If iArrlst > dtResult.Rows.Count Then
                iTotal = iArrlst - dtResult.Rows.Count
                For iRow = 0 To iTotal
                    dRow = dtResult.NewRow
                    dtResult.Rows.Add(dRow)
                Next
            Else
                iArrlst = dtResult.Rows.Count
            End If

            For iRow = 0 To UBound(arrDesc)
                dtResult.Rows(iRow).Item("Descriptors") = arrDesc(iRow)
            Next
            For iRow = 1 To UBound(arrKWords)
                dtResult.Rows(iCount).Item("Keywords") = arrKWords(iRow)
                iCount = iCount + 1
            Next

            sScanDt = objclsSearch.GetScanDetails(sSession.AccessCode, iDetailsID)
            If sScanDt <> "" Then
                arrScanDt = sScanDt.Split("|")
                iArrlst = arrScanDt.Length

                If iArrlst > dtResult.Rows.Count Then
                    iTotal = iArrlst - dtResult.Rows.Count
                    For iRow = 0 To iTotal
                        dRow = dtResult.NewRow
                        dtResult.Rows.Add(dRow)
                    Next
                Else
                    iArrlst = dtResult.Rows.Count
                End If

                For iRow = 0 To UBound(arrScanDt)
                    dtResult.Rows(iRow).Item("ScanDocument") = arrScanDt(iRow)
                Next
            End If
            dgSelectedData.DataSource = dtResult
            dgSelectedData.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildResultTable() As DataTable
        Dim dtlocal As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("Details")
            dtlocal.Columns.Add(dc)
            dc = New DataColumn("Descriptors")
            dtlocal.Columns.Add(dc)
            dc = New DataColumn("KeyWords")
            dtlocal.Columns.Add(dc)
            dc = New DataColumn("ScanDocument")
            dtlocal.Columns.Add(dc)
            Return dtlocal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function LoadDescriptorsDetails(ByVal iBaseNameID As Integer) As String
        Dim sDescName As String
        Try
            sDescName = objclsSearch.getDescName(sSession.AccessCode, iBaseNameID)
            Return sDescName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelect As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            If dgViewSearchData.Rows.Count > 0 Then
                chkAll = CType(sender, CheckBox)
                If chkAll.Checked = True Then
                    For iIndx = 0 To dgViewSearchData.Rows.Count - 1
                        chkSelect = dgViewSearchData.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = True
                    Next
                Else
                    For iIndx = 0 To dgViewSearchData.Rows.Count - 1
                        chkSelect = dgViewSearchData.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = False
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgViewSearchData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgViewSearchData.RowDataBound
        Dim chkBox As CheckBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > -1 And e.Row.RowIndex > 0 And dgViewSearchData.Rows.Count > 0 Then
                    If IsDBNull(dtSearchResult.Rows(e.Row.RowIndex)("Title")) = True Then
                        chkBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
                        chkBox.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgViewSearchData_ItemDataBound")
        End Try
    End Sub
    Public Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            lblError.Text = ""
            dgParam.DataSource = dtParam
            dgParam.DataBind()
            dgSelectedData.DataSource = Nothing
            dgSelectedData.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalSearchLinkValidation').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCancel_Click")
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim chkSelect As New CheckBox
        Dim iCheckSelected As Integer = 0, iSelectedFirstID As Integer = 0
        Dim sImagePath As String = ""
        Dim oSelectedCabID As Object, oSelectedSubCabID As Object, oSelectedFolID As Object, oSelectedDocTypeID As Object, oSelectedKWID As Object, oSelectedDescID As Object
        Dim oImagePath As Object, oSelectedChecksIDs As Object, oSelectedFrmtID As Object, oSelectedCrByID As Object, oSelectedIndexID As Object, oSelId As Object
        Dim aSelectedChecksIDs() As String
        Dim sDetailsID As String = ""
        Dim lblDetailsID As New Label
        Try
            lblError.Text = ""
            For i = 0 To dgViewSearchData.Rows.Count - 1
                chkSelect = dgViewSearchData.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCheckSelected = iCheckSelected + 1
                End If
            Next
            If iCheckSelected = 0 Then
                lblError.Text = "Select Documents to View." : lblSearchValidationMsg.Text = "Select Documents to View."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To dgViewSearchData.Rows.Count - 1
                lblDetailsID = dgViewSearchData.Rows(i).FindControl("lblDetailsID")
                chkSelect = dgViewSearchData.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    If Val(lblDetailsID.Text <> "") Then
                        sImagePath = objclsSearch.GetPageFromEdict(sSession.AccessCode, lblDetailsID.Text)
                        If sImagePath = "" Then
                            GoTo NextID
                        End If
                        sDetailsID = sDetailsID & "," & lblDetailsID.Text
                        If (sDetailsID.Length > 0) Then
                            If (sDetailsID.Chars(0).ToString = ",") Then
                                sDetailsID = sDetailsID.Remove(0, 1)
                            End If
                        End If
                    End If
                End If
NextID:     Next
            If sDetailsID = "" Then
                lblError.Text = "There is no attachments to View." : lblSearchValidationMsg.Text = "There is no attachments to View."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchValidation').modal('show');", True)
                Exit Sub
            End If
            sSelectedChecksIDs = sDetailsID
            If Not sSelectedChecksIDs Is Nothing Then
                If (sSelectedChecksIDs.Length > 0) Then
                    If (sSelectedChecksIDs.Chars(0).ToString = ",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    aSelectedChecksIDs = sSelectedChecksIDs.Split(",")
                    If aSelectedChecksIDs.Length > 0 Then
                        iSelectedFirstID = aSelectedChecksIDs(0)
                    End If
                End If
            End If

            sImagePath = objclsSearch.GetPageFromEdict(sSession.AccessCode, iSelectedFirstID)
            oImagePath = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sImagePath))
            oSelectedChecksIDs = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedChecksIDs))
            oSelId = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelId))

            oSelectedCabID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedCabID))
            oSelectedSubCabID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedSubCabID))
            oSelectedFolID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedFolID))
            oSelectedDocTypeID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedDocTypeID))
            oSelectedKWID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedKWID))
            oSelectedDescID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedDescID))
            oSelectedFrmtID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedFrmtID))
            oSelectedCrByID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedCrByID))
            oSelectedIndexID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(ddlIndex.SelectedValue))

            Response.Redirect(String.Format("~/Search/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={6}&SelectedDescID={7}&SelectedFrmtID={8}&SelectedCrByID={9}&SelectedIndexID={10}&ImgFilePath={11}&DocumentID={12}&FileID={13}&DocumentSelectedID={14}&FileSelectedID={15}&BackToFormID={16}", oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID, oImagePath, "", "", "", "", ""), False)
            'Response.Redirect(String.Format("~/Search/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", oImagePath, oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID), False)
            Response.Redirect(String.Format("~/Search/ImageView.aspx?SelId={0}&SelectedChecksIDs={1}&SelectedCabID={2}&SelectedSubCabID={3}&SelectedFolID={4}&SelectedDocTypeID={5}&SelectedKWID={6}&SelectedDescID={7}&SelectedFrmtID={8}&SelectedCrByID={9}&SelectedIndexID={10}&ImgFilePath={11}&DocumentID={12}&FileID={13}&DocumentSelectedID={14}&FileSelectedID={15}&BackToFormID={16}", oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID, oImagePath, "", "", "", "", ""), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnView_Click")
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            lblError.Text = ""
            bCondation = False : imgbtnView.Visible = False
            sSelName = String.Empty : sSelId = String.Empty
            dtCopyofParam = objclsSearch.SetRows(dtCopyofParam, 14)
            dgParam.DataSource = dtCopyofParam
            dgParam.DataBind()
            ddlIndex.SelectedValue = 4
            ddlIndex_SelectedIndexChanged(sender, e)
            dgSelectedData.DataSource = Nothing
            dgSelectedData.DataBind()

            AddCritToGrid("CABINETS", sSelName, sSelId)
            AddCritToGrid("SUBCABINETS", sSelName, sSelId)
            AddCritToGrid("FOLDERS", sSelName, sSelId)
            AddCritToGrid("DOCUMENTTYPES", sSelName, sSelId)
            AddCritToGrid("KEYWORDS", sSelName, sSelId)
            AddCritToGrid("FORMAT", sSelName, sSelId)
            AddCritToGrid("CREATED BY", sSelName, sSelId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnReset_Click")
        End Try
    End Sub

    Private Sub LoadSearch(ByVal sCABINET As String, ByVal sSUBCABINET As String, ByVal sFOLDER As String)
        Try
            lblError.Text = ""
            dtSearch = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.UserID, "", sSUBCABINET, sFOLDER, "", "", "", "", "", "", "", "", "", "", , "")
            If dtSearch.Rows.Count > 0 Then
                'To check the permission for one Document Permission for selected Folder
                ' dtSearch = objclsSearch.ChkFolDocTypePerm(sSession.AccessCode, dtSearch, sSession.UserID)
                If dtSearch.Rows.Count > 0 Then
                    lblSearchLinkValidationMsg.Text = dtSearch.Rows.Count & " documents found, Do you want to see the documents?" : lblError.Text = dtSearch.Rows.Count & " documents found, Do you want to see the documents?"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgLinkType').addClass('alert alert-success');$('#ModalSearchLinkValidation').modal('show');", True)
                    iRet = dtSearch.Rows.Count
                End If
            Else
                dgSelectedData.DataSource = Nothing
                dgSelectedData.DataBind()
                dgViewSearchData.DataSource = Nothing
                dgViewSearchData.DataBind()
                lblSearchValidationMsg.Text = "No documents found." : lblError.Text = "No documents found."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalSearchValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
