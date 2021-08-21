Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Object
Imports System.Diagnostics
Partial Class Accounts_PettyCashDayBook
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts/PettyCashDayBook"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objPC As New clsPettyCash
    Dim objPCDB As New ClsPettyCashDayBook
    Dim dtMerge As New DataTable
    Private Shared iDbOrCr As Integer = 0
    'Public Shared dtAdd As New DataTable

    Private objclsAttachments As New clsAttachments
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared dtAttach As New DataTable
    Private Shared sWFDelete As String
    Private Shared sINWView As String
    Private Shared sINWDownload As String
    Private Shared iInwardID As Integer
    Private objclsModulePermission As New clsModulePermission
    Private Shared iStatus As Integer
    Private objAccSetting As New clsAccountSetting
    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Public Shared dtsample As New DataTable
    'Public Shared dtAddCash As New DataTable
    'Public Shared dtAddDebit As New DataTable
    Public Shared dtAddExixting As New DataTable
    Public Shared SrNo As Integer


    Public Shared iMasId As Integer = 0

    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgCash.ImageUrl = "~/Images/Add16.png"
        imgDebit.ImageUrl = "~/Images/Add16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
        ImgBtnApprove.ImageUrl = "~/Images/Checkmark24.png"

    End Sub
    Public Function GetLineNumber(ByVal ex As Exception)
        Dim lineNumber As Int32 = 0
        Const lineSearch As String = ":line "
        Dim index = ex.StackTrace.LastIndexOf(lineSearch)
        If index <> -1 Then
            Dim lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length)
            If Int32.TryParse(lineNumberText, lineNumber) Then
            End If
        End If
        Return lineNumber
    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim sFormButtons As String = ""
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                iMasId = 0
                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PCT")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False
                imgCash.Visible = False : imgDebit.Visible = False
                btnAddAttch.Visible = False : btnScan.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        ' imgDebit.Visible = True
                        imgCash.Visible = True : imgDebit.Visible = True
                        btnAddAttch.Visible = True : btnScan.Visible = True
                    End If
                End If


                Session("Petty") = Nothing
                dtMerge = Nothing
                Session("datatable") = Nothing
                'imgbtnAdd.Visible = False : imgbtnSave.Visible = False :
                imgbtnUpdate.Visible = False
                ImgBtnApprove.Visible = False
                lblStatus.Visible = False
                'imgbtnAdd.Visible = True : imgbtnSave.Visible = True
                LoadExistingPC(0)
                'LoadSubGL()
                txtTransactionNo.Text = objPCDB.GenerateTransactionNumber(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)


                ddlCrHead.SelectedIndex = 1
                ddlCrHead_SelectedIndexChanged(sender, e)

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)
                LoadParty(3)
                ImgBtnApprove.Visible = False
                'LoadSubGL()
                iDefaultBranch = objPCDB.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If
                'dtAddCash.Clear()
                'dtAddDebit.Clear()
                dtAddExixting.Clear()
                'RFVdbGL.InitialValue = "Select GL Code" : RFVdbGL.ErrorMessage = "Select General Ledger."
                'RFVCrGL.InitialValue = "Select GL Code" : RFVCrGL.ErrorMessage = "Select General Ledger."
                RFVParty.InitialValue = "Select Customer/Supplier/Party" : RFVParty.ErrorMessage = "Select Customer/Supplier/Party."
                'RFVBillType.InitialValue = "Select Voucher Type" : RFVBillType.ErrorMessage = "Select Voucher Type."
                'RFVBillNo.ErrorMessage = "Enter Valid Bill Number."


                RFVInvoiceDate.ErrorMessage = "Enter Invoice Date."
                REVInvoiceDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVInvoiceDate.ErrorMessage = "Enter Valid Date Format."

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                sINWView = "YES" : sINWDownload = "YES" : sWFDelete = "YES"
                iAttachID = 0
                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                imgView.ImageUrl = "~/Images/NoImage.jpg"

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExisting.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExisting_SelectedIndexChanged(sender, e)
                    divcollapseAttachments.Visible = True
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                End If

            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
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
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "--- Select Region ---")
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
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "--- Select Area ---")
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
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingPC(ByVal iTransId As Integer)
        Try

            ddlExisting.DataSource = objPCDB.LoadExistingPettyCashVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExisting.DataTextField = "PDB_TransactionNo"
            ddlExisting.DataValueField = "PDB_PKID"
            ddlExisting.DataBind()
            ddlExisting.Items.Insert(0, "Existing PettyCash DayBook Voucher")

        Catch ex As Exception
            Throw
        End Try
    End Sub


    'Private Sub LoadSubGL()
    '    Try
    '        ddldbsUbGL.DataSource = objPCDB.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
    '        ddldbsUbGL.DataTextField = "GlDesc"
    '        ddldbsUbGL.DataValueField = "gl_Id"
    '        ddldbsUbGL.DataBind()
    '        ddldbsUbGL.Items.Insert(0, "Select Sub Genearal Ledger")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddlExisting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExisting.SelectedIndexChanged
        Try
            imgView.ImageUrl = "~/Images/NoImage.jpg"
            If ddlExisting.SelectedIndex > 0 Then

                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExisting.SelectedValue)
                ddlParty_SelectedIndexChanged(sender, e)
                ddlCrHead.SelectedIndex = 1
                ddlCrHead_SelectedIndexChanged(sender, e)

            Else
                imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                ImgBtnApprove.Visible = False
                GvPettyCashDetails.DataSource = Nothing
                GvPettyCashDetails.DataBind()
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExisting_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPayment As Integer)
        Dim dt As New DataTable, dtTrans As New DataTable
        Dim iAdvanceAmount As Integer = 0
        Dim dtPetty As New DataTable
        Try

            imgbtnSave.Visible = True : imgbtnUpdate.Visible = True
            ImgBtnApprove.Visible = True
            imgCash.Enabled = True : imgDebit.Enabled = True : txtCreditAmount.Enabled = True : txtDebitAmount.Enabled = True
            ddldbsUbGL.Enabled = True : ddlParty.Enabled = True : GvPettyCashDetails.Enabled = True
            txtVoucherNo.Enabled = True
            txtInvoiceDate.Enabled = True : txtNarration.Enabled = True
            dt = objPCDB.GetPaymentDetails(sNameSpace, iCompID, iPayment)
            If dt.Rows.Count > 0 Then

                'If IsDBNull(dt.Rows(0)("PDB_AttachID")) = False Then
                '    iAttachID = dt.Rows(0)("Acc_PCM_AttachID").ToString()
                'Else
                '    iAttachID = ""
                'End If

                If IsDBNull(dt.Rows(0)("PDB_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("PDB_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                If dt.Rows(0)("PDB_Status").ToString() = "W" Then
                    imgbtnSave.Visible = False
                    lblStatus.Text = "Waiting for Approval"
                ElseIf dt.Rows(0)("PDB_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                ElseIf dt.Rows(0)("PDB_Status").ToString() = "A" Then
                    lblStatus.Text = "Approved"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    ImgBtnApprove.Visible = False
                    imgCash.Enabled = False : imgDebit.Enabled = False : txtCreditAmount.Enabled = False : txtDebitAmount.Enabled = False
                    ddldbsUbGL.Enabled = False : ddlParty.Enabled = False : GvPettyCashDetails.Enabled = False
                    txtVoucherNo.Enabled = False
                    txtInvoiceDate.Enabled = False : txtNarration.Enabled = False
                End If

                If IsDBNull(dt.Rows(0)("PDB_CreatedOn").ToString()) = False Then
                    txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("PDB_CreatedOn").ToString(), "D")
                Else
                    txtInvoiceDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("PDB_Narration").ToString()) = False Then
                    If (dt.Rows(0)("PDB_Narration").ToString()) = 0 Then
                        txtNarration.Text = ""
                    Else
                        txtNarration.Text = dt.Rows(0)("PDB_Narration").ToString()
                    End If
                Else
                    txtNarration.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("PDB_ZoneID").ToString()) = False Then
                    If (dt.Rows(0)("PDB_ZoneID").ToString() = "") Then
                    Else
                        ddlAccZone.SelectedValue = dt.Rows(0)("PDB_ZoneID").ToString()
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("PDB_RegionID").ToString()) = False Then
                    If (dt.Rows(0)("PDB_RegionID").ToString() = "") Then
                    Else
                        ddlAccRgn.SelectedValue = dt.Rows(0)("PDB_RegionID").ToString()
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("PDB_AreaID").ToString()) = False Then
                    If (dt.Rows(0)("PDB_AreaID").ToString() = "") Then
                    Else
                        ddlAccArea.SelectedValue = dt.Rows(0)("PDB_AreaID").ToString()
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                End If
                If IsDBNull(dt.Rows(0)("PDB_BranchID").ToString()) = False Then
                    If (dt.Rows(0)("PDB_BranchID").ToString() = "") Then
                    Else
                        ddlAccBrnch.SelectedValue = dt.Rows(0)("PDB_BranchID").ToString()
                    End If
                End If
                ddlCrHead.SelectedIndex = 0
                'ddlCrGL.SelectedIndex = 0
                dtAddExixting.Clear()
                dtTrans = objPCDB.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPayment)

                dtAddExixting.Merge(dtTrans)
                GvPettyCashDetails.DataSource = dtAddExixting
                GvPettyCashDetails.DataBind()


            Else
                GvPettyCashDetails.DataSource = Nothing
                GvPettyCashDetails.DataBind()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub GetAttachFile(ByVal sTrNo As String)
    '    Dim dRow As DataRow
    '    Dim dt, dt1 As New DataTable
    '    Try
    '        dt.Columns.Add("FilePath")
    '        dt.Columns.Add("FileName")
    '        dt.Columns.Add("Extension")
    '        dt.Columns.Add("CreatedOn")

    '        dt1 = objPC.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
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
    Private Sub ddlCrGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrGL.SelectedIndexChanged
        Try
            If ddlCrGL.SelectedIndex > 0 Then
                ddlCrSubGL.DataSource = objPCDB.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                ddlCrSubGL.DataTextField = "GlDesc"
                ddlCrSubGL.DataValueField = "gl_Id"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrGL_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub ddlCrSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrSubGL.SelectedIndexChanged
        Dim iHead As Integer
        Try
            If ddlCrSubGL.SelectedIndex > 0 Then
                iHead = objPC.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrGL.DataSource = objPCDB.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")

                ddlCrGL.SelectedValue = objPC.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
                ddlCrHead.SelectedIndex = iHead
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrSubGL_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    'Private Function CheckDebitAndCredit() As Integer
    '    Dim i As Integer = 0
    '    Dim dDebit As Double = 0, dCredit As Double = 0
    '    Try
    '        For i = 0 To GvPettyCashDetails.Rows.Count - 1
    '            If (IsDBNull(GvPettyCashDetails.Rows(i).Cells(10).Text) = False) And (GvPettyCashDetails.Rows(i).Cells(10).Text <> "&nbsp;") Then
    '                dDebit = dDebit + Convert.ToDouble(GvPettyCashDetails.Rows(i).Cells(10).Text)
    '            End If

    '            If (IsDBNull(GvPettyCashDetails.Rows(i).Cells(11).Text) = False) And (GvPettyCashDetails.Rows(i).Cells(11).Text <> "&nbsp;") Then
    '                dCredit = dCredit + Convert.ToDouble(GvPettyCashDetails.Rows(i).Cells(11).Text)
    '            End If
    '        Next

    '        'If dDebit <> dCredit Then
    '        '    Return 1  ' Debit and Credit amount not Matched
    '        'ElseIf dDebit <> txtBillAmount.Text.Trim Then
    '        '    Return 2
    '        'End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objPett As New clsPettyCash.Petty
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim lblCashRecieved As Label, lblDate As Label, lblParticulars As Label, lblVoucherNo As Label, lblAmount As Label, lblId As Label, lblNarration As Label
        Try

            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count = 0 Then
                lblError.Text = "Enter Cash and debit Details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash and debit Details.','', 'success');", True)
                Exit Sub
            End If

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            ''Cheque Date Comparision'

            ''Cheque Date Comparision'
            ''If txtBillDate.Text <> "" Then
            ''    dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    m = DateDiff(DateInterval.Day, dDate, dSDate)
            ''    If m < 0 Then
            ''        lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
            ''        lblCustomerValidationMsg.Text = lblError.Text
            ''        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ''        txtBillDate.Focus()
            ''        Exit Sub
            ''    End If

            ''    dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    m = DateDiff(DateInterval.Day, dDate, dSDate)
            ''    If m > 0 Then
            ''        lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
            ''        lblCustomerValidationMsg.Text = lblError.Text
            ''        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ''        txtBillDate.Focus()
            ''        Exit Sub
            ''    End If
            ''End If
            ''Cheque Date Comparision'

            ''If ddldbsUbGL.Items.Count > 1 Then
            ''    If ddldbsUbGL.SelectedIndex > 0 Then
            ''    Else
            ''        lblError.Text = "Select the Sub General Ledger for Debit."
            ''        Exit Sub
            ''    End If
            ''End If

            'If ddlCrSubGL.Items.Count > 1 Then
            '    If ddlCrSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        Exit Sub
            '    End If
            'End If

            'iRet = CheckDebitAndCredit()

            'If iRet = 1 Then
            '    lblError.Text = "Debit Amount and Credit Amount Not Matched."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
            '    Exit Sub
            'ElseIf iRet = 2 Then
            '    lblError.Text = "Amount not Matched with Bill Amount."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
            '    Exit Sub
            'End If


            objPCDB.iPdb_pkid = iMasId
            objPCDB.sPdb_transactionNo = txtTransactionNo.Text
            objPCDB.iPDB_ZoneID = ddlAccZone.SelectedValue
            objPCDB.iPDB_RegionID = ddlAccRgn.SelectedValue
            objPCDB.iPDB_AreaID = ddlAccArea.SelectedValue
            objPCDB.iPDB_BranchID = ddlAccBrnch.SelectedValue
            objPCDB.sPdb_Narration = txtNarration.Text
            objPCDB.dPdb_cashTotal = 0
            objPCDB.dPdb_DebitTotal = 0
            objPCDB.iPDB_CompID = sSession.UserID
            objPCDB.iPDB_YearId = sSession.YearID
            objPCDB.sPDB_Status = "W"
            objPCDB.iPDB_CreatedBy = sSession.UserID
            objPCDB.iPDB_UpdatedBy = sSession.UserID
            objPCDB.sPDB_IPAddress = sSession.IPAddress
            Arr = objPCDB.SavePettyCashDayBookMaster(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
            iTransID = Arr(1)
            iMasId = Arr(1)
            If GvPettyCashDetails.Rows.Count > 0 Then
                For i = 0 To GvPettyCashDetails.Rows.Count - 1



                    objPCDB.iPDBDD_PKID = 0
                    objPCDB.iPDBD_MasterID = Arr(1)
                    lblCashRecieved = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")
                    If Val(lblCashRecieved.Text) = 0 Then
                        objPCDB.dPDBD_CashReceived = 0
                    Else
                        objPCDB.dPDBD_CashReceived = lblCashRecieved.Text
                    End If
                    lblDate = GvPettyCashDetails.Rows(i).FindControl("lblDate")
                    objPCDB.dPDBD_Date = lblDate.Text
                    lblParticulars = GvPettyCashDetails.Rows(i).FindControl("lblParticularsId")
                    objPCDB.sPDBD_Particulars = lblParticulars.Text
                    lblVoucherNo = GvPettyCashDetails.Rows(i).FindControl("lblVoucherNo")
                    objPCDB.sPDBD_Voucherno = lblVoucherNo.Text
                    lblAmount = GvPettyCashDetails.Rows(i).FindControl("lblAmount")
                    If Val(lblAmount.Text) = 0.0 Then
                        objPCDB.dPDBD_DebitAmount = 0
                    Else
                        objPCDB.dPDBD_DebitAmount = lblAmount.Text
                    End If

                    lblNarration = GvPettyCashDetails.Rows(i).FindControl("lblNarration")
                    objPCDB.sPDBD_Narration = lblNarration.Text

                    objPCDB.iPDBD_CreatedBy = sSession.UserID
                    objPCDB.iPDBD_YearID = sSession.UserID
                    objPCDB.iPDBD_CompID = sSession.YearID
                    objPCDB.sPDBD_Status = "W"
                    objPCDB.sPDBD_IPAddress = sSession.IPAddress
                    objPCDB.iPDBD_UpdatedBy = sSession.UserID


                    objPCDB.SavePettyCashDayBookDetails(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
                Next
            End If


            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
                ImgBtnApprove.Visible = True
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                ImgBtnApprove.Visible = True
            End If

            For i = 0 To GvPettyCashDetails.Rows.Count - 1
                objPCDB.iATD_TrType = 12
                objPCDB.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objPCDB.iATD_BillId = iTransID
                objPCDB.iATD_PaymentType = 0
                'objPC.dATD_TransactionDate = Date.Today
                lblParticulars = GvPettyCashDetails.Rows(i).FindControl("lblParticularsId")
                lblAmount = GvPettyCashDetails.Rows(i).FindControl("lblAmount")
                lblCashRecieved = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")

                If lblParticulars.Text <> "" Then
                    objPCDB.iATD_SubGL = Convert.ToInt32(lblParticulars.Text)
                    'objPCDB.iATD_GL = 0
                    objPCDB.iATD_GL = objPCDB.GetGLId(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Convert.ToInt32(lblParticulars.Text))
                    If objPCDB.iATD_GL <> 0 Then
                        objPCDB.iATD_Head = objPCDB.GetHeadId(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objPCDB.iATD_GL)
                    Else
                        objPCDB.iATD_Head = 0
                    End If

                Else
                    objPCDB.iATD_GL = 0
                    objPCDB.iATD_SubGL = 0
                    objPCDB.iATD_Head = 0
                End If



                If lblAmount.Text <> "" Then
                    objPCDB.dATD_Debit = Convert.ToInt32(lblAmount.Text)
                    objPCDB.iATD_DbOrCr = 1
                Else
                    objPCDB.dATD_Debit = 0
                End If

                If lblCashRecieved.Text <> "" Then
                    objPCDB.dATD_Credit = Convert.ToInt32(lblCashRecieved.Text)
                    objPCDB.iATD_DbOrCr = 2
                Else
                    objPCDB.dATD_Credit = 0
                End If

                objPCDB.iATD_CreatedBy = sSession.UserID
                objPCDB.iATD_UpdatedBy = sSession.UserID
                objPCDB.sATD_Status = "W"
                objPCDB.iATD_YearID = sSession.YearID
                objPCDB.sATD_Operation = "C"
                objPCDB.sATD_IPAddress = sSession.IPAddress

                objPCDB.iATD_ZoneID = ddlAccZone.SelectedValue
                objPCDB.iATD_RegionID = ddlAccRgn.SelectedValue
                objPCDB.iATD_AreaID = ddlAccArea.SelectedValue
                objPCDB.iATD_BranchID = ddlAccBrnch.SelectedValue

                objPCDB.dATD_OpenDebit = "0.00"
                objPCDB.dATD_OpenCredit = "0.00"
                objPCDB.dATD_ClosingDebit = "0.00"
                objPCDB.dATD_ClosingCredit = "0.00"
                objPCDB.iATD_SeqReferenceNum = 0

                objPCDB.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
            Next

            BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, iTransID)
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

            lblStatus.Text = "Waiting for Approval"
            LoadExistingPC(iTransID)
            ' ddlExisting.SelectedValue = iTransID

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click") ', GetLineNumber(ex)
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""

            If lblStatus.Text = "Waiting for Approval" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            End If
            Response.Redirect(String.Format("~/Accounts/PettyCashTransaction.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dtNew As New DataTable
        Try

            lblError.Text = ""
            dtMerge = dtNew : Session("Datatable") = Nothing
            txtTransactionNo.Text = objPCDB.GenerateTransactionNumber(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExisting.SelectedIndex = 0 : ddlParty.SelectedIndex = 0
            ddlCrHead.SelectedIndex = 0
            lblStatus.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            GvPettyCashDetails.DataSource = dtNew
            GvPettyCashDetails.DataBind()
            'LoadSubGL()
            dtAddExixting.Clear()

            Response.Redirect("~/Accounts/PettyCashDayBook.aspx")



            ddlCrGL.DataSource = dtNew
            ddlCrGL.DataBind()

            Session("Petty") = Nothing
            'dgPetty.DataSource = Nothing
            'dgPetty.DataBind()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click") ', GetLineNumber(ex)
        End Try
    End Sub

    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            ds = objPC.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                divcollapseAttachments.Visible = True
            Else
                divcollapseAttachments.Visible = False
            End If
            dgAttach.PageSize = 1000
            dgAttach.DataSource = ds
            dgAttach.DataBind()
            lblBadgeCount.Text = dgAttach.Items.Count
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Dim sTempPath As String = ""
        Dim lSize As Long
        Dim dt As New DataTable
        Try
            lblMsg.Text = ""
            dtAttach.Columns.Add("FilePath")
            dtAttach.Columns.Add("FileName")
            lblError.Text = "" : iDocID = 0

            Dim hfc As HttpFileCollection = Request.Files
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        lSize = CType(hpf.ContentLength, Integer)
                        If (sSession.FileSize * 1024 * 1024) < lSize Then
                            lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                            Exit Sub
                        End If
                        dRow = dtAttach.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")

                        If sTempPath.EndsWith("\") = True Then
                            sTempPath = sTempPath & "Temp\Attachment\"
                        Else
                            sTempPath = sTempPath & "Temp\Attachment\"
                        End If

                        objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sTempPath)
                        'objclsGeneralFunctions.ClearBrowseDirectory(sTempPath)
                        hpf.SaveAs(sTempPath & sFilesNames)
                        dRow("FilePath") = sTempPath & sFilesNames
                        dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName) & "." & System.IO.Path.GetExtension(hpf.FileName)
                        If System.IO.File.Exists(dRow("FilePath")) = True Then
                            iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, dRow("FilePath"), sSession.UserID, iAttachID)
                            If iAttachID > 0 Then
                                BindAllAttachments(sSession.AccessCode, iAttachID)
                            End If
                        Else
                            lblMsg.Text = "No file to Attach."
                        End If
                        dtAttach.Rows.Add(dRow)
                    End If
                Next
            End If
            If dtAttach.Rows.Count = 0 Then
                lblMsg.Text = "No file to Attach."
            End If

            dtAttach = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
            Throw
        End Try
    End Sub
    Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
        Dim lblExt As New Label, lblFile As New Label
        Dim File As New LinkButton
        Dim imgbtnView As New ImageButton, imgbtnAdd As New ImageButton, imgbtnDownload As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnView = CType(e.Item.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"
                imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnDownload = CType(e.Item.FindControl("imgbtnDownload"), ImageButton)
                imgbtnDownload.ImageUrl = "~/Images/Download16.png"
                imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                lblFile = CType(e.Item.FindControl("lblFile"), Label)
                File = CType(e.Item.FindControl("File"), LinkButton)
                lblExt = CType(e.Item.FindControl("lblExt"), Label)
                lblExt.Text = UCase(lblExt.Text)

                dgAttach.Columns(4).Visible = False : dgAttach.Columns(6).Visible = False : dgAttach.Columns(7).Visible = False

                If sINWDownload = "YES" Then
                    dgAttach.Columns(6).Visible = True
                End If

                If sINWView = "YES" Then
                    dgAttach.Columns(4).Visible = True
                End If

                If sWFDelete = "YES" Then
                    dgAttach.Columns(7).Visible = True
                End If

                If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "BRK" Or lblExt.Text = "CAL" Or lblExt.Text = "PDF" Or
                        lblExt.Text = "CLP" Or lblExt.Text = "DCX" Or lblExt.Text = "EPS" Or lblExt.Text = "ICO" Or lblExt.Text = "IFF" Or lblExt.Text = "IMT" Or
                        lblExt.Text = "ICA" Or lblExt.Text = "PCT" Or lblExt.Text = "PCX" Or lblExt.Text = "PNG" Or lblExt.Text = "PSD" Or lblExt.Text = "RAS" Or
                    lblExt.Text = "SGI" Or lblExt.Text = "TGA" Or lblExt.Text = "XBM" Or lblExt.Text = "XPM" Or lblExt.Text = "XWD" Or lblExt.Text = "TIF" Or lblExt.Text = "TIFF" Or lblExt.Text = "TXT") Then
                    imgbtnView.Enabled = True
                    lblFile.Visible = True
                    File.Visible = False
                Else
                    imgbtnView.Enabled = False
                    lblFile.Visible = False
                    File.Visible = True
                End If


                If (lblExt.Text = "JPG" Or lblExt.Text = "JPEG" Or lblExt.Text = "BMP" Or lblExt.Text = "GIF" Or lblExt.Text = "PNG") Then
                    lblFile.Visible = True : File.Visible = False
                Else
                    lblFile.Visible = False : File.Visible = True
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub dgAttach_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Dim sExtn As String = ""
        Try
            lblError.Text = "" : lblMsg.Text = ""

            If e.CommandName = "OPENPAGE" Or e.CommandName = "VIEW" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                'sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "TempPath")
                sPaths = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Temp\Attachment\"
                Else
                    sPaths = sPaths & "\Temp\Attachment\"
                End If
                If e.CommandName = "VIEW" Then
                    Dim oImgFilePath As New Object, oDocumentID As New Object, oFileID As New Object, oInwrdID As Object, oStatus As Object, oBackToFormID As Object

                    sDestFilePath = objclsAttachments.GetOriginalDocumentPathNew(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                    oImgFilePath = HttpUtility.UrlEncode(objGen.EncryptQueryString(sDestFilePath))
                    oDocumentID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iAttachID))
                    oFileID = HttpUtility.UrlEncode(objGen.EncryptQueryString(iDocID))
                    'oInwrdID = HttpUtility.UrlDecode(objclsEdictGeneral.EncryptQueryString(iInwardID))
                    oStatus = HttpUtility.UrlDecode(objGen.EncryptQueryString(iStatus))
                    oBackToFormID = HttpUtility.UrlDecode(objGen.EncryptQueryString(2))

                    'SelId ---> InwardID
                    'SelectedChecksIDs ---> Status

                    'Response.Redirect(String.Format("~/VSAnnotation/AnnotationDemo.aspx?SelId={0}&SelectedChecksIDs={1}&DocumentID={2}&FileID={3}&ImgFilePath={4}&BackToFormID={5}", oInwrdID, oStatus, oDocumentID, oFileID, oImgFilePath, oBackToFormID), False)
                    sExtn = objPC.GetExtension(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                    'Response.Redirect(String.Format("~/ViewAttachment/View.aspx?ImgFilePath={0}&sExtn={1}&AttachID={2}&DocID={3}", oImgFilePath, sExtn, iAttachID, iDocID), False)
                    ViewContent(sDestFilePath, sExtn)

                ElseIf e.CommandName = "OPENPAGE" Then
                    sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                    DownloadMyFile(sDestFilePath)
                End If
            End If
            If e.CommandName = "REMOVE" Then
                txtDescription.Text = ""
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = e.Item.FindControl("lblFDescription")
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddDesc_Click(sender As Object, e As EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "collapse", "$('#collapseAttachments').collapse('show');", True)
        Catch ex As Exception
            GetLineNumber(ex)
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click") ', GetLineNumber(ex)
        End Try
    End Sub
    Public Sub ViewContent(ByVal sPath As String, ByVal sExtn As String)
        Try
            If UCase(sExtn) = "JPG" Or UCase(sExtn) = "PNG" Or UCase(sExtn) = "JPEG" Or UCase(sExtn) = "GIF" Or UCase(sExtn) = "BMP" Then
                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sPath)
                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                imgView.ImageUrl = imageDataURL1
            Else
                imgView.ImageUrl = "~/Images/NoImage.jpg"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlParty.SelectedIndex > 0 Then
                ddldbsUbGL.DataSource = objPCDB.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                'ddldbsUbGL.DataSource = objPCDB.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID)
                ddldbsUbGL.DataTextField = "GlDesc"
                ddldbsUbGL.DataValueField = "gl_Id"
                ddldbsUbGL.DataBind()
                ddldbsUbGL.Items.Insert(0, "Select Sub Genearal Ledger")
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    'Private Sub imgbtnAddBillAmt_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddBillAmt.Click
    '    'Dim dBillAmt, dDebitAmt, dTotalBillAmt As Double
    '    Dim dt As New DataTable
    '    Dim dr As DataRow
    '    Dim dTotalAmt As Double
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("PettyID")
    '        dt.Columns.Add("BillNo")
    '        dt.Columns.Add("BillDate")
    '        dt.Columns.Add("BillAmount")

    '        If IsNothing(Session("Petty")) = False Then
    '            dt = Session("Petty")
    '        End If

    '        dr = dt.NewRow
    '        dr("ID") = 0
    '        dr("PettyID") = 0
    '        dr("BillNo") = txtBillNo.Text
    '        dr("BillDate") = txtBillDate.Text
    '        dr("BillAmount") = txtBillAmount.Text

    '        dt.Rows.Add(dr)

    '        dgPetty.DataSource = dt
    '        dgPetty.DataBind()

    '        Session("Petty") = dt

    '        For i = 0 To dgPetty.Items.Count - 1
    '            dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
    '        Next
    '        txtDebitAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
    '        txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
    '        dTotalAmt = 0
    '        'dBillAmt = txtBillAmount.Text
    '        'If txtDebitAmount.Text <> "" Then
    '        '    dDebitAmt = txtDebitAmount.Text
    '        'Else
    '        '    txtDebitAmount.Text = txtBillAmount.Text
    '        'End If
    '        'dTotalBillAmt = dDebitAmt + dBillAmt
    '        'txtDebitAmount.Text = dTotalBillAmt
    '        'txtBillAmount.Text = ""
    '    Catch ex As Exception
    '        GetLineNumber(ex)
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddBillAmt_Click", GetLineNumber(ex))
    '    End Try
    'End Sub
    'Private Sub dgPetty_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPetty.ItemCommand
    '    Dim dt As New DataTable
    '    Dim dTotalAmt As Double
    '    Try
    '        If e.CommandName = "Delete" Then
    '            dt = Session("Petty")
    '            dt.Rows.Item(e.Item.ItemIndex).Delete()
    '            Session("Petty") = dt
    '        End If
    '        dgPetty.DataSource = dt
    '        dgPetty.DataBind()

    '        For i = 0 To dgPetty.Items.Count - 1
    '            dTotalAmt = dTotalAmt + Convert.ToDouble(dgPetty.Items(i).Cells(4).Text)
    '        Next
    '        '  txtDebitAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
    '    Catch ex As Exception
    '        GetLineNumber(ex)
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPetty_ItemCommand", GetLineNumber(ex))
    '    End Try
    'End Sub

    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
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
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlAccBrnch.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Payment Voucher")

            If ddlExisting.SelectedIndex = 0 Then
                lblError.Text = "Select Existing payment No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExisting.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlExisting.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

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
                        lblCustomerValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASPayment').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AutomaticIndexing") ', GetLineNumber(ex)
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

            If ddlExisting.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Existing Payment No."
                ddlExisting.Focus()
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
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender") ', GetLineNumber(ex)
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
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
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlExisting.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Payment Voucher")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlExisting.SelectedItem.Text)

                    dt = objPC.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
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
                lblError.Text = "Select Existing Payment No"
                Exit Sub
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnView_Click") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                iParent = objPCDB.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objPCDB.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objPCDB.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadParty(ByVal iType As Integer)
        Try
            ddlParty.DataSource = objPCDB.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "GlDesc"
            ddlParty.DataValueField = "gl_Id"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select General Ledger")
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Sub GvPettyCashDetails_PreRender(sender As Object, e As EventArgs) Handles GvPettyCashDetails.PreRender
        Dim dt As New DataTable
        Try
            If GvPettyCashDetails.Rows.Count > 0 Then
                GvPettyCashDetails.UseAccessibleHeader = True
                GvPettyCashDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GvPettyCashDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetails_PreRender") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub ddlCrHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrHead.SelectedIndexChanged
        Try
            ddlCrGL.Items.Clear()
            If ddlCrHead.SelectedIndex > 0 Then
                ddlCrGL.DataSource = objPCDB.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex)
                ddlCrGL.DataTextField = "GlDesc"
                ddlCrGL.DataValueField = "gl_Id"
                ddlCrGL.DataBind()
                ddlCrGL.Items.Insert(0, "Select GL Code")
                If ddlCrHead.SelectedIndex = 1 Then
                    ddlCrGL.SelectedValue = objPCDB.LoadAssetGL(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex)
                End If
                If ddlCrHead.SelectedIndex = 1 Then
                    ddlCrSubGL.DataSource = objPCDB.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                    ddlCrSubGL.DataTextField = "GlDesc"
                    ddlCrSubGL.DataValueField = "gl_Id"
                    ddlCrSubGL.DataBind()
                    ddlCrSubGL.Items.Insert(0, "Select GL Code")
                    Dim Sglid As Integer = objPCDB.LoadAssetSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedIndex)
                    ddlCrSubGL.SelectedValue = Sglid
                End If
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrHead_SelectedIndexChanged") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub imgcash_Click(sender As Object, e As ImageClickEventArgs) Handles imgCash.Click
        Dim dtAdd As New DataTable
        Dim dRow As DataRow

        Try
            lblError.Text = ""
            If txtCreditAmount.Text = "" Or txtCreditAmount.Text = 0 Then
                lblError.Text = "Add Cash."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Cash.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count > 0 Then

            End If
            If ddlParty.SelectedIndex = 0 Then
                lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select General Ledger.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If
            If txtInvoiceDate.Text = "" Then
                lblError.Text = "Add Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Date.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If

            dtAdd.Columns.Add("SrNo")
            dtAdd.Columns.Add("PkId")
            dtAdd.Columns.Add("MasterId")
            dtAdd.Columns.Add("Date")
            dtAdd.Columns.Add("CashRecieved")
            'dt.Columns.Add("DateofCashRecieved")
            dtAdd.Columns.Add("Particulars")
            dtAdd.Columns.Add("ParticularsId")
            dtAdd.Columns.Add("VoucherNo")
            dtAdd.Columns.Add("Amount")
            dtAdd.Columns.Add("Narration")
            'If IsNothing(dtAddDebit) = True Then
            '    dtAdd.Merge(dtAdd)
            'Else
            '    dtAdd.Merge(dtAddDebit)
            'End If
            'If IsNothing(dtAddExixting) = True Then
            '    dtAdd.Merge(dtAdd)
            'Else
            '    dtAdd.Merge(dtAddExixting)
            'End If
            dRow = dtAdd.NewRow

            dRow = dtAdd.NewRow

            If GvPettyCashDetails.Rows.Count = 0 Then
                SrNo = 1
            Else
                SrNo = SrNo + 1
            End If

            dRow("SrNo") = SrNo
            dRow("PkId") = 0
            dRow("MasterId") = 0
            dRow("Date") = txtInvoiceDate.Text
            dRow("CashRecieved") = txtCreditAmount.Text

            dRow("Particulars") = ddlCrSubGL.SelectedItem
            dRow("ParticularsId") = ddlCrSubGL.SelectedValue
            dRow("VoucherNo") = ""
            dRow("Amount") = ""
            dRow("Narration") = ""
            dtAdd.Rows.Add(dRow)
            'dtAddCash.Merge(dtAdd)

            dtAddExixting.Merge(dtAdd)
            GvPettyCashDetails.DataSource = dtAddExixting

            'GvPettyCashDetails.DataSource = dtAddCash
            GvPettyCashDetails.DataBind()
            'dtAddDebit.Clear()
            'dtAddExixting.Clear()
            txtCreditAmount.Text = ""
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgcash_Click") ', GetLineNumber(ex)
        End Try
    End Sub

    Private Sub GvPettyCashDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvPettyCashDetails.RowCommand

        Dim lblDGBadgeCount As New Label
        Dim imgbtndgAttachment As New ImageButton
        Dim iDgAtchCount As New Integer
        Dim i As Integer = 0
        Dim lblPKID As New Label, lblMasterId As New Label, lblAsgnID As New Label, lblcashRecieved As New Label, lblamount As New Label
        Dim iAsgnID As Integer = 0, iDetailsID As Integer = 0, dCashRecived As Double = 0, dAmount As Double = 0
        Dim ds As New DataSet
        Dim iMasterId As Integer = 0
        Dim iPkID As Integer = 0
        'Dim dr As New DataRow
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblMasterId = DirectCast(clickedRow.FindControl("lblMasterId"), Label)
                lblPKID = DirectCast(clickedRow.FindControl("lblPKid"), Label)
                'imgbtndgAttachment = GvPettyCashDetails.Rows.Item(i).FindControl("imgbtnDeActivate")
                iMasterId = Val(lblMasterId.Text)
                iPkID = Val(lblPKID.Text)
                If iMasterId > 0 Then
                    objPCDB.DeletePettyCashDetails(sSession.AccessCode, sSession.AccessCodeID, iPkID, iMasterId)
                    BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, iMasterId)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetails_RowCommand") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub editTextBox(ByVal dCashrecieved As Double, ByVal dAmount As Double)
        Try
            If dCashrecieved > 0 Then
                txtCreditAmount.Text = dCashrecieved
                txtCreditAmount.Focus()
            End If
            If dAmount > 0 Then
                txtDebitAmount.Text = dAmount
                txtDebitAmount.Focus()
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub GvPettyCashDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvPettyCashDetails.RowDataBound
        Dim imgbtndgAdd As New ImageButton, imgbtnDeactivcate As New ImageButton
        Dim DDLParticulars As DropDownList
        Dim lblSubProID As New Label, lblDetailsID As New Label, lblAsgnID As New Label
        Try
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                imgbtnDeactivcate = CType(e.Row.FindControl("imgbtnDeActivate"), ImageButton)
                imgbtnDeactivcate.ImageUrl = "~/Images/Trash16.png"

                'DDLParticulars = (TryCast(e.Row.FindControl("DDLParticulars"), DropDownList))
                'DDLParticulars.Enabled = True
                'If txtCreditAmount.Text = "" Then
                '    dt = objPCDB.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                'Else
                '    dt = objPCDB.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                'End If
                'If dt.Rows.Count > 0 Then
                '    DDLParticulars.DataSource = dt
                '    DDLParticulars.DataTextField = "GlDesc"
                '    DDLParticulars.DataValueField = "gl_ID"
                '    DDLParticulars.DataBind()
                'Else
                '    DDLParticulars.DataSource = Nothing
                '    DDLParticulars.DataBind()
                'End If

            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetails_RowDataBound") ', GetLineNumber(ex)
        End Try
    End Sub

    Private Sub imgDebit_Click(sender As Object, e As ImageClickEventArgs) Handles imgDebit.Click

        Dim dtsample As New DataTable, dt1 As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Dim lblAsgnID As New Label, lblDetailsID As New Label
        Dim lblvoucher As New Label, lblamount As New Label, lblCash As New Label, lbldate As New Label, lblPerticulars As New Label
        Dim iVoucher As String = ""
        Dim dCash As Double = 0
        Dim iSlno As Integer = 0
        Dim sPerticulars As String = ""
        Dim dAmount As Double = 0
        Dim sVoucher As String = ""
        Try
            If txtInvoiceDate.Text = "" Then
                lblError.Text = "Add Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Date.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If

            If ddldbsUbGL.SelectedIndex = 0 Then
                lblError.Text = "Select Sub Genearal Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Sub Genearal Ledger.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If
            If txtVoucherNo.Text = "" Then
                lblError.Text = "Enter Voucher No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Voucher No.','', 'info');", True)
                ddlParty.Focus()
                Exit Sub
            End If
            If txtDebitAmount.Text = "" Then
                lblError.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Debit Amount.','', 'info');", True)
                txtDebitAmount.Focus()
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count = 0 Then
                lblError.Text = "Enter Cash."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash.','', 'info');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count > 0 Then
                For i = 0 To GvPettyCashDetails.Rows.Count - 1
                    lblCash = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")
                    dCash = Val(lblCash.Text) + dCash
                    lblamount = GvPettyCashDetails.Rows(i).FindControl("lblamount")
                    dAmount = Val(lblamount.Text) + dAmount
                    lblvoucher = GvPettyCashDetails.Rows(i).FindControl("lblVoucherNo")
                    sVoucher = lblvoucher.Text
                    If sVoucher = txtVoucherNo.Text Then
                        lblError.Text = "Voucher no Already Exist."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Voucher no Already Exist.','', 'info');", True)
                        txtVoucherNo.Focus()
                        Exit Sub
                    End If
                Next
                dAmount = Val(txtDebitAmount.Text) + dAmount
            End If
            If dAmount > dCash Then
                lblError.Text = "Enter Cash."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash.','', 'info');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If

            If txtDebitAmount.Text = 0 Then
                lblError.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Debit Amount.','', 'info');", True)
                txtDebitAmount.Focus()
                Exit Sub
            End If



            lblError.Text = ""
            txtCreditAmount.Text = ""

            dt1.Columns.Add("SrNo")
            dt1.Columns.Add("PkId")
            dt1.Columns.Add("MasterId")
            dt1.Columns.Add("Date")
            dt1.Columns.Add("CashRecieved")
            dt1.Columns.Add("Particulars")
            dt1.Columns.Add("ParticularsId")
            dt1.Columns.Add("VoucherNo")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("Narration")
            'If IsNothing(dtAddCash) = True Then
            '    dt1.Merge(dt1)
            'Else
            '    dt1.Merge(dtAddCash)
            'End If
            'If IsNothing(dtAddExixting) = True Then
            '    dt1.Merge(dt1)
            'Else
            '    dt1.Merge(dtAddExixting)
            'End If




            dRow = dt1.NewRow

            dRow("SrNo") = ""
            dRow("PkId") = 0
            dRow("MasterId") = 0
            dRow("Date") = txtInvoiceDate.Text
            dRow("CashRecieved") = ""
            dRow("Particulars") = ddldbsUbGL.SelectedItem
            dRow("ParticularsId") = ddldbsUbGL.SelectedValue
            dRow("VoucherNo") = txtVoucherNo.Text
            dRow("Amount") = txtDebitAmount.Text
            dRow("Narration") = txtNarration.Text
            dt1.Rows.Add(dRow)
            ' dtAddDebit.Merge(dt1)
            dtAddExixting.Merge(dt1)

            ' GvPettyCashDetails.DataSource = dtAddDebit
            GvPettyCashDetails.DataSource = dtAddExixting
            GvPettyCashDetails.DataBind()

            'dtAddCash.Clear()
            'dtAddExixting.Clear()
            txtDebitAmount.Text = "" : txtVoucherNo.Text = "" : txtNarration.Text = ""
            ddlParty_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AddRowGVpettyCashDetails") ', GetLineNumber(ex)
        End Try
    End Sub
    Private Sub GvPettyCashDetails_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GvPettyCashDetails.RowEditing
        Try
        Catch ex As Exception
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objPett As New clsPettyCash.Petty
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dt As New DataTable
        Dim lblCashRecieved As Label, lblDate As Label, lblParticulars As Label, lblVoucherNo As Label, lblAmount As Label, lblId As Label, lblNarration As Label
        Try
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count = 0 Then
                lblError.Text = "Enter Cash and debit Details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash and debit Details.','', 'success');", True)
                Exit Sub
            End If
            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date Of Payment (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            ''Cheque Date Comparision'

            ''Cheque Date Comparision'
            ''If txtBillDate.Text <> "" Then
            ''    dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    m = DateDiff(DateInterval.Day, dDate, dSDate)
            ''    If m < 0 Then
            ''        lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
            ''        lblCustomerValidationMsg.Text = lblError.Text
            ''        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ''        txtBillDate.Focus()
            ''        Exit Sub
            ''    End If

            ''    dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            ''    m = DateDiff(DateInterval.Day, dDate, dSDate)
            ''    If m > 0 Then
            ''        lblError.Text = "BillDate (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
            ''        lblCustomerValidationMsg.Text = lblError.Text
            ''        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ''        txtBillDate.Focus()
            ''        Exit Sub
            ''    End If
            ''End If
            ''Cheque Date Comparision'

            ''If ddldbsUbGL.Items.Count > 1 Then
            ''    If ddldbsUbGL.SelectedIndex > 0 Then
            ''    Else
            ''        lblError.Text = "Select the Sub General Ledger for Debit."
            ''        Exit Sub
            ''    End If
            ''End If

            'If ddlCrSubGL.Items.Count > 1 Then
            '    If ddlCrSubGL.SelectedIndex > 0 Then
            '    Else
            '        lblError.Text = "Select the Sub General Ledger for Credit."
            '        Exit Sub
            '    End If
            'End If

            'iRet = CheckDebitAndCredit()

            'If iRet = 1 Then
            '    lblError.Text = "Debit Amount and Credit Amount Not Matched."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Debit Amount and Credit Amount Not Matched.','', 'info');", True)
            '    Exit Sub
            'ElseIf iRet = 2 Then
            '    lblError.Text = "Amount not Matched with Bill Amount."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not Matched with Bill Amount.','', 'info');", True)
            '    Exit Sub
            'End If

            If ddlExisting.SelectedIndex > 0 Then
                objPCDB.iPdb_pkid = ddlExisting.SelectedValue
            Else
                objPCDB.iPdb_pkid = iMasId
            End If
            objPCDB.sPdb_transactionNo = txtTransactionNo.Text
            objPCDB.iPDB_ZoneID = ddlAccZone.SelectedValue
            objPCDB.iPDB_RegionID = ddlAccRgn.SelectedValue
            objPCDB.iPDB_AreaID = ddlAccArea.SelectedValue
            objPCDB.iPDB_BranchID = ddlAccBrnch.SelectedValue
            objPCDB.sPdb_Narration = ""
            objPCDB.dPdb_cashTotal = 0
            objPCDB.dPdb_DebitTotal = 0
            objPCDB.iPDB_CompID = sSession.UserID
            objPCDB.iPDB_YearId = sSession.YearID
            objPCDB.sPDB_Status = "W"
            objPCDB.iPDB_CreatedBy = sSession.UserID
            objPCDB.iPDB_UpdatedBy = sSession.UserID
            objPCDB.sPDB_IPAddress = sSession.IPAddress
            Arr = objPCDB.SavePettyCashDayBookMaster(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
            iTransID = Arr(1)




            '    For j = 0 To dgPetty.Items.Count - 1
            '        objPett.iAPMD_ID = 0
            '        objPett.iAPMD_MasterID = iTransID
            '        If (IsDBNull(dgPetty.Items(j).Cells(2).Text) = False) And (dgPetty.Items(j).Cells(2).Text <> "&nbsp;") Then
            '            objPett.sAPMD_BillNo = dgPetty.Items(j).Cells(2).Text
            '        Else
            '            objPett.sAPMD_BillNo = ""
            '        End If
            '        If (IsDBNull(dgPetty.Items(j).Cells(3).Text) = False) And (dgPetty.Items(j).Cells(3).Text <> "&nbsp;") Then
            '            objPett.dAPMD_BillDate = Date.ParseExact(dgPetty.Items(j).Cells(3).Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            '        Else
            '            objPett.dAPMD_BillDate = "01/01/1900"
            '        End If
            '        If (IsDBNull(dgPetty.Items(j).Cells(4).Text) = False) And (dgPetty.Items(j).Cells(4).Text <> "&nbsp;") Then
            '            objPett.dAPMD_BillAmount = dgPetty.Items(j).Cells(4).Text
            '        Else
            '            objPett.dAPMD_BillAmount = 0
            '        End If
            '        objPett.sAPMD_Status = "W"
            '        objPett.iAPMD_CreatedBy = sSession.UserID
            '        objPett.dAPMD_CreatedOn = Date.Today
            '        objPett.iAPMD_CompID = sSession.AccessCodeID
            '        objPett.iAPMD_YearID = sSession.YearID
            '        objPett.sAPMD_Operation = "C"
            '        objPett.sAPMD_IPAddress = sSession.IPAddress

            '        objPC.SavePettyBreakUp(sSession.AccessCode, sSession.AccessCodeID, objPett)
            '    Next

            ''Multiple BillNo Saving Option'

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
            End If
            If GvPettyCashDetails.Rows.Count > 0 Then
                For i = 0 To GvPettyCashDetails.Rows.Count - 1


                    lblId = GvPettyCashDetails.Rows(i).FindControl("lblpkID")
                    objPCDB.iPDBDD_PKID = lblId.Text
                    objPCDB.iPDBD_MasterID = Arr(1)
                    lblCashRecieved = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")
                    If Val(lblCashRecieved.Text) = 0 Then
                        objPCDB.dPDBD_CashReceived = 0
                    Else
                        objPCDB.dPDBD_CashReceived = lblCashRecieved.Text
                    End If
                    lblDate = GvPettyCashDetails.Rows(i).FindControl("lblDate")
                    objPCDB.dPDBD_Date = lblDate.Text
                    lblParticulars = GvPettyCashDetails.Rows(i).FindControl("lblParticularsId")
                    objPCDB.sPDBD_Particulars = lblParticulars.Text
                    lblVoucherNo = GvPettyCashDetails.Rows(i).FindControl("lblVoucherNo")
                    objPCDB.sPDBD_Voucherno = lblVoucherNo.Text
                    lblAmount = GvPettyCashDetails.Rows(i).FindControl("lblAmount")

                    If Val(lblAmount.Text) = 0.0 Then
                        objPCDB.dPDBD_DebitAmount = 0
                    Else
                        objPCDB.dPDBD_DebitAmount = lblAmount.Text
                    End If

                    lblNarration = GvPettyCashDetails.Rows(i).FindControl("lblNarration")
                    objPCDB.sPDBD_Narration = lblNarration.Text

                    objPCDB.iPDBD_CreatedBy = sSession.UserID
                    objPCDB.iPDBD_YearID = sSession.UserID
                    objPCDB.iPDBD_CompID = sSession.YearID
                    objPCDB.sPDBD_Status = "W"
                    objPCDB.sPDBD_IPAddress = sSession.IPAddress
                    objPCDB.iPDBD_UpdatedBy = sSession.UserID


                    objPCDB.SavePettyCashDayBookDetails(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
                Next
            End If

            objPCDB.DeleteExePCDBTransDetails(sSession.AccessCode, sSession.AccessCodeID, iTransID)

            For i = 0 To GvPettyCashDetails.Rows.Count - 1
                objPCDB.iATD_TrType = 12
                objPCDB.dATD_TransactionDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objPCDB.iATD_BillId = iTransID
                objPCDB.iATD_PaymentType = 0
                'objPC.dATD_TransactionDate = Date.Today
                lblParticulars = GvPettyCashDetails.Rows(i).FindControl("lblParticularsId")
                lblAmount = GvPettyCashDetails.Rows(i).FindControl("lblAmount")
                lblCashRecieved = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")

                If lblParticulars.Text <> "" Then
                    objPCDB.iATD_SubGL = Convert.ToInt32(lblParticulars.Text)
                    'objPCDB.iATD_GL = 0
                    objPCDB.iATD_GL = objPCDB.GetGLId(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Convert.ToInt32(lblParticulars.Text))
                Else
                    objPCDB.iATD_GL = 0
                    objPCDB.iATD_SubGL = 0
                End If

                If (IsDBNull(GvPettyCashDetails.Rows(i).Cells(1).Text) = False) And (GvPettyCashDetails.Rows(i).Cells(1).Text <> "&nbsp;") Then
                    objPCDB.iATD_Head = GvPettyCashDetails.Rows(i).Cells(1).Text
                Else
                    objPCDB.iATD_Head = 0
                End If

                If lblAmount.Text <> "" Then
                    objPCDB.dATD_Debit = Convert.ToInt32(lblAmount.Text)
                    objPCDB.iATD_DbOrCr = 1
                Else
                    objPCDB.dATD_Debit = 0
                End If

                If lblCashRecieved.Text <> "" Then
                    objPCDB.dATD_Credit = Convert.ToInt32(lblCashRecieved.Text)
                    objPCDB.iATD_DbOrCr = 2
                Else
                    objPCDB.dATD_Credit = 0
                End If

                objPCDB.iATD_CreatedBy = sSession.UserID
                objPCDB.iATD_UpdatedBy = sSession.UserID
                objPCDB.sATD_Status = "W"
                objPCDB.iATD_YearID = sSession.YearID
                objPCDB.sATD_Operation = "C"
                objPCDB.sATD_IPAddress = sSession.IPAddress

                objPCDB.iATD_ZoneID = ddlAccZone.SelectedValue
                objPCDB.iATD_RegionID = ddlAccRgn.SelectedValue
                objPCDB.iATD_AreaID = ddlAccArea.SelectedValue
                objPCDB.iATD_BranchID = ddlAccBrnch.SelectedValue

                objPCDB.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPCDB)
            Next
            BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, iTransID)
            'dt = objPCDB.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID)
            'dtAddExixting.Merge(dt)
            'GvPettyCashDetails.DataSource = dtAddExixting
            'GvPettyCashDetails.DataBind()
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

            lblStatus.Text = "Waiting for Approval"
            LoadExistingPC(iTransID)
            ' ddlExisting.SelectedValue = iTransID

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click") ', GetLineNumber(ex)
        End Try
    End Sub

    Private Sub ImgBtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnApprove.Click
        Dim lblcash As New Label, lblamount As New Label
        Dim dcash As Double = 0, dAmount As Double = 0, iPkid As Integer = 0, iMasterId As Integer = 0, dAmountTotal As Integer = 0
        Dim lblMasterId As New Label
        Try
            lblError.Text = ""
            If GvPettyCashDetails.Rows.Count = 0 Then
                lblError.Text = "Enter Cash and Debit details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash and Debit details.','', 'info');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If
            If GvPettyCashDetails.Rows.Count > 0 Then
                For i = 0 To GvPettyCashDetails.Rows.Count - 1
                    lblcash = GvPettyCashDetails.Rows(i).FindControl("lblCashRecieved")
                    dcash = Val(lblcash.Text) + dcash
                    lblamount = GvPettyCashDetails.Rows(i).FindControl("lblamount")
                    lblMasterId = GvPettyCashDetails.Rows(i).FindControl("lblmasterID")
                    dAmount = Val(lblamount.Text) + dAmount
                Next
                dAmountTotal = Val(txtDebitAmount.Text) + dAmount
            End If

            If dAmount > dcash Then
                lblError.Text = "Enter Cash."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Cash.','', 'info');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If


            If ddlExisting.SelectedIndex > 0 Then
                iPkid = ddlExisting.SelectedValue
            Else
                iPkid = Convert.ToInt32(lblMasterId.Text)
            End If

            objPCDB.uploadStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPkid)
            objPCDB.uploadCreditAndDebit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPkid, dcash, dAmount)
            BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, iPkid)
            'GvPettyCashDetails.DataSource = objPCDB.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExisting.SelectedValue)
            'GvPettyCashDetails.DataBind()

            'lblStatus.Text = "Waiting for Approval"
            LoadExistingPC(0)
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            ImgBtnApprove.Visible = False : imgCash.Enabled = False
            imgDebit.Enabled = False : txtCreditAmount.Enabled = False : txtDebitAmount.Enabled = False
            ddldbsUbGL.Enabled = False : ddlParty.Enabled = False
            lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnApprove_Click") ', GetLineNumber(ex)
        End Try
    End Sub
    Protected Sub GvPettyCashDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim lblPKID As Label
        Dim dt1 As New DataTable
        Try
            dt1.Merge(dtAddExixting)
            If GvPettyCashDetails.Rows.Count > 0 Then
                lblPKID = GvPettyCashDetails.Rows(0).FindControl("lblPkId")
                Dim index As Integer = Convert.ToInt32(e.RowIndex)
                If Val(lblPKID.Text) > 0 Then
                    Exit Sub
                Else
                    dt1.Rows(index).Delete()
                    dtAddExixting = dt1
                    GvPettyCashDetails.DataSource = dtAddExixting
                    GvPettyCashDetails.DataBind()
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetails_RowDeleting") ', GetLineNumber(ex)
        End Try
    End Sub
End Class