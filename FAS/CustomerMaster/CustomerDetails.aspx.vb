Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Data.SqlClient
Public Class CustomerMaster_CustomerDetails
    Inherits System.Web.UI.Page
    Public Shared iCustID As Integer
    Public Shared iUserID As Integer
    Private objclsCustomerDetails As New clsCustomerDetails
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                iCustID = 0 : iUserID = 0 : btnSave.Visible = True : btnCancel.Visible = True   ' imgbtnBack.Visible = False :
                If (Request.QueryString("CustID")) Is Nothing = False Then
                    iCustID = Request.QueryString("CustID")
                    If iCustID > 0 Then
                        imgbtnBack.Visible = True
                        btnSave.Visible = False : btnCancel.Visible = False
                    End If
                End If
                'If (Request.QueryString("UserID")) Is Nothing = False Then
                '    iUserID = Request.QueryString("UserID")
                'End If

                RFVCDCompanyName.ErrorMessage = "Enter Company Name."
                REVCDCompanyName.ErrorMessage = "Exceeded max size(max 100)." : REVCDCompanyName.ValidationExpression = "^[\s\S]{0,100}$"

                RFVCDCompanyWebsite.ErrorMessage = "Enter Company Website."
                REVCDCompanyWebsite.ErrorMessage = "Exceeded max size(max 100)." : REVCDCompanyWebsite.ValidationExpression = "^[\s\S]{0,100}$"

                RFVCDCompanyEmailID.ErrorMessage = "Enter Company EmailID."
                REVCDCompanyEmailID.ErrorMessage = "Enter valid EmailID." : REVCDCompanyEmailID.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"

                REVCDCompanyTelephoneno.ErrorMessage = "Enter valid Telephone No." : REVCDCompanyTelephoneno.ValidationExpression = "^[0-9]{0,15}$"

                RFVPDMobileno.ErrorMessage = "Enter Mobile No."
                REVPDMobileno.ErrorMessage = "Enter valid Mobile No." : REVPDMobileno.ValidationExpression = "^[0-9]{10}$"

                RFVCDContactPerson.ErrorMessage = "Enter Contact Person."
                REVCDContactPerson.ErrorMessage = "Exceeded max size(max 100)." : REVCDContactPerson.ValidationExpression = "^[\s\S]{0,100}$"

                REVCDCompanyAddress.ErrorMessage = "Exceeded max size(max 500)." : REVCDCompanyAddress.ValidationExpression = "^[\s\S]{0,500}$"


                If iCustID > 0 Then
                    Dim dtDisplay As DataTable = objclsCustomerDetails.GetSelectedCustomerDetails(iCustID)
                    txtCDCompanyName.Text = dtDisplay.Rows(0)("MCD_CD_CompanyName")
                    txtCDCompanyWebsite.Text = dtDisplay.Rows(0)("MCD_CD_CompanyWebsite")
                    txtCDCompanyEmailID.Text = dtDisplay.Rows(0)("MCD_CD_EmailID")
                    txtCDCompanyTelephoneno.Text = dtDisplay.Rows(0)("MCD_CD_Telephoneno")
                    txtPDMobileno.Text = dtDisplay.Rows(0)("MCD_CD_Mobilenumber")
                    txtCDContactPerson.Text = dtDisplay.Rows(0)("MCD_CD_ContactPerson")
                    txtCDCompanyAddress.Text = dtDisplay.Rows(0)("MCD_CD_Address")
                    ddlPDProductInterest.SelectedValue = dtDisplay.Rows(0)("MCD_CD_ProductInterest")
                    ddlPDReason.SelectedValue = dtDisplay.Rows(0)("MCD_CD_Reason")
                    ddlPDAboutUs.SelectedValue = dtDisplay.Rows(0)("MCD_CD_Aboutus")
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearALL()
        Try
            txtCDCompanyName.Text = ""
            txtCDCompanyWebsite.Text = ""
            txtCDCompanyEmailID.Text = ""
            txtCDCompanyTelephoneno.Text = ""
            txtPDMobileno.Text = ""
            txtCDContactPerson.Text = ""
            txtCDCompanyAddress.Text = ""
            ddlPDProductInterest.SelectedIndex = 0
            ddlPDReason.SelectedIndex = 0
            ddlPDAboutUs.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ddlPDProductInterest.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select the Product"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#ddlPDProductInterest').focus();", True)
                Exit Sub
            End If
            If ddlPDReason.SelectedIndex = 0 Then
                lblValidationMsg.Text = "Select Reason for looking at the Website"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#ddlPDReason').focus();", True)
                Exit Sub
            End If
            Dim sIPAddress As String = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            objclsCustomerDetails.SaveCustRegDetails(txtCDCompanyName.Text, txtCDCompanyWebsite.Text, txtCDCompanyEmailID.Text, txtCDCompanyTelephoneno.Text,
                                                    txtPDMobileno.Text, txtCDContactPerson.Text, txtCDCompanyAddress.Text, ddlPDProductInterest.SelectedValue,
                                                    ddlPDReason.SelectedValue, ddlPDAboutUs.SelectedValue, sIPAddress)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Submitted');", True)

            ClearALL()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ClearALL()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/CustomerMaster/CustomerMaster.aspx"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class