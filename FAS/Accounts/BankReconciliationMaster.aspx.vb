Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class Accounts_BankReoconcilationMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_BankReoconcilationMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objBankRconln As New clsBankReconcilationMaster
    Private Shared sSession As AllSession
    Private Shared dtBankDetails As New DataTable
    Dim objClsFASGnrl As New clsFASGeneral
    Private Shared sEMPAD As String
    Dim objJE As New clsJournalEntry
    Dim objGen As New clsFASGeneral

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png" : imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                BindStatus()
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                RFVSearch.ErrorMessage = "Select Search by."
                RFVSearch.InitialValue = "Select"
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objClsFASGnrl.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtBankDetails = objBankRconln.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                dgBankReconcilation.DataSource = dtBankDetails
                dgBankReconcilation.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Approved")
            ddlStatus.Items.Insert(1, "Waiting for Approval")
            ddlStatus.Items.Insert(2, "Company Data")
            ddlStatus.Items.Insert(3, "Bank Data")
            ddlStatus.Items.Insert(4, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                dt = objBankRconln.LoadAllDetails2(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A")
                dgBankReconcilation.DataSource = dt
                dgBankReconcilation.DataBind()
            ElseIf ddlStatus.SelectedIndex = 1 Then
                dt = objBankRconln.LoadAllDetails2(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W")
                dgBankReconcilation.DataSource = dt
                dgBankReconcilation.DataBind()
            ElseIf ddlStatus.SelectedIndex = 2 Then
                dt = objBankRconln.LoadAllDetails3(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "C")
                dgBankReconcilation.DataSource = dt
                dgBankReconcilation.DataBind()
            ElseIf ddlStatus.SelectedIndex = 3 Then
                dt = objBankRconln.LoadAllDetails3(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "B")
                dgBankReconcilation.DataSource = dt
                dgBankReconcilation.DataBind()
            ElseIf ddlStatus.SelectedIndex = 4 Then
                dt = objBankRconln.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                dgBankReconcilation.DataSource = dt
                dgBankReconcilation.DataBind()
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgBankReconcilation.Rows.Count - 1
                    chkField = dgBankReconcilation.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgBankReconcilation.Rows.Count - 1
                    chkField = dgBankReconcilation.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim BankID As New Object, oStatusID As New Object
        Try
            lblError.Text = ""
            BankID = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(1))

            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(4))
            End If
            Response.Redirect(String.Format("~/Accounts/BankReconciliation.aspx?EmpID={0}&StatusID={1}", BankID, oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objBankRconln.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/Bankrecondashboard.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=BankreconcilationDashboard" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub


    Protected Sub dgBankReconcilation_PreRender(sender As Object, e As EventArgs) Handles dgBankReconcilation.PreRender
        Try
            If dgBankReconcilation.Rows.Count > 0 Then
                dgBankReconcilation.UseAccessibleHeader = True
                dgBankReconcilation.HeaderRow.TableSection = TableRowSection.TableHeader
                dgBankReconcilation.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBankReconcilation_PreRender")
        End Try
    End Sub
    Protected Sub dgBankReconcilation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgBankReconcilation.RowDataBound
        Dim imgbutton As ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbutton = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbutton.ImageUrl = "~/Images/Edit16.png"
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub dgBankReconcilation_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgBankReconcilation.RowCommand
        Dim lblCredcitAmount As Label
        Dim lblDabitAmount As Label
        Dim lblCCredit As Label
        Dim lblCDabit As Label
        Dim BnkID As New Label : Dim status As String
        Dim oMasterID As Object
        Dim lnkSerialNo As New LinkButton : Dim trType As Integer : Dim TrnsID As Integer
        Try

            If e.CommandName.Equals("ShowDetails") Then
                Dim click As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lnkSerialNo = DirectCast(click.FindControl("lnkSerialNo"), LinkButton)
                TrnsID = objBankRconln.GetTransactionID(sSession.AccessCode, sSession.AccessCodeID, lnkSerialNo.Text)
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(TrnsID)))
                trType = objBankRconln.GetTransactionType(sSession.AccessCode, sSession.AccessCodeID, lnkSerialNo.Text)
                If trType > 0 Then
                    Response.Redirect(String.Format("~/Accounts/PaymentTransactionDetails.aspx?MasterID={0}", oMasterID), False)
                Else
                    Response.Redirect(String.Format("~/Accounts/ReceiptTransactionDetails.aspx?MasterID={0}", oMasterID), False)
                End If
            End If

            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                BnkID = DirectCast(clickedRow.FindControl("BnkID"), Label)
                lblBANKID.Text = BnkID.Text
                status = objBankRconln.check(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text)
                If status = "W" Then
                    'objBankRconln.DeleteCompanyData(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text)
                    lblDabitAmount = DirectCast(clickedRow.FindControl("lblDebit"), Label) 'BankAmount
                    lblCredcitAmount = DirectCast(clickedRow.FindControl("lblCredit"), Label)
                    txtCredit.Text = lblCredcitAmount.Text
                    txtdebit.Text = lblDabitAmount.Text
                    lblCDabit = DirectCast(clickedRow.FindControl("lblCDabit"), Label) 'CompanyAmount
                    txtboxCDebit.Text = lblCDabit.Text
                    lblCCredit = DirectCast(clickedRow.FindControl("lblCCredit"), Label)
                    txtboxCredit.Text = lblCCredit.Text
                    txtDescription.Text = "Adjustment"
                    If (lblCredcitAmount.Text <> "0.00" Or lblDabitAmount.Text <> "0.00") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBankReconcilation_RowCommand")
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim imasterID As Integer
        Dim Arr() As String
        Dim iTransID As String
        Dim lnkSerialNo As New LinkButton : Dim trType As Integer
        Try
            If txtboxCredit.Text = "0.00" And txtboxCDebit.Text = "0.00" Then
                objBankRconln.UpdateDescriptionAmountNotexist(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text)
            Else
                If chkbxJE.Checked = True Then
                    objJE.iAcc_JE_ID = 0
                    objJE.sAcc_JE_TransactionNo = 0
                    objJE.iAcc_JE_Location = 0
                    objJE.iAcc_JE_Party = 0
                    objJE.iAcc_JE_BillType = 0
                    objJE.sAcc_JE_ChequeNo = ""
                    objJE.dAcc_JE_ChequeDate = "01/01/1900"
                    objJE.sAcc_JE_IFSCCode = ""
                    objJE.sAcc_JE_BankName = ""
                    objJE.sAcc_JE_BranchName = ""
                    objJE.sAcc_JE_AdvanceNaration = ""
                    objJE.sAcc_JE_BillNo = 0
                    objJE.dAcc_JE_BillDate = Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objJE.dAcc_JE_BillAmount = 0
                    objJE.iAcc_JE_YearID = sSession.YearID
                    objJE.sAcc_JE_Status = "W"
                    objJE.iAcc_JE_CreatedBy = sSession.UserID
                    objJE.iAcc_JE_UpdatedBy = sSession.UserID
                    objJE.sAcc_JE_Operation = "C"
                    objJE.sAcc_JE_IPAddress = sSession.IPAddress
                    objJE.dAcc_JE_InvoiceDate = Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objJE.iAcc_JE_AttachID = 0
                    objJE.iACC_JE_ZoneID = 0
                    objJE.iACC_JE_RegionID = 0
                    objJE.iACC_JE_AreaID = 0
                    objJE.iACC_JE_BranchID = 0
                    'objJE.dATD_TransactionDate = Date.Today
                    Arr = objJE.SaveJournalMaster(sSession.AccessCode, sSession.AccessCodeID, objJE)

                    iTransID = Arr(1)

                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                    End If

                    trType = objBankRconln.GetTransactionType(sSession.AccessCode, sSession.AccessCodeID, lnkSerialNo.Text)

                    If trType > 0 Then 'payment
                        objJE.iATD_TrType = 11 'bankreconcilation form transaction
                        objJE.iATD_BillId = iTransID
                        objJE.iATD_PaymentType = 1
                        objJE.iATD_Head = 1
                        objJE.iATD_GL = 194
                        objJE.iATD_SubGL = 195
                        objJE.iATD_DbOrCr = 1
                        objJE.dATD_Debit = txtdebit.Text
                        objJE.dATD_Credit = txtCredit.Text
                        objJE.iATD_CreatedBy = sSession.UserID
                        objJE.iATD_UpdatedBy = sSession.UserID
                        objJE.sATD_Status = "A"
                        objJE.iATD_YearID = sSession.YearID
                        objJE.sATD_Operation = "U"
                        objJE.sATD_IPAddress = sSession.IPAddress
                        objJE.dATD_TransactionDate = Date.Today
                        Arr = objJE.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
                        imasterID = objBankRconln.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, Arr(1))
                        dtBankDetails = objBankRconln.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                        dgBankReconcilation.DataSource = dtBankDetails
                        dgBankReconcilation.DataBind()
                    Else  'Receipt
                        objJE.iATD_TrType = 11 'bankreconcilation form transaction
                        objJE.iATD_BillId = iTransID
                        objJE.iATD_PaymentType = 1
                        objJE.iATD_Head = 1
                        objJE.iATD_GL = 191
                        objJE.iATD_SubGL = 192
                        objJE.iATD_DbOrCr = 1
                        objJE.dATD_Debit = txtdebit.Text
                        objJE.dATD_Credit = txtCredit.Text
                        objJE.iATD_CreatedBy = sSession.UserID
                        objJE.iATD_UpdatedBy = sSession.UserID
                        objJE.sATD_Status = "A"
                        objJE.iATD_YearID = sSession.YearID
                        objJE.sATD_Operation = "U"
                        objJE.sATD_IPAddress = sSession.IPAddress
                        objJE.dATD_TransactionDate = date.Today
                        Arr = objJE.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
                        imasterID = objBankRconln.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, Arr(1))
                        dtBankDetails = objBankRconln.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                        dgBankReconcilation.DataSource = dtBankDetails
                        dgBankReconcilation.DataBind()
                    End If
                Else
                    imasterID = objBankRconln.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, 0)
                    dtBankDetails = objBankRconln.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    dgBankReconcilation.DataSource = dtBankDetails
                    dgBankReconcilation.DataBind()
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class
