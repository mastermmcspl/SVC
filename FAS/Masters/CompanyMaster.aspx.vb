Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_CompanyMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_CompanyMaster"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Private Shared sCMSave As String
    Dim objCompMaster As New clsCompanyMaster
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        'imgbtnAddOther.ImageUrl = "~/Images/Add24.png"
        'imgbtnAddBranch.ImageUrl = "~/Images/Add24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "COMS")
                imgbtnUpdate.Visible = False : btnAdd.Visible = False : btnSave.Visible = False : btnBranchSave.Visible = False : sCMSave = "NO"

                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnUpdate.Visible = True
                        btnAdd.Visible = True
                        btnSave.Visible = True
                        btnBranchSave.Visible = True
                        sCMSave = "YES"
                    End If
                    'If sFormButtons.Contains(",Report,") = True Then
                    '    imgbtnReport.Visible = True
                    'End If
                End If

                If sSession.YearID = 0 Then
                    lblError.Text = "Set the Financial Year in Holiday & Year Master."
                    Exit Sub
                End If

                'BindCategory()
                LoadBranchCompanyType()
                BindAllDropDown() : BindBranchDetails()
                lblCompCode.Text = objCompMaster.GetCompanyNameAndCode(sSession.AccessCode, "Sad_CompanyMaster_Settings", "Sad_CMS_AccessCode")
                txtCompanyName.Text = objCompMaster.GetCompanyNameAndCode(sSession.AccessCode, "Sad_CompanyMaster_Settings", "Sad_CMS_Name")
                If lblCompCode.Text <> "" Then
                    BindCompanyDetails()
                End If
                divcollapseOtherDetails.Visible = True : divcollapseBranchDetails.Visible = True

                RFVCompanyName.ErrorMessage = "Enter Company Name."
                RFVemail.ErrorMessage = "Enter E-Mail." : REVemail.ErrorMessage = "Enter valid E-Mail." : REVemail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                RFVCompanyType.InitialValue = "Select Company Type" : RFVCompanyType.ErrorMessage = "Select Company Type"
                'RFVSalesType.InitialValue = "Select Sales Type" : RFVSalesType.ErrorMessage = "Select Sales Type"

                RFVBillAddress.ErrorMessage = "Enter Bill Address."
                REVBillPostalCode.ValidationExpression = "[0-9]{6}" : REVBillPostalCode.ErrorMessage = "Enter Valid Postal Code."
                RFVBillCity.InitialValue = "Select Billing City" : RFVBillCity.ErrorMessage = "Select Billing City"
                RFVBillState.InitialValue = "Select Billing State" : RFVBillState.ErrorMessage = "Select Billing State"
                RFVBillCountry.InitialValue = "Select Billing Country" : RFVBillCountry.ErrorMessage = "Select Billing Country"
                REVBillTelphone.ValidationExpression = "[0-9]{10}" : REVBillTelphone.ErrorMessage = "Enter Valid Telephone no."

                RFVDelAddress.ErrorMessage = "Enter Bill Address."
                REVDelPostalCode.ValidationExpression = "[0-9]{6}" : REVDelPostalCode.ErrorMessage = "Enter Valid Postal Code."
                RFVDelCity.InitialValue = "Select Delivery City" : RFVDelCity.ErrorMessage = "Select Delivery City"
                RFVDelState.InitialValue = "Select Delivery State" : RFVDelState.ErrorMessage = "Select Delivery State"
                RFVDelCountry.InitialValue = "Select Delivery Country" : RFVDelCountry.ErrorMessage = "Select Delivery Country"
                REVDelTelephone.ValidationExpression = "[0-9]{10}" : REVDelTelephone.ErrorMessage = "Enter Valid Telephone no."

                RFVBranchName.ErrorMessage = "Enter Branch Name"
                RFVBranchAddress.ErrorMessage = "Enter Branch Address"
                REVBranchPostalCode.ValidationExpression = "[0-9]{6}" : REVBranchPostalCode.ErrorMessage = "Enter Valid Postal Code."
                RFVBranchCity.InitialValue = "Select City" : RFVBranchCity.ErrorMessage = "Select City"
                RFVBranchState.InitialValue = "Select State" : RFVBranchState.ErrorMessage = "Select State"
                RFVBranchCountry.InitialValue = "Select Country" : RFVBranchCountry.ErrorMessage = "Select Country"
                RFVBContactPerson.ErrorMessage = "Enter Branch Contact Person"
                REVBranchPhno.ValidationExpression = "[0-9]{10}" : REVBranchPhno.ErrorMessage = "Enter Valid Telephone no."

                REVddlBnkName.InitialValue = "SElect BankName" : REVddlBnkName.ErrorMessage = "SElect BankName"

                RFVddlBranchGSTNCategory.InitialValue = "Select" : RFVddlBranchGSTNCategory.ErrorMessage = "Select GSTN Category For Branch"
                RFVddlCategory.InitialValue = "Select" : RFVddlCategory.ErrorMessage = "Select GSTN Category"
                LoadBranches()
                'REVProvisionNo.ValidationExpression = "/^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}Z[0-9]{1}?$/"
                'REVProvisionNo.ErrorMessage = "Enter Valid Billing GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One integer)."

                'Me.imgbtnUpdate.Attributes.Add("OnClick", "return validation()")
                'Me.btnAdd.Attributes.Add("onclick", "javascript:return validationStatutoryRef();")
                'Me.btnBranchSave.Attributes.Add("OnClick", "return Branchvalidation()")

                BindBankDetails()
                LoadBanksName()
                divbankdetails.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadBranches()
        Dim dt As New DataTable
        Try
            dt = objCompMaster.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count > 0 Then
                ddlBranch.DataSource = dt
                ddlBranch.DataTextField = "Org_Name"
                ddlBranch.DataValueField = "Org_Node"
                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, "Select Branch")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadBranchCompanyType()
        Try
            ddlBranchCompanyType.DataSource = objCompMaster.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranchCompanyType.DataTextField = "Mas_Desc"
            ddlBranchCompanyType.DataValueField = "Mas_Id"
            ddlBranchCompanyType.DataBind()
            ddlBranchCompanyType.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindAllDropDown()
        Dim dtCity As New DataTable
        Dim dtState As New DataTable
        Dim dtCountry As New DataTable
        Try
            ddlCompanyType.DataSource = objCompMaster.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select Company Type")

            dtCity = objCompMaster.LoadCity(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillCity.DataSource = dtCity
            ddlBillCity.DataTextField = "Mas_Desc"
            ddlBillCity.DataValueField = "Mas_Id"
            ddlBillCity.DataBind()
            ddlBillCity.Items.Insert(0, "Select Billing City")

            ddlDelCity.DataSource = dtCity
            ddlDelCity.DataTextField = "Mas_Desc"
            ddlDelCity.DataValueField = "Mas_Id"
            ddlDelCity.DataBind()
            ddlDelCity.Items.Insert(0, "Select Delivery City")

            dtState = objCompMaster.LoadState(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillState.DataSource = dtState
            ddlBillState.DataTextField = "Mas_Desc"
            ddlBillState.DataValueField = "Mas_Id"
            ddlBillState.DataBind()
            ddlBillState.Items.Insert(0, "Select Billing State")

            ddlDelState.DataSource = dtState
            ddlDelState.DataTextField = "Mas_Desc"
            ddlDelState.DataValueField = "Mas_Id"
            ddlDelState.DataBind()
            ddlDelState.Items.Insert(0, "Select Delivery State")

            dtCountry = objCompMaster.LoadCountry(sSession.AccessCode, sSession.AccessCodeID)
            ddlBillCountry.DataSource = dtCountry
            ddlBillCountry.DataTextField = "Mas_Desc"
            ddlBillCountry.DataValueField = "Mas_Id"
            ddlBillCountry.DataBind()
            ddlBillCountry.Items.Insert(0, "Select Billing Country")

            ddlDelCountry.DataSource = dtCountry
            ddlDelCountry.DataTextField = "Mas_Desc"
            ddlDelCountry.DataValueField = "Mas_Id"
            ddlDelCountry.DataBind()
            ddlDelCountry.Items.Insert(0, "Select Delivery Country")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Try
            objCompMaster.CUST_NAME = objGen.SafeSQL(Trim(txtCompanyName.Text))
            objCompMaster.CUST_CODE = objGen.SafeSQL(Trim(lblCompCode.Text))
            objCompMaster.CUST_EMAIL = objGen.SafeSQL(Trim(txtEmail.Text))

            If ddlCompanyType.SelectedIndex > 0 Then
                objCompMaster.CUST_INDTYPEID = ddlCompanyType.SelectedValue
            Else
                objCompMaster.CUST_INDTYPEID = 0
            End If

            objCompMaster.CUST_COMM_ADDRESS = objGen.SafeSQL(LTrim(txtBillAddress.Text))

            If ddlBillCity.SelectedIndex > 0 Then
                objCompMaster.CUST_COMM_CITY = ddlBillCity.SelectedValue
            Else
                objCompMaster.CUST_COMM_CITY = 0
            End If

            objCompMaster.CUST_COMM_PIN = objGen.SafeSQL(txtBillPostalCode.Text)

            If ddlBillState.SelectedIndex > 0 Then
                objCompMaster.CUST_COMM_STATE = ddlBillState.SelectedValue
            Else
                objCompMaster.CUST_COMM_STATE = 0
            End If

            If ddlBillCountry.SelectedIndex > 0 Then
                objCompMaster.CUST_COMM_COUNTRY = ddlBillCountry.SelectedValue
            Else
                objCompMaster.CUST_COMM_COUNTRY = 0
            End If

            objCompMaster.CUST_COMM_FAX = objGen.SafeSQL(Trim(txtBillFax.Text))
            objCompMaster.CUST_COMM_TEL = objGen.SafeSQL(Trim(txtBillTelphone.Text))

            objCompMaster.CUST_ADDRESS = objGen.SafeSQL(Trim(txtDelAddress.Text))

            If ddlDelCity.SelectedIndex > 0 Then
                objCompMaster.CUST_CITY = ddlDelCity.SelectedValue
            Else
                objCompMaster.CUST_CITY = 0
            End If
            objCompMaster.CUST_PIN = objGen.SafeSQL(txtDelPostalCode.Text)

            If ddlDelState.SelectedIndex > 0 Then
                objCompMaster.CUST_STATE = ddlDelState.SelectedValue
            Else
                objCompMaster.CUST_STATE = 0
            End If

            If ddlDelCountry.SelectedIndex > 0 Then
                objCompMaster.CUST_COUNTRY = ddlDelCountry.SelectedValue
            Else
                objCompMaster.CUST_COUNTRY = 0
            End If

            objCompMaster.CUST_FAX = objGen.SafeSQL(Trim(txtDelFax.Text))
            objCompMaster.CUST_TELPHONE = objGen.SafeSQL(Trim(txtDelTelephone.Text))
            objCompMaster.CUST_STATUS = "C"
            objCompMaster.CUST_DELFLG = "W"
            objCompMaster.CUST_CRON = Date.Today
            objCompMaster.CUST_CRBY = sSession.UserID
            'If ddlSalesType.SelectedIndex > 0 Then
            '    objCompMaster.Cust_SaleType = ddlSalesType.SelectedValue
            'Else
            '    objCompMaster.Cust_SaleType = 0
            'End If

            objCompMaster.CUST_CompID = sSession.AccessCodeID
            objCompMaster.CUST_UpdatedBy = sSession.UserID
            objCompMaster.CUST_UpdatedOn = Date.Today
            objCompMaster.CUST_Operation = "C"
            objCompMaster.CUST_IPAddress = sSession.IPAddress
            objCompMaster.CUST_COMM_PhFirst = objGen.SafeSQL(Trim(txtBillTelphone1.Text))
            objCompMaster.CUST_COMM_PhSecond = objGen.SafeSQL(Trim(txtBillTelphone2.Text))
            objCompMaster.CUST_PhFirst = objGen.SafeSQL(Trim(txtDelTelephone1.Text))
            objCompMaster.CUST_PhSecond = objGen.SafeSQL(Trim(txtDelTelephone2.Text))

            If ddlCategory.SelectedIndex > 0 Then
                objCompMaster.CUST_TAXPayableCategory = ddlCategory.SelectedValue
            Else
                objCompMaster.CUST_TAXPayableCategory = 0
            End If

            'If ddlForm.SelectedIndex > 0 Then
            '    objCompMaster.CUST_GSTRForm = ddlForm.SelectedValue
            'Else
            '    objCompMaster.CUST_GSTRForm = 0
            'End If

            'If ddlPeriodicity.SelectedIndex > 0 Then
            '    objCompMaster.CUST_Periodicity = ddlPeriodicity.SelectedValue
            'Else
            '    objCompMaster.CUST_Periodicity = 0
            'End If

            objCompMaster.CUST_GSTRForm = 0
            objCompMaster.CUST_Periodicity = 0

            If txtProvisionNo.Text <> "" Then
                objCompMaster.CUST_ProvisionalNo = txtProvisionNo.Text
            Else
                objCompMaster.CUST_ProvisionalNo = ""
            End If

            If txtFinalNo.Text <> "" Then
                objCompMaster.CUST_FinalNo = txtFinalNo.Text
            Else
                objCompMaster.CUST_FinalNo = ""
            End If

            Arr = objCompMaster.SaveCumsterDetails(sSession.AccessCode, objCompMaster)
            objCompMaster.UpdateCompanyName(sSession.AccessCode, txtCompanyName.Text)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved.','', 'success');", True)
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdate_Click")
        End Try
    End Sub

    Private Sub BindCompanyDetails()
        Dim dt As New DataTable
        Try
            If lblCompCode.Text <> "" Then
                dt = objCompMaster.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, lblCompCode.Text)
                If dt.Rows.Count > 0 Then

                    If IsDBNull(dt.Rows(0).Item("CUST_NAME").ToString()) = False Then
                        txtCompanyName.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_NAME").ToString())
                    Else
                        txtCompanyName.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_EMAIL").ToString()) = False Then
                        txtEmail.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_EMAIL").ToString())
                    Else
                        txtEmail.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_ADDRESS").ToString()) = False Then
                        txtBillAddress.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_ADDRESS").ToString())
                    Else
                        txtBillAddress.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_CITY").ToString()) = False Then
                        If dt.Rows(0).Item("CUST_COMM_CITY").ToString() <> "" Then
                            ddlBillCity.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_CITY").ToString())
                        Else
                            ddlBillCity.SelectedIndex = 0
                        End If
                    Else
                        ddlBillCity.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_PIN").ToString()) = False Then
                        txtBillPostalCode.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_PIN").ToString())
                    Else
                        txtBillPostalCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_STATE").ToString()) = False Then
                        ddlBillState.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_STATE").ToString())
                    Else
                        ddlBillState.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_COUNTRY").ToString()) = False Then
                        ddlBillCountry.SelectedValue = dt.Rows(0).Item("CUST_COMM_COUNTRY").ToString()
                    Else
                        ddlBillCountry.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_FAX").ToString()) = False Then
                        txtBillFax.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_FAX").ToString())
                    Else
                        txtBillFax.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_TEL").ToString()) = False Then
                        txtBillTelphone.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_TEL").ToString())
                    Else
                        txtBillTelphone.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_ADDRESS").ToString()) = False Then
                        txtDelAddress.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_ADDRESS").ToString())
                    Else
                        txtDelAddress.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_CITY").ToString()) = False Then
                        If dt.Rows(0).Item("CUST_CITY").ToString() <> "" Then
                            ddlDelCity.SelectedValue = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_CITY").ToString())
                        Else
                            ddlDelCity.SelectedIndex = 0
                        End If
                    Else
                        ddlDelCity.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_PIN").ToString()) = False Then
                        txtDelPostalCode.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_PIN").ToString())
                    Else
                        txtDelPostalCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_STATE").ToString()) = False Then
                        If dt.Rows(0).Item("CUST_STATE").ToString() <> "" Then
                            ddlDelState.SelectedValue = dt.Rows(0).Item("CUST_STATE").ToString()
                        Else
                            ddlDelState.SelectedIndex = 0
                        End If
                    Else
                        ddlDelState.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COUNTRY").ToString()) = False Then
                        If dt.Rows(0).Item("CUST_COUNTRY").ToString() <> "" Then
                            ddlDelCountry.SelectedValue = dt.Rows(0).Item("CUST_COUNTRY").ToString()
                        Else
                            ddlDelCountry.SelectedIndex = 0
                        End If
                    Else
                        ddlDelCountry.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_FAX").ToString()) = False Then
                        txtDelFax.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_FAX").ToString())
                    Else
                        txtDelFax.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_TELPHONE").ToString()) = False Then
                        txtDelTelephone.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_TELPHONE").ToString())
                    Else
                        txtDelTelephone.Text = ""
                    End If

                    'If dt.Rows(0).Item("Cust_SaleType") <> 0 Then
                    '    ddlSalesType.SelectedValue = dt.Rows(0).Item("Cust_SaleType")
                    'Else
                    '    ddlSalesType.SelectedValue = 0
                    'End If

                    If IsDBNull(dt.Rows(0).Item("CUST_INDTYPEID")) = False Then
                        If dt.Rows(0).Item("CUST_INDTYPEID") > 0 Then
                            ddlCompanyType.SelectedValue = dt.Rows(0).Item("CUST_INDTYPEID")
                        Else
                            ddlCompanyType.SelectedValue = 0
                        End If
                    Else
                        ddlCompanyType.SelectedValue = 0
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_PhFirst").ToString()) = False Then
                        txtBillTelphone1.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_PhFirst").ToString())
                    Else
                        txtBillTelphone1.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_COMM_PhSecond").ToString()) = False Then
                        txtBillTelphone2.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_COMM_PhSecond").ToString())
                    Else
                        txtBillTelphone2.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_PhFirst").ToString()) = False Then
                        txtDelTelephone1.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_PhFirst").ToString())
                    Else
                        txtDelTelephone1.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_PhSecond").ToString()) = False Then
                        txtDelTelephone2.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_PhSecond").ToString())
                    Else
                        txtDelTelephone2.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_TAXPayableCategory")) = False Then
                        If dt.Rows(0).Item("CUST_TAXPayableCategory") > 0 Then
                            BindCategory(ddlCompanyType.SelectedItem.Text)
                            ddlCategory.SelectedValue = dt.Rows(0).Item("CUST_TAXPayableCategory")
                        Else
                            ddlCategory.SelectedIndex = 0
                        End If
                    Else
                        ddlCategory.SelectedIndex = 0
                    End If

                    'If IsDBNull(dt.Rows(0).Item("CUST_GSTRForm")) = False Then
                    '    If dt.Rows(0).Item("CUST_GSTRForm") > 0 Then
                    '        ddlForm.SelectedValue = dt.Rows(0).Item("CUST_GSTRForm")
                    '    Else
                    '        ddlForm.SelectedIndex = 0
                    '    End If
                    'Else
                    '    ddlForm.SelectedIndex = 0
                    'End If

                    'If IsDBNull(dt.Rows(0).Item("CUST_Periodicity")) = False Then
                    '    If dt.Rows(0).Item("CUST_Periodicity") > 0 Then
                    '        ddlPeriodicity.SelectedValue = dt.Rows(0).Item("CUST_Periodicity")
                    '    Else
                    '        ddlPeriodicity.SelectedIndex = 0
                    '    End If
                    'Else
                    '    ddlPeriodicity.SelectedIndex = 0
                    'End If

                    If IsDBNull(dt.Rows(0).Item("CUST_ProvisionalNo").ToString()) = False Then
                        txtProvisionNo.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_ProvisionalNo").ToString())
                    Else
                        txtProvisionNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0).Item("CUST_FinalNo").ToString()) = False Then
                        txtFinalNo.Text = objGen.ReplaceSafeSQL(dt.Rows(0).Item("CUST_FinalNo").ToString())
                    Else
                        txtFinalNo.Text = ""
                    End If

                    Dim taxcategory As String
                    If ddlCategory.SelectedIndex > 0 Then
                        taxcategory = objCompMaster.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCategory.SelectedValue)
                        If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                            txtProvisionNo.Enabled = False
                        Else
                            txtProvisionNo.Enabled = True
                        End If
                    End If

                End If
            End If
            dgOtherDetails.DataSource = objCompMaster.LoadOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgOtherDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCompanyDetails")
        End Try
    End Sub
    'Private Sub imgbtnAddOther_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddOther.Click
    '    Try
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalOther').modal('show');", True)
    '        dgOtherDetails.DataSource = objCompMaster.LoadOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
    '        dgOtherDetails.DataBind()
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddOther_Click")
    '    End Try
    'End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim iCheckID As Integer = 0
        Try
            If txtName.Text.Trim = "" Then
                txtName.Focus()
                lblError.Text = "Enter Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Name.','', 'info');", True)
                Exit Sub
            End If

            If txtValue.Text.Trim = "" Then
                txtValue.Focus()
                lblError.Text = "Enter Value."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Value.','', 'info');", True)
                Exit Sub
            End If

            iCheckID = objCompMaster.CheckValue(sSession.AccessCode, sSession.AccessCodeID, txtName.Text)
            If txtName.Text <> "" And txtValue.Text <> "" Then
                objCompMaster.SaveOtherDetails(sSession.AccessCode, sSession.AccessCodeID, objGen.SafeSQL(txtName.Text), objGen.SafeSQL(txtValue.Text), iCheckID)
                txtName.Text = "" : txtValue.Text = ""
                lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved.','', 'success');", True)
            End If

            dgOtherDetails.DataSource = objCompMaster.LoadOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgOtherDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click")
        End Try
    End Sub
    Private Sub dgOtherDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgOtherDetails.ItemCommand
        Try
            If e.CommandName = "Delete" Then
                objCompMaster.DeleteOtherDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text)
            End If
            dgOtherDetails.DataSource = objCompMaster.LoadOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            dgOtherDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgOtherDetails_ItemCommand")
        End Try
    End Sub
    Private Sub BindBranchDetails()
        Try
            ddlBranchCity.DataSource = objCompMaster.LoadCity(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranchCity.DataTextField = "Mas_Desc"
            ddlBranchCity.DataValueField = "Mas_Id"
            ddlBranchCity.DataBind()
            ddlBranchCity.Items.Insert(0, "Select City")

            ddlBranchState.DataSource = objCompMaster.LoadState(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranchState.DataTextField = "Mas_Desc"
            ddlBranchState.DataValueField = "Mas_Id"
            ddlBranchState.DataBind()
            ddlBranchState.Items.Insert(0, "Select State")

            ddlBranchCountry.DataSource = objCompMaster.LoadCountry(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranchCountry.DataTextField = "Mas_Desc"
            ddlBranchCountry.DataValueField = "Mas_Id"
            ddlBranchCountry.DataBind()
            ddlBranchCountry.Items.Insert(0, "Select Country")

            grdBranch.DataSource = objCompMaster.LoadBranchDetails(sSession.AccessCode, sSession.AccessCodeID, 0)
            grdBranch.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBranch').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub grdBranch_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles grdBranch.ItemCommand
        Dim dt As New DataTable
        Dim taxcategory As String
        Try
            If e.CommandName = "Select" Then
                dt = objCompMaster.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        If IsDBNull(dt.Rows(i)("CUSTB_Name").ToString()) = False Then
                            If dt.Rows(i)("CUSTB_Name").ToString() > 0 Then
                                ddlBranch.SelectedValue = dt.Rows(i)("CUSTB_Name").ToString()
                            Else
                                ddlBranch.SelectedIndex = 0
                            End If
                        Else
                            ddlBranch.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_ContactPerson").ToString()) = False Then
                            txtContactPerson.Text = dt.Rows(i)("CUSTB_ContactPerson").ToString()
                        Else
                            txtContactPerson.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_CITY").ToString()) = False Then
                            ddlBranchCity.SelectedValue = dt.Rows(i)("CUSTB_CITY").ToString()
                        Else
                            ddlBranchCity.SelectedIndex = 0
                        End If

                        'If IsDBNull(dt.Rows(i)("CUSTB_STATE").ToString()) = False Then
                        '    ddlBranchState.SelectedValue = dt.Rows(i)("CUSTB_STATE").ToString()
                        'Else
                        '    ddlBranchState.SelectedIndex = 0
                        'End If
                        If IsDBNull(dt.Rows(i)("CUSTB_STATE").ToString()) = False Then
                            If dt.Rows(i)("CUSTB_STATE") > 0 Then
                                ddlBranchState.SelectedValue = dt.Rows(i)("CUSTB_STATE").ToString()
                            Else
                                ddlBranchState.SelectedIndex = 0
                            End If
                            '    'ddlBranchState.SelectedValue = dt.Rows(i)("CUSTB_STATE").ToString()
                            'Else
                            '    ddlBranchState.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_COUNTRY").ToString()) = False Then
                            ddlBranchCountry.SelectedValue = dt.Rows(i)("CUSTB_COUNTRY").ToString()
                        Else
                            ddlBranchCountry.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_PIN").ToString()) = False Then
                            txtBranchPostalCode.Text = dt.Rows(i)("CUSTB_PIN").ToString()
                        Else
                            txtBranchPostalCode.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_TEL").ToString()) = False Then
                            txtBranchPhoneNo.Text = dt.Rows(i)("CUSTB_TEL").ToString()
                        Else
                            txtBranchPhoneNo.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_FAX").ToString()) = False Then
                            txtBranchFax.Text = dt.Rows(i)("CUSTB_FAX").ToString()
                        Else
                            txtBranchFax.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_ADDRESS").ToString()) = False Then
                            txtBranchAddress.Text = dt.Rows(i)("CUSTB_ADDRESS").ToString()
                        Else
                            txtBranchAddress.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_CompanyType")) = False Then
                            ddlBranchCompanyType.SelectedValue = dt.Rows(i)("CUSTB_CompanyType")
                        Else
                            ddlBranchCompanyType.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_GSTNCategory")) = False Then
                            BindBranchGSTNCategory(ddlBranchCompanyType.SelectedItem.Text)
                            ddlBranchGSTNCategory.SelectedValue = dt.Rows(i)("CUSTB_GSTNCategory")
                        Else
                            ddlBranchGSTNCategory.SelectedIndex = 0
                        End If

                        taxcategory = objCompMaster.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlBranchGSTNCategory.SelectedValue)
                        If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                            txtBranchGSTNRegNo.Enabled = False
                        Else
                            txtBranchGSTNRegNo.Enabled = True
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_GSTNRegNo").ToString()) = False Then
                            txtBranchGSTNRegNo.Text = dt.Rows(i)("CUSTB_GSTNRegNo").ToString()
                        Else
                            txtBranchGSTNRegNo.Text = ""
                        End If

                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdBranch_ItemCommand")
        End Try
    End Sub

    Protected Sub btnBranchSave_Click(sender As Object, e As EventArgs) Handles btnBranchSave.Click
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlBranchGSTNCategory.SelectedItem.Text = UCase("COMPOSITION DEALER") Or ddlBranchGSTNCategory.SelectedItem.Text = UCase("RIGISTERED DEALER") Then
                If txtBranchGSTNRegNo.Text = "" Then
                    lblError.Text = "Enter GSTN Reg.No For Branch."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter GSTN Reg.No For Branch.','', 'success');", True)
                    Exit Sub
                End If
            End If

            objCompMaster.CUSTB_CUST_ID = sSession.AccessCodeID
            objCompMaster.CUSTB_NAME = ddlBranch.SelectedValue
            objCompMaster.CUSTB_ContactPerson = txtContactPerson.Text
            objCompMaster.CUSTB_CITY = ddlBranchCity.SelectedValue
            'objCompMaster.CUSTB_STATE = ddlBranchState.SelectedValue
            If ddlBranchState.SelectedIndex > 0 Then
                objCompMaster.CUSTB_STATE = ddlBranchState.SelectedValue
            Else
                objCompMaster.CUSTB_STATE = 0
            End If
            objCompMaster.CUSTB_COUNTRY = ddlBranchCountry.SelectedValue
            objCompMaster.CUSTB_PIN = txtBranchPostalCode.Text
            objCompMaster.CUSTB_TELPHONE = txtBranchPhoneNo.Text
            objCompMaster.CUSTB_FAX = txtBranchFax.Text
            objCompMaster.CUSTB_ADDRESS = txtBranchAddress.Text
            objCompMaster.CUSTB_STATUS = "C"
            objCompMaster.CUSTB_DELFLG = "W"
            objCompMaster.CUSTB_CompID = sSession.AccessCodeID
            objCompMaster.CUSTB_CRBY = sSession.UserID
            objCompMaster.CUSTB_CRON = Date.Today
            objCompMaster.CUSTB_UpdatedBy = sSession.UserID
            objCompMaster.CUSTB_UpdatedOn = Date.Today
            objCompMaster.CUSTB_Operation = "C"
            objCompMaster.CUSTB_IPAddress = sSession.IPAddress

            If ddlBranchCompanyType.SelectedIndex > 0 Then
                objCompMaster.CUSTB_CompanyType = ddlBranchCompanyType.SelectedValue
            Else
                objCompMaster.CUSTB_CompanyType = 0
            End If

            If ddlBranchGSTNCategory.SelectedIndex > 0 Then
                objCompMaster.CUSTB_GSTNCategory = ddlBranchGSTNCategory.SelectedValue
            Else
                objCompMaster.CUSTB_GSTNCategory = 0
            End If

            If txtBranchGSTNRegNo.Text <> "" Then
                objCompMaster.CUSTB_GSTNRegNo = txtBranchGSTNRegNo.Text
            Else
                objCompMaster.CUSTB_GSTNRegNo = ""
            End If

            Arr = objCompMaster.SaveCumsterBranchDetails(sSession.AccessCode, objCompMaster)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated Branch Details.','', 'success');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved Branch Details.','', 'success');", True)
            End If
            ClearAll()
            grdBranch.DataSource = objCompMaster.LoadBranchDetails(sSession.AccessCode, sSession.AccessCodeID, 0)
            grdBranch.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBranch').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnBranchSave_Click")
        End Try
    End Sub

    Public Sub ClearAll()
        Try
            ddlBranch.SelectedIndex = 0 : txtContactPerson.Text = ""
            ddlBranchCity.SelectedIndex = 0 : ddlBranchState.SelectedIndex = 0 : ddlBranchCountry.SelectedIndex = 0
            txtBranchPostalCode.Text = "" : txtBranchPhoneNo.Text = "" : txtBranchFax.Text = ""
            txtBranchAddress.Text = "" : ddlBranchCompanyType.SelectedIndex = 0 : ddlBranchGSTNCategory.Items.Clear() : txtBranchGSTNRegNo.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub chkSameAddress_CheckedChanged(sender As Object, e As EventArgs) Handles chkSameAddress.CheckedChanged
        Try
            If chkSameAddress.Checked = True Then
                txtDelAddress.Text = objGen.SafeSQL(LTrim(txtBillAddress.Text))

                If ddlBillCity.SelectedIndex > 0 Then
                    ddlDelCity.SelectedValue = ddlBillCity.SelectedValue
                Else
                    ddlDelCity.SelectedIndex = 0
                End If

                txtDelPostalCode.Text = objGen.SafeSQL(txtBillPostalCode.Text)

                If ddlBillState.SelectedIndex > 0 Then
                    ddlDelState.SelectedValue = ddlBillState.SelectedValue
                Else
                    ddlDelState.SelectedIndex = 0
                End If

                If ddlBillCountry.SelectedIndex > 0 Then
                    ddlDelCountry.SelectedValue = ddlBillCountry.SelectedValue
                Else
                    ddlDelCountry.SelectedIndex = 0
                End If

                txtDelFax.Text = objGen.SafeSQL(Trim(txtBillFax.Text))
                txtDelTelephone.Text = objGen.SafeSQL(Trim(txtBillTelphone.Text))

                txtDelTelephone1.Text = objGen.SafeSQL(Trim(txtBillTelphone1.Text))
                txtDelTelephone2.Text = objGen.SafeSQL(Trim(txtBillTelphone2.Text))

            Else
                txtDelAddress.Text = ""
                ddlDelCity.SelectedIndex = 0
                txtDelPostalCode.Text = ""
                ddlDelState.SelectedIndex = 0
                ddlDelCountry.SelectedIndex = 0
                txtDelFax.Text = ""
                txtDelTelephone.Text = ""
                txtDelTelephone1.Text = ""
                txtDelTelephone2.Text = ""

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSameAddress_CheckedChanged")
        End Try
    End Sub
    Private Sub BindCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objCompMaster.LoadCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlCategory.DataSource = dt
            ddlCategory.DataTextField = "GC_GSTCategory"
            ddlCategory.DataValueField = "GC_Id"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindBranchGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objCompMaster.LoadCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlBranchGSTNCategory.DataSource = dt
            ddlBranchGSTNCategory.DataTextField = "GC_GSTCategory"
            ddlBranchGSTNCategory.DataValueField = "GC_Id"
            ddlBranchGSTNCategory.DataBind()
            ddlBranchGSTNCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Private Sub BindForms()
    '    Dim dt As New DataTable
    '    Dim iCatID As Integer
    '    Try
    '        If ddlCategory.SelectedIndex > 0 Then
    '            iCatID = ddlCategory.SelectedValue
    '        Else
    '            iCatID = 0
    '        End If
    '        dt = objCompMaster.LoadForms(sSession.AccessCode, sSession.AccessCodeID, iCatID)
    '        ddlForm.DataSource = dt
    '        ddlForm.DataTextField = "Mas_Desc"
    '        ddlForm.DataValueField = "Mas_Id"
    '        ddlForm.DataBind()
    '        ddlForm.Items.Insert(0, "Select")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub BindPeriodicity()
    '    Dim dt As New DataTable
    '    Dim iCatID As Integer
    '    Try
    '        If ddlCategory.SelectedIndex > 0 Then
    '            iCatID = ddlCategory.SelectedValue
    '        Else
    '            iCatID = 0
    '        End If
    '        dt = objCompMaster.LoadPeriodicity(sSession.AccessCode, sSession.AccessCodeID, iCatID)
    '        ddlPeriodicity.DataSource = dt
    '        ddlPeriodicity.DataTextField = "Mas_Desc"
    '        ddlPeriodicity.DataValueField = "Mas_Id"
    '        ddlPeriodicity.DataBind()
    '        ddlPeriodicity.Items.Insert(0, "Select")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
    '    Try
    '        If ddlCategory.SelectedIndex > 0 Then
    '            BindForms()
    '            BindPeriodicity()
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddlCompanyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompanyType.SelectedIndexChanged
        Try
            BindCategory(ddlCompanyType.SelectedItem.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompanyType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBranchCompanyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranchCompanyType.SelectedIndexChanged
        Try
            BindBranchGSTNCategory(ddlBranchCompanyType.SelectedItem.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranchCompanyType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnBranchNew_Click(sender As Object, e As EventArgs) Handles btnBranchNew.Click
        Try
            lblError.Text = ""
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnBranchNew_Click")
        End Try
    End Sub
    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlBranch.SelectedIndex > 0 Then
                dt = objCompMaster.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1

                        If IsDBNull(dt.Rows(i)("CUSTB_Name").ToString()) = False Then
                            If dt.Rows(i)("CUSTB_Name").ToString() > 0 Then
                                ddlBranch.SelectedValue = dt.Rows(i)("CUSTB_Name").ToString()
                            Else
                                ddlBranch.SelectedIndex = 0
                            End If
                        Else
                            ddlBranch.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_ContactPerson").ToString()) = False Then
                            txtContactPerson.Text = dt.Rows(i)("CUSTB_ContactPerson").ToString()
                        Else
                            txtContactPerson.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_CITY").ToString()) = False Then
                            ddlBranchCity.SelectedValue = dt.Rows(i)("CUSTB_CITY").ToString()
                        Else
                            ddlBranchCity.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_STATE").ToString()) = False Then
                            ddlBranchState.SelectedValue = dt.Rows(i)("CUSTB_STATE").ToString()
                        Else
                            ddlBranchState.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_COUNTRY").ToString()) = False Then
                            ddlBranchCountry.SelectedValue = dt.Rows(i)("CUSTB_COUNTRY").ToString()
                        Else
                            ddlBranchCountry.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_PIN").ToString()) = False Then
                            txtBranchPostalCode.Text = dt.Rows(i)("CUSTB_PIN").ToString()
                        Else
                            txtBranchPostalCode.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_TEL").ToString()) = False Then
                            txtBranchPhoneNo.Text = dt.Rows(i)("CUSTB_TEL").ToString()
                        Else
                            txtBranchPhoneNo.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_FAX").ToString()) = False Then
                            txtBranchFax.Text = dt.Rows(i)("CUSTB_FAX").ToString()
                        Else
                            txtBranchFax.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_ADDRESS").ToString()) = False Then
                            txtBranchAddress.Text = dt.Rows(i)("CUSTB_ADDRESS").ToString()
                        Else
                            txtBranchAddress.Text = ""
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_CompanyType")) = False Then
                            ddlBranchCompanyType.SelectedValue = dt.Rows(i)("CUSTB_CompanyType")
                        Else
                            ddlBranchCompanyType.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_GSTNCategory")) = False Then
                            BindBranchGSTNCategory(ddlBranchCompanyType.SelectedItem.Text)
                            ddlBranchGSTNCategory.SelectedValue = dt.Rows(i)("CUSTB_GSTNCategory")
                        Else
                            ddlBranchGSTNCategory.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(i)("CUSTB_GSTNRegNo").ToString()) = False Then
                            txtBranchGSTNRegNo.Text = dt.Rows(i)("CUSTB_GSTNRegNo").ToString()
                        Else
                            txtBranchGSTNRegNo.Text = ""
                        End If

                        Dim taxcategory As String
                        taxcategory = objCompMaster.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlBranchGSTNCategory.SelectedValue)
                        If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                            txtBranchGSTNRegNo.Enabled = False
                        Else
                            txtBranchGSTNRegNo.Enabled = True
                        End If

                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindBankDetails()
        Dim dt As New DataTable
        Try
            dt = objCompMaster.LoadBnkDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddgBnkDtls.DataSource = dt
            ddgBnkDtls.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBankDetails")
        End Try
    End Sub
    Public Sub LoadBanksName()
        Dim dt As New DataTable
        Try
            dt = objCompMaster.LoadBanksName(sSession.AccessCode, sSession.AccessCodeID)
            ddlBnkName.DataTextField = "Description"
            ddlBnkName.DataValueField = "gl_id"
            ddlBnkName.DataSource = dt
            ddlBnkName.DataBind()
            ddlBnkName.Items.Insert(0, "Select Bank")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim Arr() As String
        Try
            If (ddlBnkName.SelectedIndex = 0) Then
                lblError.Text = "Select the Bank Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select the Bank Name.','', '');", True)
                Exit Sub
            End If

            objCompMaster.BD_ID = 0
            objCompMaster.BD_CUSTID = sSession.AccessCodeID

            If ddlBranch.SelectedIndex > 0 Then
                objCompMaster.BD_BranchID = ddlBranch.SelectedValue
            Else
                objCompMaster.BD_BranchID = 0
            End If
            objCompMaster.BD_BankName = ddlBnkName.SelectedValue
            objCompMaster.BD_AccountNo = txtAccNo.Text
            objCompMaster.BD_IFSCCode = txtIFSCode.Text
            objCompMaster.BD_BranchName = txtBrnchName.Text
            objCompMaster.BD_CreatedBy = sSession.UserID
            objCompMaster.BD_UpdatedBy = sSession.UserID
            objCompMaster.BD_DelFlag = "X"
            objCompMaster.BD_YearID = sSession.YearID
            objCompMaster.BD_CompID = sSession.AccessCodeID
            objCompMaster.BD_Opeartion = "C"
            objCompMaster.BD_IPAddress = sSession.IPAddress

            Arr = objCompMaster.SavecomapanyBank_Details(sSession.AccessCode, sSession.AccessCodeID, objCompMaster)

            If Arr(0) = "2" Then
                BindBankDetails()
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
            ElseIf Arr(0) = "3" Then
                BindBankDetails()
                lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved.','', 'success');", True)
                ddlBnkName.SelectedIndex = 0 : txtAccNo.Text = "" : txtIFSCode.Text = "" : txtBrnchName.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub
    Private Sub ddlBnkName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBnkName.SelectedIndexChanged
        Dim dtBank As New DataTable
        Try
            If ddlBnkName.SelectedIndex = 0 Then
                txtAccNo.Text = "" : txtIFSCode.Text = "" : txtBrnchName.Text = ""
                btnSave.Text = "Save"

            ElseIf ddlBnkName.SelectedValue > 0 Then
                dtBank = objCompMaster.GetBank_Details(sSession.AccessCode, sSession.AccessCodeID, ddlBnkName.SelectedValue)
                If dtBank.Rows.Count > 0 Then
                    btnSave.Text = "update"
                    For i = 0 To dtBank.Rows.Count - 1
                        ddlBnkName.SelectedValue = dtBank.Rows(i)("BD_BankName")
                        txtAccNo.Text = dtBank.Rows(i)("BD_AccountNo")
                        txtIFSCode.Text = dtBank.Rows(i)("BD_IFSCCode")
                        txtBrnchName.Text = dtBank.Rows(i)("BD_BranchName")
                    Next
                Else
                    txtAccNo.Text = "" : txtIFSCode.Text = "" : txtBrnchName.Text = ""
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBnkName_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        Dim taxcategory As String
        Try
            taxcategory = objCompMaster.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCategory.SelectedValue)
            If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                txtProvisionNo.Text = ""
                txtProvisionNo.Enabled = False
            Else
                txtProvisionNo.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBranchGSTNCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranchGSTNCategory.SelectedIndexChanged
        'Dim taxcategory As String
        Try
            'taxcategory = objCompMaster.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlBranchGSTNCategory.SelectedValue)
            If UCase(ddlBranchGSTNCategory.SelectedItem.Text) = UCase("UNRIGISTERED DEALER") Then
                txtBranchGSTNRegNo.Enabled = False
            Else
                txtBranchGSTNRegNo.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranchGSTNCategory_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub dgOtherDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgOtherDetails.ItemDataBound
        Try
            dgOtherDetails.Columns(3).Visible = False
            If sCMSave = "YES" Then
                dgOtherDetails.Columns(3).Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBnkName_SelectedIndexChanged")
        End Try
    End Sub
End Class
