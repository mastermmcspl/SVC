Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class Sales_SalesPartyUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_UploadSalesOrders"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objSPU As New ClsSalesPartyUpload
    Private Shared sFile As String
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = False
                imgbtnSave.Enabled = False
                lblErrorup.Text = ""
                Me.imgbtnSave.Attributes.Add("OnClick", "return validationSave()")
            End If
        Catch ex As Exception
            lblErrorup.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Function ReadExcel(ByVal sSql As String) As DataTable
        Dim con As New OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As New DataSet
        Try
            con = MSAccessOpenConnection()
            If IsNothing(con) = False Then
                da = New OleDb.OleDbDataAdapter(sSql, con)
                da.Fill(ds)
                con.Close()
                Return ds.Tables(0)
            End If
        Catch ex As Exception
        Finally
            da.Dispose()
        End Try
    End Function
    Private Function MSAccessOpenConnection() As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
    End Function

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            lblErrorup.Text = ""
            SaveCustomerMaster()
        Catch ex As Exception
            lblErrorup.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=SalesPartyMasters.xlsx")
            Response.TransmitFile(Server.MapPath("../") & "SampleExcels\SalesPartyMasters.xlsx")
            Response.End()
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
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
    Private Sub SaveCustomerMaster()
        Dim dtUpload As New DataTable
        Dim sName As String = "" : Dim sAddress As String = "" : Dim sPinCode As String = "" : Dim sContact As String = "" : Dim sMail As String = "" : Dim sOffice As String = ""
        Dim sMobile As String = "" : Dim Code As String = "" : Dim ContectPerson As String = "" : Dim FaxNo As String = "" : Dim Adress As String = "" : Dim PinCode As String = ""
        Dim city As String = "" : Dim State As String = "" : Dim flag As String = "" : Dim j As Integer

        Dim sAddress1 As String = "" : Dim sAddress2 As String = "" : Dim sAddress3 As String = "" : Dim PreFix As String = "" : Dim TINNo As String = ""
        Try
            If IsNothing(Session("dtUpload")) = False Then
                dtUpload = Session("dtUpload")
            Else
                lblErrorup.Text = "Laod before you Upload."
                lblCustomerValidationMsg.Text = lblErrorup.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            dtUpload = Session("dtUpload")

            'Validate Excel Data'
            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" And dtUpload.Rows(j).Item(2).ToString <> "&nbsp;" Then 'Checking for blank row

                        If IsDBNull(dtUpload.Rows(j).Item(0)) = False And dtUpload.Rows(j).Item(0).ToString <> "" Then
                            PreFix = dtUpload.Rows(j).Item(0)
                        Else
                            lblErrorup.Text = "PreFix cannot be blank" & "Line No:" & j + 1
                            lblCustomerValidationMsg.Text = lblErrorup.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(1)) = False And dtUpload.Rows(j).Item(1).ToString <> "" Then
                            Code = PreFix & " - " & (dtUpload.Rows(j).Item(1))
                        Else
                            lblErrorup.Text = "Party Code cannot be blank" & "Line No:" & j + 1
                            lblCustomerValidationMsg.Text = lblErrorup.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" Then
                            sName = (dtUpload.Rows(j).Item(2))
                        Else
                            lblErrorup.Text = "Party Name cannot be blank" & "Line No:" & j + 1
                            lblCustomerValidationMsg.Text = lblErrorup.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(3)) = False And dtUpload.Rows(j).Item(3).ToString <> "" Then
                            sAddress = (dtUpload.Rows(j).Item(3))
                        Else
                            sAddress = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(4)) = False And dtUpload.Rows(j).Item(4).ToString <> "" Then
                            sAddress1 = (dtUpload.Rows(j).Item(4))
                        Else
                            sAddress1 = ""
                        End If
                        If IsDBNull(dtUpload.Rows(j).Item(5)) = False And dtUpload.Rows(j).Item(5).ToString <> "" Then
                            sAddress2 = (dtUpload.Rows(j).Item(5))
                        Else
                            sAddress2 = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(6)) = False And dtUpload.Rows(j).Item(6).ToString <> "" Then
                            sAddress3 = (dtUpload.Rows(j).Item(6))
                        Else
                            sAddress3 = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(7)) = False And dtUpload.Rows(j).Item(7).ToString <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(7)) = True Then
                                sPinCode = (dtUpload.Rows(j).Item(7))
                                If sPinCode.Length < 6 Or sPinCode.Length > 6 Then
                                    lblErrorup.Text = "Line No " & j + 1 & " PinCode maximum size is 6"
                                    lblCustomerValidationMsg.Text = lblErrorup.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                lblErrorup.Text = "Line No " & j + 1 & " PinCode is not valid"
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            End If
                        Else
                            sPinCode = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(8)) = False And dtUpload.Rows(j).Item(8).ToString <> "" Then
                            sContact = (dtUpload.Rows(j).Item(8))
                        Else
                            sContact = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(9)) = False And dtUpload.Rows(j).Item(9).ToString <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(9)) = True And dtUpload.Rows(j).Item(9).ToString <> "" Then
                                sOffice = (dtUpload.Rows(j).Item(9))
                                If sOffice.Length > 15 Then
                                    lblErrorup.Text = "Line No " & j + 1 & " OfficePhone No maximum size is 15."
                                    lblCustomerValidationMsg.Text = lblErrorup.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                sOffice = ""
                            End If
                        Else
                            sOffice = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(10)) = False And dtUpload.Rows(j)(10).ToString() <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(10)) = True Then
                                sMobile = (dtUpload.Rows(j).Item(10))
                                If sMobile.Length < 10 Or sMobile.Length > 10 Then
                                    lblErrorup.Text = "Line No " & j + 1 & " Party Phone No maximum size is 10"
                                    lblCustomerValidationMsg.Text = lblErrorup.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                sMobile = ""
                            End If
                        Else
                            sMobile = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(11)) = False And dtUpload.Rows(j).Item(11).ToString <> "" Then
                            sMail = (dtUpload.Rows(j).Item(11))
                            If emailaddresscheck(sMail) = False Then
                                lblErrorup.Text = "Enter email address correctly Line No " & j + 1 & " "
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            End If
                        Else
                            sMail = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(12)) = False And dtUpload.Rows(j).Item(12).ToString <> "" Then
                            Dim iRet As Integer = objSPU.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(12), 0)
                            If iRet = 0 Then
                                lblErrorup.Text = "Create " & dtUpload.Rows(j).Item(12) & " - city in General Master " & " Line No - " & j + 1
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            Else
                                city = iRet.ToString()
                            End If
                        End If

                        'State
                        If IsDBNull(dtUpload.Rows(j).Item(13)) = False And dtUpload.Rows(j).Item(13).ToString <> "" Then
                            Dim iRet As Integer = objSPU.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(13), 1)
                            If iRet = 0 Then
                                lblErrorup.Text = "Create " & dtUpload.Rows(j).Item(13) & " - State in General Master " & " Line No - " & j + 1
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            Else
                                State = iRet.ToString()
                            End If
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(14)) = False And dtUpload.Rows(j).Item(14).ToString <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(14)) = True Then
                                FaxNo = (dtUpload.Rows(j).Item(14))
                                If FaxNo.Length > 12 Then
                                    lblErrorup.Text = "Line No " & j + 1 & " Fax Maximum size is 12"
                                    lblCustomerValidationMsg.Text = lblErrorup.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                lblErrorup.Text = "Line No " & j + 1 & " Enter valid Fax"
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            End If
                        Else
                            FaxNo = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(15)) = False And dtUpload.Rows(j).Item(15).ToString <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(15)) = True Then
                                TINNo = (dtUpload.Rows(j).Item(15))
                                If TINNo.Length > 11 Then
                                    lblErrorup.Text = "Line No " & j + 1 & " TINNo Exceeds, Maximum size is 11"
                                    lblCustomerValidationMsg.Text = lblErrorup.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                lblErrorup.Text = "Line No " & j + 1 & " Enter valid TINNo"
                                lblCustomerValidationMsg.Text = lblErrorup.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                Exit Sub
                            End If
                        Else
                            TINNo = ""
                        End If

                    End If

                Next
            End If
            'Validate Excel Data'


            'Save Excel Data once it is correct format'

            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    If IsDBNull(dtUpload.Rows(j).Item(0)) = False And dtUpload.Rows(j).Item(0).ToString <> "" Then
                        PreFix = dtUpload.Rows(j).Item(0)
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(1)) = False And dtUpload.Rows(j).Item(1).ToString <> "" Then
                        Code = PreFix & " - " & (dtUpload.Rows(j).Item(1))
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" Then
                        sName = (dtUpload.Rows(j).Item(2))
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(3)) = False And dtUpload.Rows(j).Item(3).ToString <> "" Then
                        sAddress = (dtUpload.Rows(j).Item(3))
                    Else
                        sAddress = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(4)) = False And dtUpload.Rows(j).Item(4).ToString <> "" Then
                        sAddress1 = (dtUpload.Rows(j).Item(4))
                    Else
                        sAddress1 = ""
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(5)) = False And dtUpload.Rows(j).Item(5).ToString <> "" Then
                        sAddress2 = (dtUpload.Rows(j).Item(5))
                    Else
                        sAddress2 = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(6)) = False And dtUpload.Rows(j).Item(6).ToString <> "" Then
                        sAddress3 = (dtUpload.Rows(j).Item(6))
                    Else
                        sAddress3 = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(7)) = False And dtUpload.Rows(j).Item(7).ToString <> "" Then
                        sPinCode = (dtUpload.Rows(j).Item(7))
                    Else
                        sPinCode = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(8)) = False And dtUpload.Rows(j).Item(8).ToString <> "" Then
                        sContact = (dtUpload.Rows(j).Item(8))
                    Else
                        sContact = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(9)) = False And dtUpload.Rows(j).Item(9).ToString <> "" Then
                        sOffice = (dtUpload.Rows(j).Item(9))
                    Else
                        sOffice = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(10)) = False And dtUpload.Rows(j)(10).ToString() <> "" Then
                        sMobile = (dtUpload.Rows(j).Item(10))
                    Else
                        sMobile = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(11)) = False And dtUpload.Rows(j).Item(11).ToString <> "" Then
                        sMail = (dtUpload.Rows(j).Item(11))
                    Else
                        sMail = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(12)) = False And dtUpload.Rows(j).Item(12).ToString <> "" Then
                        Dim iRet As Integer = objSPU.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(12), 0)
                        city = iRet.ToString()
                    End If

                    'State
                    If IsDBNull(dtUpload.Rows(j).Item(13)) = False And dtUpload.Rows(j).Item(13).ToString <> "" Then
                        Dim iRet As Integer = objSPU.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(13), 1)
                        State = iRet.ToString()
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(14)) = False And dtUpload.Rows(j).Item(14).ToString <> "" Then
                        FaxNo = (dtUpload.Rows(j).Item(14))
                    Else
                        FaxNo = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(15)) = False And dtUpload.Rows(j).Item(15).ToString <> "" Then
                        TINNo = (dtUpload.Rows(j).Item(15))
                    Else
                        TINNo = ""
                    End If

                    Dim sPerm As String = ""
                    Dim sArray1 As Array
                    sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    Dim iHead, iGroup, iSubGroup, iGL, iSubGL As Integer
                    iHead = sArray1(0) '1
                    iGroup = sArray1(1) '29
                    iSubGroup = sArray1(2) '31
                    iGL = sArray1(3) '146

                    iSubGL = CreateChartOfAccounts(sName, 3, sArray1(3), 1)

                    objSPU.SaveSalesPartyData(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, Code, sName, sAddress, sAddress1, sAddress2, sAddress3, sPinCode, sContact, sOffice, sMobile, sMail, city, State, FaxNo, TINNo, sSession.IPAddress, iGroup, iSubGroup, iGL, iSubGL)
                Next
            End If
            'Save Excel Data once it is correct format'
            lblErrorup.Text = "Uploaded Successfully"
            lblCustomerValidationMsg.Text = lblErrorup.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer) As Integer
        Dim sRet As String
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try

            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sName)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function emailaddresscheck(ByVal emailaddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailaddress, pattern)
        If emailAddressMatch.Success Then
            emailaddresscheck = True
        Else
            emailaddresscheck = False
        End If
    End Function
    Private Function TINNocheck(ByVal TINNo As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim TINNoMatch As Match = Regex.Match(TINNo, pattern)
        If TINNoMatch.Success Then
            TINNocheck = True
        Else
            TINNocheck = False
        End If
    End Function

    Private Sub btnExcelSheetName_Click(sender As Object, e As EventArgs) Handles btnExcelSheetName.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            imgbtnSave.Visible = False
            dgUpload.Visible = False
            If FULoad.FileName <> String.Empty Then
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName

                    End If
                    FULoad.PostedFile.SaveAs(sFile)
                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                Else
                    lblErrorup.Text = "Select Excel file only."
                    lblCustomerValidationMsg.Text = lblErrorup.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblErrorup.Text = "Select The Excel File"
                lblCustomerValidationMsg.Text = lblErrorup.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblErrorup.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnExcelSheetName_Click")
        End Try
    End Sub

    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Try
            lblErrorup.Text = ""
            'PanelLoad.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadExcelTable(sFile)
                If IsNothing(dttable) Then
                    lblErrorup.Text = "Invalid Excel format in selected sheet."
                    ddlSheetName.Items.Clear()
                    lblCustomerValidationMsg.Text = lblErrorup.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
                dgUpload.DataSource = dttable
                dgUpload.DataBind()
                dgUpload.Visible = True

                Session("dtUpload") = dttable
                imgbtnSave.Visible = True : imgbtnSave.Enabled = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()
                imgbtnSave.Enabled = False
            Else
                lblErrorup.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadExcelTable(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTable.Columns.Add("PreFix")
            dtTable.Columns.Add("Code")
            dtTable.Columns.Add("Name")
            dtTable.Columns.Add("Address")
            dtTable.Columns.Add("Address1")
            dtTable.Columns.Add("Address2")
            dtTable.Columns.Add("Address3")
            dtTable.Columns.Add("PinCode")
            dtTable.Columns.Add("ContactPerson")
            dtTable.Columns.Add("OfficePhoneNo")
            dtTable.Columns.Add("Mobile No")
            dtTable.Columns.Add("Email ID")
            dtTable.Columns.Add("City")
            dtTable.Columns.Add("State")
            dtTable.Columns.Add("Fax")
            dtTable.Columns.Add("TIN")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()
                Return dtStock
                lblCustomerValidationMsg.Text = lblErrorup.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If

            If dtStock.Columns.Count > 16 Then
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()
                lblCustomerValidationMsg.Text = lblErrorup.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Function
            End If

            For i = 0 To dtStock.Rows.Count - 1

                If IsDBNull(dtStock.Rows(i).Item(2).ToString) = False And dtStock.Rows(i).Item(2).ToString <> "" And dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then 'Checking for name and taking that row into grid

                    dRow = dtTable.NewRow

                    If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                        dRow("PreFix") = objGen.SafeSQL(dtStock.Rows(i).Item(0))
                    Else
                        dRow("PreFix") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                        If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                            dRow("Code") = objGen.SafeSQL(dtStock.Rows(i).Item(1))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                        If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                            dRow("Name") = objGen.SafeSQL(dtStock.Rows(i).Item(2))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                        dRow("Address") = objGen.SafeSQL(dtStock.Rows(i).Item(3))
                    Else
                        dRow("Address") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                        dRow("Address1") = objGen.SafeSQL(dtStock.Rows(i).Item(4))
                    Else
                        dRow("Address1") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                        dRow("Address2") = objGen.SafeSQL(dtStock.Rows(i).Item(5))
                    Else
                        dRow("Address2") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                        dRow("Address3") = objGen.SafeSQL(dtStock.Rows(i).Item(6))
                    Else
                        dRow("Address3") = ""
                    End If


                    If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                        dRow("PinCode") = objGen.SafeSQL(dtStock.Rows(i).Item(7))
                    Else
                        dRow("PinCode") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                        If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                            dRow("ContactPerson") = objGen.SafeSQL(dtStock.Rows(i).Item(8))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                        dRow("OfficePhoneNo") = objGen.SafeSQL(dtStock.Rows(i).Item(9))
                    Else
                        dRow("OfficePhoneNo") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                        dRow("Mobile No") = objGen.SafeSQL(dtStock.Rows(i).Item(10))
                    Else
                        dRow("Mobile No") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                        If dtStock.Rows(i).Item(11).ToString <> "&nbsp;" Then
                            dRow("Email ID") = objGen.SafeSQL(dtStock.Rows(i).Item(11))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                        dRow("City") = objGen.SafeSQL(dtStock.Rows(i).Item(12))
                    Else
                        dRow("City") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
                        dRow("State") = objGen.SafeSQL(dtStock.Rows(i).Item(13))
                    Else
                        dRow("State") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
                        dRow("Fax") = objGen.SafeSQL(dtStock.Rows(i).Item(14))
                    Else
                        dRow("Fax") = ""
                    End If

                    If IsDBNull(dtStock.Rows(i).Item(15)) = False Then
                        dRow("TIN") = objGen.SafeSQL(dtStock.Rows(i).Item(15))
                    Else
                        dRow("TIN") = ""
                    End If

                    dtTable.Rows.Add(dRow)
                End If

            Next
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExcelSheetNames(ByVal sPath As String) As DataTable
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables,
                   New Object() {Nothing, Nothing, Nothing, "TABLE"})
            If dt.Rows.Count > 0 Then
                dtTab.Columns.Add("ID")
                dtTab.Columns.Add("Name")
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("ID") = i + 1
                    drow("Name") = dt.Rows(i)(2)
                    dtTab.Rows.Add(drow)
                Next
            End If
            XLCon.Close()
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function MSAccessOpenConnection(ByVal sFile As String) As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
    End Function
    Private Sub dgUpload_PreRender(sender As Object, e As EventArgs) Handles dgUpload.PreRender
        Dim dt As New DataTable
        Try
            If dgUpload.Rows.Count > 0 Then
                dgUpload.UseAccessibleHeader = True
                dgUpload.HeaderRow.TableSection = TableRowSection.TableHeader
                dgUpload.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgUpload_PreRender")
        End Try
    End Sub
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
