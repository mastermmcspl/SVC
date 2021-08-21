Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class FixedAsset_DepreciationComputation
    Inherits System.Web.UI.Page

    Private sFormName As String = "DepreciationComputation"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objDepComp As New ClsDepreciationComputation
    Dim dtDep As New DataTable
    Dim dtIt As New DataTable

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub pageload(sender As Object, e As EventArgs) Handles Me.Load

        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                BindDepreciationComputation()
                BindItRateComputation()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload")
        End Try
    End Sub
    Public Sub BindDepreciationComputation()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            btnCalculate.Enabled = True
            dt = objDepComp.LoadDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgDepComp.DataSource = dt
            dgDepComp.DataBind()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("DepreciationRate").ToString() = "" Then
                        lblError.Text = "Enter Depreciation Rate in Asset Master for " & dt.Rows(i)("Assettype") & "-" & dt.Rows(i)("AssetDescription")
                        btnCalculate.Enabled = False
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDepreciationComputation")
        End Try
    End Sub
    Public Sub BindItRateComputation()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            btnCalculate.Enabled = True
            dt = objDepComp.LoadItRateComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgItComp.DataSource = dt
            dgItComp.DataBind()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ItRate").ToString() = "" Then
                        lblError.Text = "Enter Depreciation Rate in Asset Master for " & dt.Rows(i)("Assettype") & "-" & dt.Rows(i)("AssetDescription")
                        btnCalculate.Enabled = False
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindItRateComputation")
        End Try
    End Sub
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Try
            dtDep = objDepComp.CalculateDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblError.Text)
            dgDepComp.DataSource = dtDep
            dgDepComp.DataBind()
            dtIt = objDepComp.CalculateItRateComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblError.Text)
            dgItComp.DataSource = dtIt
            dgItComp.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click")
        End Try
    End Sub

    Private Sub dgDepComp_PreRender(sender As Object, e As EventArgs) Handles dgDepComp.PreRender
        Try
            If dgDepComp.Rows.Count > 0 Then
                dgDepComp.UseAccessibleHeader = True
                dgDepComp.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDepComp.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDepComp_PreRender")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objDepComp As New ClsDepreciationComputation
        Dim objItComp As New ClsDepreciationComputation
        Dim Arr() As String
        Dim lblAssetMasterPKID As New Label
        Dim lblAssetTypeID As New Label

        Try
            If dgDepComp.Rows.Count > 0 Then

                For i = 0 To dgDepComp.Rows.Count - 1

                    objDepComp.ADep_ID = 0


                    lblAssetMasterPKID = dgDepComp.Rows(i).FindControl("lblAssetMasterPKID")
                    lblAssetTypeID = dgDepComp.Rows(i).FindControl("lblAssetTypeID")
                    objDepComp.ADep_Asset_MasterID = lblAssetMasterPKID.Text
                    objDepComp.ADep_AssetID = lblAssetTypeID.Text


                    'objDepComp.ADep_Asset_MasterID = dgDepComp.Rows(i).Cells(0).Text
                    'objDepComp.ADep_AssetID = dgDepComp.Rows(i).Cells(1).Text

                    objDepComp.ADep_Description = dgDepComp.Rows(i).Cells(4).Text
                    objDepComp.ADep_CommissionDate = dgDepComp.Rows(i).Cells(5).Text
                    objDepComp.ADep_Quantity = dgDepComp.Rows(i).Cells(6).Text
                    objDepComp.ADep_Depreciation_rate = dgDepComp.Rows(i).Cells(7).Text
                    objDepComp.ADep_PurchaseAmount = dgDepComp.Rows(i).Cells(8).Text
                    objDepComp.ADep_AssetAge = dgDepComp.Rows(i).Cells(9).Text
                    objDepComp.ADep_NoOfDays = dgDepComp.Rows(i).Cells(10).Text
                    objDepComp.ADep_Depreciationfor_theyear = dgDepComp.Rows(i).Cells(11).Text
                    objDepComp.ADep_YTD = dgDepComp.Rows(i).Cells(12).Text
                    objDepComp.ADep_WDV = dgDepComp.Rows(i).Cells(13).Text
                    objDepComp.ADep_ResidualValue = dgDepComp.Rows(i).Cells(14).Text
                    objDepComp.ADep_CreatedBy = sSession.UserID
                    objDepComp.ADep_CreatedOn = DateTime.Today
                    objDepComp.ADep_UpdatedBy = sSession.UserID
                    objDepComp.ADep_UpdatedOn = DateTime.Today
                    objDepComp.ADep_DelFlag = "X"
                    objDepComp.ADep_Status = "W"
                    objDepComp.ADep_YearID = sSession.YearID
                    objDepComp.ADep_CompID = sSession.AccessCodeID
                    objDepComp.ADep_Opeartion = "C"
                    objDepComp.ADep_IPAddress = sSession.IPAddress

                    Arr = objDepComp.SaveDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDepComp)

                    'If Arr(0) = "2" Then

                    '    lblError.Text = "Successfully Updated"
                    '    lblPaymentMasterValidationMsg.Text = lblError.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

                    'ElseIf Arr(0) = "3" Then
                    '    lblError.Text = "Successfully Saved"
                    '    lblPaymentMasterValidationMsg.Text = lblError.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    'End If
                Next

            End If
            If dgItComp.Rows.Count > 0 Then

                For i = 0 To dgItComp.Rows.Count - 1

                    objItComp.AIT_ID = 0


                    lblAssetMasterPKID = dgItComp.Rows(i).FindControl("lblAssetMasterPKID")
                    lblAssetTypeID = dgItComp.Rows(i).FindControl("lblAssetTypeID")
                    objItComp.AIT_Asset_MasterID = lblAssetMasterPKID.Text
                    objItComp.AIT_AssetID = lblAssetTypeID.Text


                    'objDepComp.ADep_Asset_MasterID = dgDepComp.Rows(i).Cells(0).Text
                    'objDepComp.ADep_AssetID = dgDepComp.Rows(i).Cells(1).Text

                    objItComp.AIT_Description = dgItComp.Rows(i).Cells(4).Text
                    objItComp.AIT_CommissionDate = dgItComp.Rows(i).Cells(5).Text
                    objItComp.AIT_Quantity = dgItComp.Rows(i).Cells(6).Text
                    objItComp.AIT_IncomeTax_rate = dgItComp.Rows(i).Cells(7).Text
                    objItComp.AIT_PurchaseAmount = dgItComp.Rows(i).Cells(8).Text
                    objItComp.AIT_AssetAge = dgItComp.Rows(i).Cells(9).Text
                    objItComp.AIT_NoOfDays = dgItComp.Rows(i).Cells(10).Text
                    objItComp.AIT_IncomeTaxfor_theyear = dgItComp.Rows(i).Cells(11).Text
                    objItComp.AIT_YTD = dgItComp.Rows(i).Cells(12).Text
                    objItComp.AIT_WDV = dgItComp.Rows(i).Cells(13).Text
                    objItComp.AIT_ResidualValue = dgItComp.Rows(i).Cells(14).Text
                    objItComp.AIT_CreatedBy = sSession.UserID
                    objItComp.AIT_CreatedOn = DateTime.Today
                    objItComp.AIT_UpdatedBy = sSession.UserID
                    objItComp.AIT_UpdatedOn = DateTime.Today
                    objItComp.AIT_DelFlag = "X"
                    objItComp.AIT_Status = "W"
                    objItComp.AIT_YearID = sSession.YearID
                    objItComp.AIT_CompID = sSession.AccessCodeID
                    objItComp.AIT_Opeartion = "C"
                    objItComp.AIT_IPAddress = sSession.IPAddress

                    Arr = objItComp.SaveIncomeTaxComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objItComp)

                    If Arr(0) = "2" Then

                        lblError.Text = "Successfully Updated"
                        lblPaymentMasterValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                        lblPaymentMasterValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    End If
                Next

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub dgItComp_PreRender(sender As Object, e As EventArgs) Handles dgItComp.PreRender
        Try
            If dgItComp.Rows.Count > 0 Then
                dgItComp.UseAccessibleHeader = True
                dgItComp.HeaderRow.TableSection = TableRowSection.TableHeader
                dgItComp.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItComp_PreRender")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            If dtDep.Rows.Count = 0 Or dtDep Is Nothing Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            If dtIt.Rows.Count = 0 Or dtIt Is Nothing Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtDep)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtIt)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/DepreciationComputation.rdlc")


            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=DepreciationComputation" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            If dtDep.Rows.Count = 0 Or dtDep Is Nothing Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            If dtIt.Rows.Count = 0 Or dtIt Is Nothing Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtDep)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtIt)
            ReportViewer1.LocalReport.DataSources.Add(rds1)

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=DepreciationComputation" + ".PDF")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
