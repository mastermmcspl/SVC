Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms

Partial Class Accounts_ChequeDetails
    Inherits System.Web.UI.Page

    Private sFormName As String = "Accounts_ChequeDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private ObjDB As New clsChequeDetails
    Dim objGen As New clsFASGeneral
    Private sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Private objclsAttachments As New clsAttachments
    Public lblToDate As New Label
    Private Shared sGMBackStatus As String
    Private Shared iMasterID As Integer
    Private Shared iAttachID As Integer
    Private Shared sCDSave As String
    Private Shared iDocID As Integer

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnDelete.ImageUrl = "~/Images/Trash24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterID As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                txtfile.Visible = True : lblBrowse.Visible = True : lblSize.Visible = True

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PDCR")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCDSave = "NO" : btnAddAttch.Visible = False
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
                        btnAddAttch.Visible = True
                        sCDSave = "YES"
                    End If
                End If

                ddlExeChequeNo_SelectedIndexChanged(sender, e)
                BindExistingCNO()
                GenSerialNo()
                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExeChequeNo.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExeChequeNo_SelectedIndexChanged(sender, e)
                End If
                ChequeDetailsClientsSideValidation()
                'txtToDate.Text = Date.ParseExact(Trim(Date.Today), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtToDate.Text = Date.Now.ToString("dd/MM/yyyy")
                txtChequeDate.Attributes.Add("onblur", "javascript:return CheckChqDate('','')")
                txtCollectedDate.Attributes.Add("onblur", "javascript:return CheckCltDate('','')")
                txtProDate.Attributes.Add("onblur", "javascript:return CheckProDate('','')")

                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                iDocID = 0 : iAttachID = 0
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myModalAttchment').modal('show');return false;")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub ChequeDetailsClientsSideValidation()
        Try
            RFVPartyCode.ErrorMessage = "Select Party Code" : RFVPartyCode.InitialValue = "0"
            RFVChequeNo.ErrorMessage = "Enter Cheque No." : REVChequeNo.ErrorMessage = "Enter only numeric values" : REVChequeNo.ValidationExpression = "^[0-9]{6}$"
            RFVChequeDate.ControlToValidate = "txtChequeDate" : RFVChequeDate.ErrorMessage = "Select Date from Calendar."
            REVChequeDate.ErrorMessage = "Enter valid Date." : REVChequeDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            RFVIFSCcode.ErrorMessage = "Enter IFSC code" : REVIFSccode.ErrorMessage = "Check IFSC code" : REVIFSccode.ValidationExpression = "^[^\s]{4}\d{7}$"
            RFVAccountNo.ErrorMessage = "Enter Account No." : REVAccountNo.ErrorMessage = "Enter only numeric values" : REVAccountNo.ValidationExpression = "^[0-9]{1,17}$"
            RFVMicrCode.ErrorMessage = "Enter MICR No." : REVMicrCode.ErrorMessage = "Enter only numeric values" : REVMicrCode.ValidationExpression = "^[0-9]{9}$"
            RFVLeafNo.ErrorMessage = "Enter Leaf No." : REVLeafNo.ErrorMessage = "Enter 3 Digit Leaf No." : REVLeafNo.ValidationExpression = "^[0-9]{3}$"
            RFVBankName.ErrorMessage = "Select Bank Name" : RFVBankName.InitialValue = "0"
            RFVBranchName.ErrorMessage = "Select Branch Name" : RFVBranchName.InitialValue = "0"
            RFVPayto.ErrorMessage = "Enter Pay name" : REVPayto.ErrorMessage = "Enter Pay name" : REVPayto.ValidationExpression = "^[a-zA-Z\s]+$"
            RFVRupees.ControlToValidate = "txtRupees" : RFVRupees.ErrorMessage = "Enter Rupees"
            REVRupees.ErrorMessage = "Enter Rupees in words" : REVRupees.ValidationExpression = "^(.{0,200})$"
            RFVAmount.ErrorMessage = "Enter Amount in numbers" : REVAmount.ErrorMessage = "Enter Amount in numbers" : REVAmount.ValidationExpression = "^[0-9]{0,10000}$"
            RFVAccountType.ErrorMessage = "Select Account Type" : RFVAccountType.InitialValue = "0"
            RFVSalesPerson.ErrorMessage = "Select Sales person" : RFVSalesPerson.InitialValue = "0"
            RFVRouteNo.ErrorMessage = "Enter Route number" : REVRouteNo.ErrorMessage = "Enter Route number" : REVRouteNo.ValidationExpression = "^[0-9A-Za-z]*$"
            RFVColDate.ControlToValidate = "txtCollectedDate" : RFVColDate.ErrorMessage = "Select Date from Calendar."
            REVColDate.ErrorMessage = "Enter valid Date." : REVColDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            RFVProDate.ControlToValidate = "txtProDate" : RFVProDate.ErrorMessage = "Select Date from Calendar."
            REVProDate.ErrorMessage = "Enter valid Date." : REVProDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
        Catch ex As Exception
            Throw
        End Try
    End Sub


    Public Sub BindExistingCNO()
        Try
            ddlExeChequeNo.DataSource = ObjDB.LoadExeChequeNo(sSession.AccessCode, txtChequeNo.Text)
            ddlExeChequeNo.DataTextField = "ACM_ChequeNo"
            ddlExeChequeNo.DataValueField = "ACM_ID"
            ddlExeChequeNo.DataBind()
            ddlExeChequeNo.Items.Insert(0, "Select Cheque No.")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingCNO")
        End Try
    End Sub
    Public Sub GenSerialNo()
        Dim dt As New DataTable
        Dim iMaxID As Integer
        Dim sMaxID As String
        Dim sIDdate As String
        Try
            iMaxID = ObjDB.GetMaxID(sSession.AccessCode, sSession.AccessCodeID)

            If iMaxID = 0 Then
                sMaxID = "001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If

            sIDdate = Date.Now.ToString("yyyyMMdd")
            txtSerialNo.Text = sIDdate & sMaxID

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenSerialNo")
        End Try
    End Sub
    Public Sub SaveOrUpdateChequeDetails()
        Dim Arr() As String

        Try
            If ddlExeChequeNo.SelectedIndex > 0 Then
                ObjDB.iACM_ID = ddlExeChequeNo.SelectedValue
            Else
                ObjDB.iACM_ID = 0
            End If
            ObjDB.sACM_SerialNo = txtSerialNo.Text
            ObjDB.iACM_Party = ddlPartyCode.SelectedValue
            ObjDB.iACM_Bank = ddlBankName.SelectedValue
            ObjDB.iACM_Branch = ddlBranchName.SelectedValue
            ObjDB.sACM_IFSCCode = txtIFCScode.Text
            ObjDB.sACM_Pay = txtPayto.Text
            ObjDB.sACM_Rupees = txtRupees.Text
            ObjDB.mACM_Amount = txtAmount.Text
            ObjDB.iACM_ChequeNo = txtChequeNo.Text
            ObjDB.sACM_ChequeDate = txtChequeDate.Text
            ObjDB.sACM_AccountNo = txtAccountNo.Text
            ObjDB.iACM_AccountType = ddlAccountType.SelectedValue
            ObjDB.iACM_SalesPerson = ddlSalesPerson.SelectedValue
            ObjDB.sACM_RouteNo = txtRouteNo.Text
            ObjDB.sACM_CollectedDate = txtCollectedDate.Text
            ObjDB.sACM_ProducedDate = txtProDate.Text
            ObjDB.sACM_Summary = mtxtSummary.Text
            ObjDB.sACM_MICRCode = txtMicrCode.Text
            ObjDB.iACM_LeafNo = txtLeafNo.Text
            ObjDB.iACM_CompID = sSession.AccessCodeID
            ObjDB.iACM_YearID = sSession.YearID
            ObjDB.sACM_Status = "A"
            ObjDB.iACM_CreatedBy = sSession.UserID
            ObjDB.iACM_UpdatedBy = sSession.UserID
            ObjDB.iACM_ApprovedBy = sSession.UserID
            ObjDB.sACM_Delflag = "N"

            If iAttachID = 0 Then
                ObjDB.iACM_AttachID = 0
            Else
                ObjDB.iACM_AttachID = iAttachID
            End If
            Arr = ObjDB.SaveChequeDetails(ObjDB, sSession.AccessCode)


            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated." : lblCustomerValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved." : lblCustomerValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveOrUpdateChequeDetails")
        End Try
    End Sub
    Public Sub BindExeChequeDetails(ByVal iACM_ID As Integer)
        Dim dt As New DataTable
        Try
            If ddlExeChequeNo.SelectedIndex > 0 Then
                dt = ObjDB.LoadExeChequeDetails(ddlExeChequeNo.SelectedValue, sSession.AccessCode)
                If dt.Rows.Count <> 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        If IsDBNull(dt.Rows(i).Item("ACM_ChequeNo")) = False Then
                            txtChequeNo.Text = dt.Rows(i).Item("ACM_ChequeNo")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_SerialNo")) = False Then
                            txtSerialNo.Text = dt.Rows(i).Item("ACM_SerialNo")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Party")) = False Then
                            ddlPartyCode.SelectedValue = dt.Rows(i).Item("ACM_Party")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_ChequeDate")) = False Then
                            txtChequeDate.Text = dt.Rows(i).Item("ACM_ChequeDate")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_IFSCCode")) = False Then
                            txtIFCScode.Text = dt.Rows(i).Item("ACM_IFSCCode")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_AccountNo")) = False Then
                            txtAccountNo.Text = dt.Rows(i).Item("ACM_AccountNo")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_MICRCode")) = False Then
                            txtMicrCode.Text = dt.Rows(i).Item("ACM_MICRCode")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_LeafNo")) = False Then
                            txtLeafNo.Text = dt.Rows(i).Item("ACM_LeafNo")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Bank")) = False Then
                            ddlBankName.SelectedValue = dt.Rows(i).Item("ACM_Bank")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Branch")) = False Then
                            ddlBranchName.SelectedValue = dt.Rows(i).Item("ACM_Branch")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Pay")) = False Then
                            txtPayto.Text = dt.Rows(i).Item("ACM_Pay")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Rupees")) = False Then
                            txtRupees.Text = dt.Rows(i).Item("ACM_Rupees")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Amount")) = False Then
                            txtAmount.Text = dt.Rows(i).Item("ACM_Amount")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_AccountType")) = False Then
                            ddlAccountType.SelectedValue = dt.Rows(i).Item("ACM_AccountType")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_SalesPerson")) = False Then
                            ddlSalesPerson.SelectedValue = dt.Rows(i).Item("ACM_SalesPerson")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_RouteNo")) = False Then
                            txtRouteNo.Text = dt.Rows(i).Item("ACM_RouteNo")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_CollectedDate")) = False Then
                            txtCollectedDate.Text = dt.Rows(i).Item("ACM_CollectedDate")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_ProducedDate")) = False Then
                            txtProDate.Text = dt.Rows(i).Item("ACM_ProducedDate")
                        End If
                        If IsDBNull(dt.Rows(i).Item("ACM_Summary")) = False Then
                            mtxtSummary.Text = dt.Rows(i).Item("ACM_Summary")
                        End If
                        iAttachID = 0 : lblBadgeCount.Text = 0
                        dgAttach.DataSource = Nothing
                        dgAttach.DataBind()
                        If IsDBNull(dt.Rows(0).Item("ACM_AtachID")) = False Then
                            iAttachID = dt.Rows(0).Item("ACM_AtachID")
                            BindAllAttachments(iAttachID)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExeChequeDetails")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            SaveOrUpdateChequeDetails()
            BindExistingCNO()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub ddlExeChequeNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExeChequeNo.SelectedIndexChanged
        Try
            If ddlExeChequeNo.SelectedIndex > 0 Then
                BindExeChequeDetails(ddlExeChequeNo.SelectedValue)
                If sCDSave = "YES" Then
                    imgbtnUpdate.Visible = True
                    imgbtnDelete.Visible = True
                End If
                imgbtnSave.Visible = False
                imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
            Else
                txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFCScode.Text = "" : txtAccountNo.Text = "" : txtMicrCode.Text = "" : txtLeafNo.Text = "" : ddlPartyCode.SelectedIndex = 0
                ddlBankName.SelectedIndex = 0 : ddlBranchName.SelectedIndex = 0 : txtPayto.Text = "" : txtRupees.Text = "" : txtAmount.Text = "" : ddlAccountType.SelectedIndex = 0
                ddlSalesPerson.SelectedIndex = 0 : txtRouteNo.Text = "" : txtCollectedDate.Text = "" : txtProDate.Text = "" : mtxtSummary.Text = "" : txtSerialNo.Text = ""

                'imgbtnSave.Visible = True
                imgbtnUpdate.Visible = False : imgbtnDelete.Visible = False
                GenSerialNo()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExeChequeNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            SaveOrUpdateChequeDetails()
            BindExistingCNO()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            ddlExeChequeNo.SelectedIndex = 0
            txtSerialNo.Text = "" : ddlPartyCode.SelectedIndex = 0 : txtCollectedDate.Text = "" : txtProDate.Text = "" : ddlSalesPerson.SelectedIndex = 0 : txtRouteNo.Text = ""
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFCScode.Text = "" : txtAccountNo.Text = "" : txtMicrCode.Text = "" : txtLeafNo.Text = "" : mtxtSummary.Text = ""
            ddlBankName.SelectedIndex = 0 : ddlBranchName.SelectedIndex = 0 : txtPayto.Text = "" : txtRupees.Text = "" : txtAmount.Text = "" : ddlAccountType.SelectedIndex = 0

            GenSerialNo()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Try
            If ddlExeChequeNo.SelectedIndex > 0 Then
                ObjDB.DelChkDetails(sSession.AccessCode, sSession.UserID, ddlExeChequeNo.SelectedValue)

                lblError.Text = "Successfully Deleted" : lblCustomerValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDelete_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("PostDatedCheDetails.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    'Protected Sub txtChequeDate_TextChanged(sender As Object, e As EventArgs) Handles txtChequeDate.TextChanged
    '    Dim dDate, dSDate As Date
    '    Try

    '        If txtChequeDate.Text.Trim = "" Then
    '            lblError.Text = "Select Date from Calendar" : lblCustomerValidationMsg.Text = "Select Date from Calendar"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtChequeDate.Focus()
    '            Exit Try
    '        Else
    '            Try
    '                dSDate = DateTime.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            Catch ex As Exception
    '                lblError.Text = "Select Valid Date from Calendar" : lblCustomerValidationMsg.Text = "Select Valid Date from Calendar"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '                txtChequeDate.Focus()
    '                Exit Try
    '            End Try
    '        End If

    '        dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '        Dim l As Integer
    '        l = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If l < 0 Then
    '            lblCustomerValidationMsg.Text = "Selected Date should be greater than or equal to Current Date."
    '            lblError.Text = "Selected Date should be greater than or equal to Current Date."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtChequeDate.Focus()
    '            Exit Try
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtChequeDate_TextChanged")
    '    End Try
    'End Sub
    'Private Sub txtCollectedDate_TextChanged(sender As Object, e As EventArgs) Handles txtCollectedDate.TextChanged
    '    Dim dCDate, dClDate As Date
    '    Try
    '        If txtCollectedDate.Text.Trim = "" Then
    '            lblError.Text = "Select Date from Calendar" : lblCustomerValidationMsg.Text = "Select Date from Calendar"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtCollectedDate.Focus()
    '            Exit Try
    '        Else
    '            Try
    '                dClDate = DateTime.ParseExact(txtCollectedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            Catch ex As Exception
    '                lblError.Text = "Select Valid Date from Calendar" : lblCustomerValidationMsg.Text = "Select Valid Date from Calendar"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '                txtCollectedDate.Focus()
    '                Exit Try
    '            End Try
    '        End If

    '        dCDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dClDate = Date.ParseExact(txtCollectedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '        Dim k As Integer
    '        k = DateDiff(DateInterval.Day, dCDate, dClDate)
    '        If k < 0 Then
    '            lblCustomerValidationMsg.Text = "Selected Date should be greater than or equal to Current Date."
    '            lblError.Text = "Selected Date should be greater than or equal to Current Date.."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtCollectedDate.Focus()
    '            Exit Try
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtCollectedDate_TextChanged")
    '    End Try
    'End Sub

    'Private Sub txtProDate_TextChanged(sender As Object, e As EventArgs) Handles txtProDate.TextChanged
    '    Dim dPDate, dPrDate As Date
    '    Try
    '        If txtProDate.Text.Trim = "" Then
    '            lblError.Text = "Select Tentative Start Date." : lblCustomerValidationMsg.Text = "Select Tentative Start Date."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtProDate.Focus()
    '            Exit Try
    '        Else
    '            Try
    '                dPrDate = DateTime.ParseExact(txtProDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '            Catch ex As Exception
    '                lblError.Text = "Enter valid Tentative Start Date." : lblCustomerValidationMsg.Text = "Enter valid Tentative Start Date."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
    '                txtProDate.Focus()
    '                Exit Try
    '            End Try
    '        End If

    '        dPDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dPrDate = Date.ParseExact(txtProDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '        Dim j As Integer
    '        j = DateDiff(DateInterval.Day, dPDate, dPrDate)
    '        If j < 0 Then
    '            lblCustomerValidationMsg.Text = "Tentative Start Date should be greater than or equal to Current Date."
    '            lblError.Text = "Tentative Start Date should be greater than or equal to Current Date."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtProDate.Focus()
    '            Exit Try
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtProDate_TextChanged")
    '    End Try
    'End Sub
    Private Sub BindAllAttachments(ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            dgAttach.PageSize = 1000
            ds = objclsAttachments.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            dgAttach.DataSource = ds
            dgAttach.DataBind()
            lblBadgeCount.Text = dgAttach.Items.Count
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnaddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblMsg.Text = "" : iDocID = 0
            If Not (txtfile.PostedFile Is Nothing) And txtfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                    Exit Sub
                End If
                lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iAttachID > 0 Then
                        BindAllAttachments(iAttachID)
                    End If
                Else
                    lblMsg.Text = "No file to Attach."
                End If
            Else
                lblMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnaddAttch_Click")
        End Try
    End Sub
    Protected Sub btnAddDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesc.Click
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
            BindAllAttachments(iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click")
        End Try
    End Sub
    Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
        Dim lblStatus As New Label
        Dim imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound")
        End Try
    End Sub
    Protected Sub dgAttach_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = e.Item.FindControl("lblFDescription")
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand")
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
End Class
