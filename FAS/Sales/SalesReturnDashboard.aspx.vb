Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Sales_SalesReturnDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales/SalesReturnDashboard"
    Dim objGen As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objDBL As New DatabaseLayer.DBHelper
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim objSalesReturn As New clsSaleReturn
    Private Shared sSRDAD As String
    Private Shared sSRDAP As String
    Dim objDispatch As New ClsDispatchDetails
    Dim dt As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SRET")
                imgbtnReport.Visible = False : sSRDAD = "NO" : sSRDAP = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        sSRDAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sSRDAP = "YES"
                    End If
                End If
                BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If


                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSalesReturnDetails(ByVal iStatus As Integer)
        Try
            dt = objSalesReturn.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iStatus)
            dgSales.DataSource = dt
            dgSales.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged

        Try
            lblError.Text = ""
            BindSalesReturnDetails(ddlStatus.SelectedIndex)

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
                For iIndx = 0 To dgSales.Rows.Count - 1
                    chkField = dgSales.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgSales.Rows.Count - 1
                    chkField = dgSales.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgSales_PreRender(sender As Object, e As EventArgs) Handles dgSales.PreRender
        Try
            If dgSales.Rows.Count > 0 Then
                dgSales.UseAccessibleHeader = True
                dgSales.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSales.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSales_PreRender")
        End Try
    End Sub
    Private Sub dgSales_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSales.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgSales.Columns(10).Visible = False
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    If sSRDAD = "YES" Then
                        dgSales.Columns(10).Visible = True
                    End If
                    dgSales.Columns(11).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    If sSRDAD = "YES" Then
                        dgSales.Columns(10).Visible = True
                    End If

                    dgSales.Columns(11).Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    If sSRDAP = "YES" Then
                        dgSales.Columns(10).Visible = True
                    End If
                    dgSales.Columns(11).Visible = True
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgSales.Columns(10).Visible = False : dgSales.Columns(11).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("~/Sales/SalesReurnDetails.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub dgSales_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSales.RowCommand
        Dim oStatusID As Object, oMasterID As Object
        Dim lblDescID As New Label, lblDescName As New Label, lblInvoiceId As New Label, lblDispatchId As New Label, lblCustId As New Label
        Dim lblInvoiceNo As New Label, lblInvoiceDate As New Label
        Dim dt As New DataTable
        Dim dtPO As New DataTable
        Dim iZoneID, iRegionID, iAreaID, iBranchID As Integer
        Try
            ' lblError.Text = ""       
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblInvoiceId = DirectCast(clickedRow.FindControl("lblInvoiceId"), Label)
            lblDispatchId = DirectCast(clickedRow.FindControl("lblDispatchId"), Label)
            lblCustId = DirectCast(clickedRow.FindControl("lblCustId"), Label)
            lblInvoiceNo = DirectCast(clickedRow.FindControl("lblInvoiceNo"), Label)
            lblInvoiceDate = DirectCast(clickedRow.FindControl("lblInvoiceDate"), Label)
            If e.CommandName.Equals("Edit") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                Response.Redirect(String.Format("~/Sales/SalesReurnDetails.aspx?StatusID={0}&MasterID={1}", oStatusID, oMasterID), False) 'GeneralMasterDetails
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objSalesReturn.UpdateSalesReturnStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objSalesReturn.UpdateSalesReturnStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    Dim sStr As String = ""
                    sStr = objSalesReturn.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                    If sStr = "A" Then
                        lblError.Text = "This has been already Approved."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This has been already Approved.','', 'success');", True)
                        Exit Sub
                    End If
                    objSalesReturn.UpdateSalesReturnStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress)
                    'Stock'
                    dt = objSalesReturn.GetSRDDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                    objSalesReturn.SaveStockLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, sSession.UserID, dt, lblDescID.Text)
                    'Stock'
                    GetSaleItemsGrid(lblDispatchId.Text, lblDescID.Text, lblCustId.Text)

                    dtPO = objSalesReturn.GetZone(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                    If dtPO.Rows.Count > 0 Then
                        iZoneID = dtPO.Rows(0)("POM_ZoneID")
                        iRegionID = dtPO.Rows(0)("POM_RegionID")
                        iAreaID = dtPO.Rows(0)("POM_AreaID")
                        iBranchID = dtPO.Rows(0)("POM_BranchID")
                    End If

                    SaveSalesJEDetails(lblInvoiceId.Text, lblCustId.Text, lblInvoiceNo.Text, lblInvoiceDate.Text, iZoneID, iRegionID, iAreaID, iBranchID)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
                ddlStatus.SelectedIndex = 0
                BindSalesReturnDetails(ddlStatus.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSales_RowCommand")
        End Try
    End Sub
    Public Sub GetSaleItemsGrid(ByVal iDispatchID As Integer, ByVal iMasterID As Integer, ByVal ICustID As Integer)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim iParty As Integer = 0

        Dim dt1 As New DataTable
        Dim dPartyTotal As Double
        Dim dtGSTRates As New DataTable : Dim sSql As String = ""
        Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Dim SGST, CGST, IGST As Double
        Dim sTypeOfBill As String = "" : Dim sState As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ICustID

            sTypeOfBill = objDBL.SQLGetDescription(sSession.AccessCode, "Select DM_Dispatchstatus From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & sSession.AccessCodeID & " And DM_YearID=" & sSession.YearID & " ")
            sState = objDBL.SQLGetDescription(sSession.AccessCode, "Select DM_State From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & sSession.AccessCodeID & " And DM_YearID=" & sSession.YearID & " ")

            sSql = "Select Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDBL.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)


            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From Sales_ReturnDetails Where SRD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And SRD_MasterID=" & iMasterID & " And SRD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("SRD_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("SRD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("SRD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("SRD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("SRD_TotalAmount"))
                        Next

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        dRow("GLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Sale Of Material"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dTotalAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)


                        SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        IGST = dtGSTRates.Rows(k)("GST_GSTRate")

                        dRow = dt.NewRow 'SGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output SGST " & SGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dSGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'CGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output CGST " & CGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dCGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'IGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output IGST " & IGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dIGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                    End If

                Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objDispatch.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
                dRow("GLID") = objDispatch.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
                dRow("SubGLID") = objDispatch.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "C")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objDispatch.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If
                dRow("Debit") = 0
                dRow("Credit") = dPartyTotal

                txtBillAmount.Text = dPartyTotal
                dt.Rows.Add(dRow)
            End If

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
        End Try
    End Sub
    Public Sub SaveSalesJEDetails(ByVal iMasterID As Integer, ByVal ICustID As Integer, ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, ByVal iBranchID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Try
            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblSalesValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblSalesValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblSalesValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 4 Then
                lblSalesValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 5 Then
                lblSalesValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            End If

            objDispatch.iAcc_JE_ID = 0
            objDispatch.sAcc_JE_TransactionNo = objDispatch.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "S")

            If ICustID > 0 Then
                objDispatch.iAcc_JE_Party = ICustID
            Else
                objDispatch.iAcc_JE_Party = 0
            End If
            objDispatch.iAcc_JE_Location = 0
            objDispatch.iAcc_JE_BillType = 0

            objDispatch.iAcc_JE_InvoiceID = iMasterID
            objDispatch.sAcc_JE_BillNo = sInvoiceNo
            objDispatch.dAcc_JE_BillDate = Date.ParseExact(sInvoiceDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objDispatch.dAcc_JE_BillAmount = txtBillAmount.Text
            objDispatch.iAcc_JE_YearID = sSession.YearID
            objDispatch.sAcc_JE_Status = "W"

            objDispatch.sAcc_JE_Type = "SR"

            objDispatch.iAcc_JE_CreatedBy = sSession.UserID
            objDispatch.iAcc_JE_CreatedOn = DateTime.Today
            objDispatch.sAcc_JE_Operation = "C"
            objDispatch.sAcc_JE_IPAddress = sSession.IPAddress
            objDispatch.dAcc_JE_BillCreatedDate = DateTime.Today

            objDispatch.dAcc_JE_AdvanceAmount = 0.00
            objDispatch.dAcc_JE_BalanceAmount = 0.00
            objDispatch.dAcc_JE_NetAmount = 0.00
            objDispatch.sAcc_JE_AdvanceNaration = ""
            objDispatch.sAcc_JE_PaymentNarration = ""
            objDispatch.sAcc_JE_ChequeNo = ""
            objDispatch.sAcc_JE_IFSCCode = ""
            objDispatch.sAcc_JE_BankName = ""
            objDispatch.sAcc_JE_BranchName = ""

            objDispatch.iAcc_JE_UpdatedBy = sSession.UserID
            objDispatch.iAcc_JE_UpdatedOn = DateTime.Today
            objDispatch.iAcc_JE_CompID = sSession.AccessCodeID


            If objDispatch.sAcc_JE_TransactionNo <> "" Then
                If objDispatch.sAcc_JE_TransactionNo.StartsWith("S") Then
                    Arr = objDispatch.SaveSalesJournalMaster(sSession.AccessCode, objDispatch)
                    iTransID = Arr(1)
                End If
            End If

            For i = 0 To dgJEDetails.Items.Count - 1

                If objDispatch.sAcc_JE_TransactionNo.StartsWith("S") Then
                    objDispatch.iATD_TrType = 6
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objDispatch.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objDispatch.iATD_ID = 0
                End If

                objDispatch.dATD_TransactionDate = DateTime.Today

                objDispatch.iATD_BillId = iTransID
                objDispatch.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objDispatch.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objDispatch.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objDispatch.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objDispatch.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objDispatch.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objDispatch.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objDispatch.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objDispatch.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objDispatch.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objDispatch.dATD_Credit = 0
                End If

                If objDispatch.dATD_Debit > 0 And objDispatch.dATD_Credit = 0 Then
                    objDispatch.iATD_DbOrCr = 1 'Debit
                ElseIf objDispatch.dATD_Debit = 0 And objDispatch.dATD_Credit > 0 Then
                    objDispatch.iATD_DbOrCr = 2 'Credit
                End If

                objDispatch.iATD_CreatedBy = sSession.UserID
                objDispatch.dATD_CreatedOn = DateTime.Today

                objDispatch.sATD_Status = "W"
                objDispatch.iATD_YearID = sSession.YearID
                objDispatch.sATD_Operation = "C"
                objDispatch.sATD_IPAddress = sSession.IPAddress

                objDispatch.iATD_UpdatedBy = sSession.UserID
                objDispatch.dATD_UpdatedOn = DateTime.Today

                objDispatch.iATD_CompID = sSession.AccessCodeID

                objDispatch.iATD_ZoneID = iZoneID
                objDispatch.iATD_RegionID = iRegionID
                objDispatch.iATD_AreaID = iAreaID
                objDispatch.iATD_BranchID = iBranchID


                objDispatch.dATD_OpenDebit = "0.00"
                objDispatch.dATD_OpenCredit = "0.00"
                objDispatch.dATD_ClosingDebit = "0.00"
                objDispatch.dATD_ClosingCredit = "0.00"
                objDispatch.iATD_SeqReferenceNum = 0


                objDispatch.SaveUpdateTransactionDetails(sSession.AccessCode, objDispatch)

            Next
            dgJEDetails.DataSource = objDispatch.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, objDispatch.sAcc_JE_TransactionNo)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveSalesJEDetails")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgJEDetails.Items.Count - 1
                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                End If
            Next

            If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) Then
                Return 1  ' Debit and Credit amount not Matched
            End If

            If dDebit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            Else
                If dDebit <> txtBillAmount.Text Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            End If

            If dCredit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            Else
                If dCredit <> txtBillAmount.Text Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckDebitAndCredit")
        End Try
    End Function
    Private Sub dgSales_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles dgSales.RowEditing

    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim dt As New DataTable
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dt = objSalesReturn.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblSalesValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-info');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesReturnDashboard.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Accounts", "Sales Return Dashboard", "PDF", 0, "", 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=SalesReturnDashboard" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dt As New DataTable
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dt = objSalesReturn.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblSalesValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-info');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesReturnDashboard.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Accounts", "Sales Return Dashboard", "Excel", 0, "", 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=SalesReturnDashboard" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class
