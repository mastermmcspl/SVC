Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Partial Class Masters_PrintSettings
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objPrint As New ClsPrintSettings
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper

    Private objclsGRACePermission As New clsFASPermission
    Private Shared sPSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            lblError.Text = ""
            If IsPostBack = False Then


                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PS")
                imgbtnSave.Visible = False : imgbtnAdd.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                'imgbtnSave.Visible = False
                'sPSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasPS", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '        sPSave = "YES"
                '    End If
                '    'If sFormButtons.Contains(",Report,") = True Then
                '    '    imgbtnReport.Visible = True
                '    'End If
                'End If
                lnkbtnPurchase_Click(sender, e)
                divPurchase.Visible = True : divSales.Visible = True
                liPurchase.Attributes.Add("class", "active") : divPurchase.Attributes.Add("class", "tab-pane active")

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub RetreiveDataSales()
        Dim dt As New DataTable
        Dim sFileName As String = ""
        Try
            dt = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From Print_Settings Where PS_Status='S'").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("PS_CAddress") = 1 Then
                        chkCAddress.Checked = True
                    Else
                        chkCAddress.Checked = False
                    End If
                    If dt.Rows(i)("PS_CPhoneNo") = 1 Then
                        chkCPhNo.Checked = True
                    Else
                        chkCPhNo.Checked = False
                    End If
                    If dt.Rows(i)("PS_CEmail") = 1 Then
                        chkCEmail.Checked = True
                    Else
                        chkCEmail.Checked = False
                    End If
                    If dt.Rows(i)("PS_CVAT") = 1 Then
                        chkCVAT.Checked = True
                    Else
                        chkCVAT.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTax") = 1 Then
                        chkCTAX.Checked = True
                    Else
                        chkCTAX.Checked = False
                    End If
                    If dt.Rows(i)("PS_CPAN") = 1 Then
                        chkCPAN.Checked = True
                    Else
                        chkCPAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTAN") = 1 Then
                        chkCTAN.Checked = True
                    Else
                        chkCTAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTIN") = 1 Then
                        chkCTIN.Checked = True
                    Else
                        chkCTIN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CCIN") = 1 Then
                        chkCCIN.Checked = True
                    Else
                        chkCCIN.Checked = False
                    End If

                    If dt.Rows(i)("PS_BAddress") = 1 Then
                        chkBAddress.Checked = True
                    Else
                        chkBAddress.Checked = False
                    End If
                    If dt.Rows(i)("PS_BPhoneNo") = 1 Then
                        chkBPhNo.Checked = True
                    Else
                        chkBPhNo.Checked = False
                    End If
                    If dt.Rows(i)("PS_BEmail") = 1 Then
                        chkBEmail.Checked = True
                    Else
                        chkBEmail.Checked = False
                    End If
                    If dt.Rows(i)("PS_BVAT") = 1 Then
                        chkBVAT.Checked = True
                    Else
                        chkBVAT.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTax") = 1 Then
                        chkBTAX.Checked = True
                    Else
                        chkBTAX.Checked = False
                    End If
                    If dt.Rows(i)("PS_BPAN") = 1 Then
                        chkBPAN.Checked = True
                    Else
                        chkBPAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTAN") = 1 Then
                        chkBTAN.Checked = True
                    Else
                        chkBTAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTIN") = 1 Then
                        chkBTIN.Checked = True
                    Else
                        chkBTIN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BCIN") = 1 Then
                        chkBCIN.Checked = True
                    Else
                        chkBCIN.Checked = False
                    End If

                    If dt.Rows(i)("PS_Terms") = 1 Then
                        chkTerms.Checked = True
                    Else
                        chkTerms.Checked = False
                    End If
                    If dt.Rows(i)("PS_Receiver") = 1 Then
                        chkReceivers.Checked = True
                    Else
                        chkReceivers.Checked = False
                    End If
                    If dt.Rows(i)("PS_Authorise") = 1 Then
                        chkAuthorised.Checked = True
                    Else
                        chkAuthorised.Checked = False
                    End If

                    If IsDBNull(dt.Rows(i)("PS_RptType")) = False Then
                        If dt.Rows(i)("PS_RptType") = 1 Then
                            chkSizeWiseReport.Checked = True
                        Else
                            chkSizeWiseReport.Checked = False
                        End If
                    Else
                        chkSizeWiseReport.Checked = False
                    End If

                    sFileName = objPrint.getImageName(sSession.AccessCode, sSession.AccessCodeID, "S")
                    If sFileName <> "" And sFileName <> "." Then
                        Dim imageDataURL As String = Server.MapPath("~/Images/" + sFileName)
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                        Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                        Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                        myImgS.ImageUrl = imageDataURL1
                    End If

                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub RetreiveDataPurchase()
        Dim dt As New DataTable
        Dim sFileName As String = ""
        Try
            dt = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From Print_Settings Where PS_Status='P'").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("PS_CAddress") = 1 Then
                        chkPCAddress.Checked = True
                    Else
                        chkPCAddress.Checked = False
                    End If
                    If dt.Rows(i)("PS_CPhoneNo") = 1 Then
                        chkPCPhNo.Checked = True
                    Else
                        chkPCPhNo.Checked = False
                    End If
                    If dt.Rows(i)("PS_CEmail") = 1 Then
                        chkPCEmail.Checked = True
                    Else
                        chkPCEmail.Checked = False
                    End If
                    If dt.Rows(i)("PS_CVAT") = 1 Then
                        chkPCVAT.Checked = True
                    Else
                        chkPCVAT.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTax") = 1 Then
                        chkPCTAX.Checked = True
                    Else
                        chkPCTAX.Checked = False
                    End If
                    If dt.Rows(i)("PS_CPAN") = 1 Then
                        chkPCPAN.Checked = True
                    Else
                        chkPCPAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTAN") = 1 Then
                        chkPCTAN.Checked = True
                    Else
                        chkPCTAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CTIN") = 1 Then
                        chkPCTIN.Checked = True
                    Else
                        chkPCTIN.Checked = False
                    End If
                    If dt.Rows(i)("PS_CCIN") = 1 Then
                        chkPCCIN.Checked = True
                    Else
                        chkPCCIN.Checked = False
                    End If

                    If dt.Rows(i)("PS_BAddress") = 1 Then
                        chkPBAddress.Checked = True
                    Else
                        chkPBAddress.Checked = False
                    End If
                    If dt.Rows(i)("PS_BPhoneNo") = 1 Then
                        chkPBPhNo.Checked = True
                    Else
                        chkPBPhNo.Checked = False
                    End If
                    If dt.Rows(i)("PS_BEmail") = 1 Then
                        chkPBEmail.Checked = True
                    Else
                        chkPBEmail.Checked = False
                    End If
                    If dt.Rows(i)("PS_BVAT") = 1 Then
                        chkPBVAT.Checked = True
                    Else
                        chkPBVAT.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTax") = 1 Then
                        chkPBTAX.Checked = True
                    Else
                        chkPBTAX.Checked = False
                    End If
                    If dt.Rows(i)("PS_BPAN") = 1 Then
                        chkPBPAN.Checked = True
                    Else
                        chkPBPAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTAN") = 1 Then
                        chkPBTAN.Checked = True
                    Else
                        chkPBTAN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BTIN") = 1 Then
                        chkPBTIN.Checked = True
                    Else
                        chkPBTIN.Checked = False
                    End If
                    If dt.Rows(i)("PS_BCIN") = 1 Then
                        chkPBCIN.Checked = True
                    Else
                        chkPBCIN.Checked = False
                    End If

                    If dt.Rows(i)("PS_Terms") = 1 Then
                        chkPTerms.Checked = True
                    Else
                        chkPTerms.Checked = False
                    End If
                    If dt.Rows(i)("PS_Receiver") = 1 Then
                        chkPReceivers.Checked = True
                    Else
                        chkPReceivers.Checked = False
                    End If
                    If dt.Rows(i)("PS_Authorise") = 1 Then
                        chkPAuthorised.Checked = True
                    Else
                        chkPAuthorised.Checked = False
                    End If

                    If IsDBNull(dt.Rows(i)("PS_RptType")) = False Then
                        If dt.Rows(i)("PS_RptType") = 1 Then
                            chkPSizeWiseReport.Checked = True
                        Else
                            chkPSizeWiseReport.Checked = False
                        End If
                    Else
                        chkPSizeWiseReport.Checked = False
                    End If

                    sFileName = objPrint.getImageName(sSession.AccessCode, sSession.AccessCodeID, "P")
                    If sFileName <> "" And sFileName <> "." Then
                        Dim imageDataURL As String = Server.MapPath("~/Images/" + sFileName)
                        Dim bytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                        Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                        Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                        myImgP.ImageUrl = imageDataURL1
                    End If

                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function SaveToBigDataSales(ByVal sPath As String) As Boolean
        Dim sExt As String = ""
        Dim sFileName As String = "" : Dim ssql As String = ""
        Dim iPosDot, iPosSlash As Integer
        Dim con As OleDb.OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader

        Dim ImgByte() As Byte
        Dim ImgLen As Integer
        Dim fs As IO.FileStream

        Dim iCAddress, iCPhNo, iCEmail, iCVAT, iCTAX, iCPAN, iCTAN, iCTIN, iCCIN As Integer
        Dim iBAddress, iBPhNo, iBEmail, iBVAT, iBTAX, iBPAN, iBTAN, iBTIN, iBCIN As Integer
        Dim iTerms, iReceivers, iAuthorised As Integer
        Dim iSizeWiseReport As Integer
        Try

            If sPath <> "" Then
                iPosSlash = InStrRev(sPath, "\")
                iPosDot = InStrRev(sPath, ".")
                If iPosDot <> 0 Then
                    sFileName = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))

                    sExt = Right(sPath, Len(sPath) - iPosDot)
                Else
                    If sPath = "" Then
                        Exit Function
                    End If
                    sFileName = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                    If sFileName = "" Then
                        Exit Function
                    End If
                    sExt = "unk"
                End If

                fs = New IO.FileStream(sPath, IO.FileMode.Open, IO.FileAccess.Read)
                ImgLen = fs.Length
                ReDim ImgByte(ImgLen)
                fs.Read(ImgByte, 0, System.Convert.ToInt32(fs.Length))
                fs.Close()
            End If

            'Customer'

            If chkCAddress.Checked = True Then
                iCAddress = 1
            Else
                iCAddress = 0
            End If
            If chkCPhNo.Checked = True Then
                iCPhNo = 1
            Else
                iCPhNo = 0
            End If
            If chkCEmail.Checked = True Then
                iCEmail = 1
            Else
                iCEmail = 0
            End If
            If chkCVAT.Checked = True Then
                iCVAT = 1
            Else
                iCVAT = 0
            End If
            If chkCTAX.Checked = True Then
                iCTAX = 1
            Else
                iCTAX = 0
            End If
            If chkCPAN.Checked = True Then
                iCPAN = 1
            Else
                iCPAN = 0
            End If
            If chkCTAN.Checked = True Then
                iCTAN = 1
            Else
                iCTAN = 0
            End If
            If chkCTIN.Checked = True Then
                iCTIN = 1
            Else
                iCTIN = 0
            End If
            If chkCCIN.Checked = True Then
                iCCIN = 1
            Else
                iCCIN = 0
            End If

            '----------------'

            'Buyer'

            If chkBAddress.Checked = True Then
                iBAddress = 1
            Else
                iBAddress = 0
            End If
            If chkBPhNo.Checked = True Then
                iBPhNo = 1
            Else
                iBPhNo = 0
            End If
            If chkBEmail.Checked = True Then
                iBEmail = 1
            Else
                iBEmail = 0
            End If
            If chkBVAT.Checked = True Then
                iBVAT = 1
            Else
                iBVAT = 0
            End If
            If chkBTAX.Checked = True Then
                iBTAX = 1
            Else
                iBTAX = 0
            End If
            If chkBPAN.Checked = True Then
                iBPAN = 1
            Else
                iBPAN = 0
            End If
            If chkBTAN.Checked = True Then
                iBTAN = 1
            Else
                iBTAN = 0
            End If
            If chkBTIN.Checked = True Then
                iBTIN = 1
            Else
                iBTIN = 0
            End If
            If chkBCIN.Checked = True Then
                iBCIN = 1
            Else
                iBCIN = 0
            End If

            '-------------'

            If chkTerms.Checked = True Then
                iTerms = 1
            Else
                iTerms = 0
            End If
            If chkReceivers.Checked = True Then
                iReceivers = 1
            Else
                iReceivers = 0
            End If
            If chkAuthorised.Checked = True Then
                iAuthorised = 1
            Else
                iAuthorised = 0
            End If

            If chkSizeWiseReport.Checked = True Then
                iSizeWiseReport = 1
            Else
                iSizeWiseReport = 0
            End If

            Dim iMaxID As Integer : Dim iExistingID As Integer
            ssql = "Select * from Print_Settings where PS_Status ='S'"
            dr = objDBL.SQLDataReader(sSession.AccessCode, ssql)
            If dr.HasRows = True Then
                'ssql = "" : ssql = "Delete from Print_Settings where PS_Status ='S'"
                'objDBL.SQLExecuteNonQuery(sSession.AccessCode, ssql)
                iExistingID = objDBL.SQLExecuteScalarInt(sSession.AccessCode, "Select PS_ID From Print_Settings Where PS_Status ='S'")
                If sPath <> "" Then
                    ssql = "" : ssql = "Update Print_Settings Set PS_CAddress=" & iCAddress & ",PS_CPhoneNo=" & iCPhNo & ",PS_CEmail=" & iCEmail & ",PS_CVAT=" & iCVAT & ",PS_CTax=" & iCTAX & ",PS_CPAN=" & iCPAN & ",PS_CTAN=" & iCTAN & ",PS_CTIN=" & iCTIN & ",PS_CCIN=" & iCCIN & ",PS_BAddress=" & iBAddress & ",PS_BPhoneNo=" & iBPhNo & ",PS_BEmail=" & iBEmail & ",PS_BVAT=" & iBVAT & ",PS_BTax=" & iBTAX & ",PS_BPAN=" & iBPAN & ",PS_BTAN=" & iBTAN & ",PS_BTIN=" & iBTIN & ",PS_BCIN=" & iBCIN & ",PS_Terms=" & iTerms & ",PS_Receiver=" & iReceivers & ",PS_Authorise=" & iAuthorised & ",PS_BIGDATA='?',PS_SIZE=" & ImgLen & ",PS_RptType=" & iSizeWiseReport & ",PS_FileName='" & sFileName & "',PS_Extn='" & sExt & "' Where PS_ID=" & iExistingID & " And PS_Status='S' And PS_CompID=" & sSession.AccessCodeID & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@PS_BIGDATA", ImgByte)
                    com.Parameters.Add("@PS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "Update Print_Settings Set PS_CAddress=" & iCAddress & ",PS_CPhoneNo=" & iCPhNo & ",PS_CEmail=" & iCEmail & ",PS_CVAT=" & iCVAT & ",PS_CTax=" & iCTAX & ",PS_CPAN=" & iCPAN & ",PS_CTAN=" & iCTAN & ",PS_CTIN=" & iCTIN & ",PS_CCIN=" & iCCIN & ",PS_BAddress=" & iBAddress & ",PS_BPhoneNo=" & iBPhNo & ",PS_BEmail=" & iBEmail & ",PS_BVAT=" & iBVAT & ",PS_BTax=" & iBTAX & ",PS_BPAN=" & iBPAN & ",PS_BTAN=" & iBTAN & ",PS_BTIN=" & iBTIN & ",PS_BCIN=" & iBCIN & ",PS_Terms=" & iTerms & ",PS_Receiver=" & iReceivers & ",PS_Authorise=" & iAuthorised & ",PS_RptType=" & iSizeWiseReport & " Where PS_ID=" & iExistingID & " And PS_Status='S' And PS_CompID=" & sSession.AccessCodeID & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If
            Else
                iMaxID = objGenFun.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Print_Settings", "PS_ID", "PS_CompID")
                If sPath <> "" Then
                    ssql = "" : ssql = "insert into Print_Settings(PS_ID,PS_CAddress,PS_CPhoneNo,PS_CEmail,PS_CVAT,PS_CTax,PS_CPAN,PS_CTAN,PS_CTIN,PS_CCIN,PS_BAddress,PS_BPhoneNo,PS_BEmail,PS_BVAT,PS_BTax,PS_BPAN,PS_BTAN,PS_BTIN,PS_BCIN,PS_Terms,PS_Receiver,PS_Authorise,PS_BIGDATA,PS_SIZE,PS_Status,PS_RptType,PS_FileName,PS_Extn,PS_CompID) values (" & iMaxID & "," & iCAddress & "," & iCPhNo & "," & iCEmail & "," & iCVAT & "," & iCTAX & "," & iCPAN & "," & iCTAN & "," & iCTIN & "," & iCCIN & "," & iBAddress & "," & iBPhNo & "," & iBEmail & "," & iBVAT & "," & iBTAX & "," & iBPAN & "," & iBTAN & "," & iBTIN & "," & iBCIN & "," & iTerms & "," & iReceivers & "," & iAuthorised & ",?,?,'S'," & iSizeWiseReport & ",'" & sFileName & "','" & sExt & "'," & sSession.AccessCodeID & ")"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@PS_BIGDATA", ImgByte)
                    com.Parameters.Add("@PS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "insert into Print_Settings(PS_ID,PS_CAddress,PS_CPhoneNo,PS_CEmail,PS_CVAT,PS_CTax,PS_CPAN,PS_CTAN,PS_CTIN,PS_CCIN,PS_BAddress,PS_BPhoneNo,PS_BEmail,PS_BVAT,PS_BTax,PS_BPAN,PS_BTAN,PS_BTIN,PS_BCIN,PS_Terms,PS_Receiver,PS_Authorise,PS_Status,PS_RptType,PS_CompID) values (" & iMaxID & "," & iCAddress & "," & iCPhNo & "," & iCEmail & "," & iCVAT & "," & iCTAX & "," & iCPAN & "," & iCTAN & "," & iCTIN & "," & iCCIN & "," & iBAddress & "," & iBPhNo & "," & iBEmail & "," & iBVAT & "," & iBTAX & "," & iBPAN & "," & iBTAN & "," & iBTIN & "," & iBCIN & "," & iTerms & "," & iReceivers & "," & iAuthorised & ",'S'," & iSizeWiseReport & "," & sSession.AccessCodeID & ")"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If
            End If



            Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
            myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
            com.Connection = con
            com.Transaction = myTrans
            com.ExecuteNonQuery()
            myTrans.Commit()
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveToBigDataPurchase(ByVal sPath As String) As Boolean
        Dim sExt, sFileName, ssql As String
        Dim iPosDot, iPosSlash As Integer
        Dim con As OleDb.OleDbConnection
        Dim objFile As FileStream
        Dim com As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader


        Dim ImgByte() As Byte
        Dim ImgLen As Integer
        Dim fs As IO.FileStream

        Dim iPCAddress, iPCPhNo, iPCEmail, iPCVAT, iPCTAX, iPCPAN, iPCTAN, iPCTIN, iPCCIN As Integer
        Dim iPBAddress, iPBPhNo, iPBEmail, iPBVAT, iPBTAX, iPBPAN, iPBTAN, iPBTIN, iPBCIN As Integer
        Dim iPTerms, iPReceivers, iPAuthorised As Integer
        Dim iPSizeWiseReport As Integer
        Try

            If sPath <> "" Then
                iPosSlash = InStrRev(sPath, "\")
                iPosDot = InStrRev(sPath, ".")
                If iPosDot <> 0 Then
                    sFileName = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))

                    sExt = Right(sPath, Len(sPath) - iPosDot)
                Else
                    If sPath = "" Then
                        Exit Function
                    End If
                    sFileName = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                    If sFileName = "" Then
                        Exit Function
                    End If
                    sExt = "unk"
                End If

                fs = New IO.FileStream(sPath, IO.FileMode.Open, IO.FileAccess.Read)
                ImgLen = fs.Length
                ReDim ImgByte(ImgLen)
                fs.Read(ImgByte, 0, System.Convert.ToInt32(fs.Length))
                fs.Close()
            End If

            'Customer'

            If chkPCAddress.Checked = True Then
                iPCAddress = 1
            Else
                iPCAddress = 0
            End If
            If chkPCPhNo.Checked = True Then
                iPCPhNo = 1
            Else
                iPCPhNo = 0
            End If
            If chkPCEmail.Checked = True Then
                iPCEmail = 1
            Else
                iPCEmail = 0
            End If
            If chkPCVAT.Checked = True Then
                iPCVAT = 1
            Else
                iPCVAT = 0
            End If
            If chkPCTAX.Checked = True Then
                iPCTAX = 1
            Else
                iPCTAX = 0
            End If
            If chkPCPAN.Checked = True Then
                iPCPAN = 1
            Else
                iPCPAN = 0
            End If
            If chkPCTAN.Checked = True Then
                iPCTAN = 1
            Else
                iPCTAN = 0
            End If
            If chkPCTIN.Checked = True Then
                iPCTIN = 1
            Else
                iPCTIN = 0
            End If
            If chkPCCIN.Checked = True Then
                iPCCIN = 1
            Else
                iPCCIN = 0
            End If

            '----------------'

            'Buyer'

            If chkPBAddress.Checked = True Then
                iPBAddress = 1
            Else
                iPBAddress = 0
            End If
            If chkPBPhNo.Checked = True Then
                iPBPhNo = 1
            Else
                iPBPhNo = 0
            End If
            If chkPBEmail.Checked = True Then
                iPBEmail = 1
            Else
                iPBEmail = 0
            End If
            If chkPBVAT.Checked = True Then
                iPBVAT = 1
            Else
                iPBVAT = 0
            End If
            If chkPBTAX.Checked = True Then
                iPBTAX = 1
            Else
                iPBTAX = 0
            End If
            If chkBPAN.Checked = True Then
                iPBPAN = 1
            Else
                iPBPAN = 0
            End If
            If chkPBTAN.Checked = True Then
                iPBTAN = 1
            Else
                iPBTAN = 0
            End If
            If chkPBTIN.Checked = True Then
                iPBTIN = 1
            Else
                iPBTIN = 0
            End If
            If chkPBCIN.Checked = True Then
                iPBCIN = 1
            Else
                iPBCIN = 0
            End If

            '-------------'

            If chkPTerms.Checked = True Then
                iPTerms = 1
            Else
                iPTerms = 0
            End If
            If chkPReceivers.Checked = True Then
                iPReceivers = 1
            Else
                iPReceivers = 0
            End If
            If chkPAuthorised.Checked = True Then
                iPAuthorised = 1
            Else
                iPAuthorised = 0
            End If

            If chkPSizeWiseReport.Checked = True Then
                iPSizeWiseReport = 1
            Else
                iPSizeWiseReport = 0
            End If

            Dim iMaxID As Integer : Dim iExistingID As Integer
            ssql = "Select * from Print_Settings where PS_Status ='P'"
            dr = objDBL.SQLDataReader(sSession.AccessCode, ssql)
            If dr.HasRows = True Then
                'ssql = "" : ssql = "Delete from Print_Settings where PS_Status ='P'"
                'objDBL.SQLExecuteNonQuery(sSession.AccessCode, ssql)
                iExistingID = objDBL.SQLExecuteScalarInt(sSession.AccessCode, "Select PS_ID From Print_Settings Where PS_Status ='P'")
                If sPath <> "" Then
                    ssql = "" : ssql = "Update Print_Settings Set PS_CAddress=" & iPCAddress & ",PS_CPhoneNo=" & iPCPhNo & ",PS_CEmail=" & iPCEmail & ",PS_CVAT=" & iPCVAT & ",PS_CTax=" & iPCTAX & ",PS_CPAN=" & iPCPAN & ",PS_CTAN=" & iPCTAN & ",PS_CTIN=" & iPCTIN & ",PS_CCIN=" & iPCCIN & ",PS_BAddress=" & iPBAddress & ",PS_BPhoneNo=" & iPBPhNo & ",PS_BEmail=" & iPBEmail & ",PS_BVAT=" & iPBVAT & ",PS_BTax=" & iPBTAX & ",PS_BPAN=" & iPBPAN & ",PS_BTAN=" & iPBTAN & ",PS_BTIN=" & iPBTIN & ",PS_BCIN=" & iPBCIN & ",PS_Terms=" & iPTerms & ",PS_Receiver=" & iPReceivers & ",PS_Authorise=" & iPAuthorised & ",PS_BIGDATA='?',PS_SIZE=" & ImgLen & ",PS_RptType=" & iPSizeWiseReport & ",PS_FileName='" & sFileName & "',PS_Extn='" & sExt & "' Where PS_ID=" & iExistingID & " And PS_Status='P' And PS_CompID=" & sSession.AccessCodeID & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@PS_BIGDATA", ImgByte)
                    com.Parameters.Add("@PS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "Update Print_Settings Set PS_CAddress=" & iPCAddress & ",PS_CPhoneNo=" & iPCPhNo & ",PS_CEmail=" & iPCEmail & ",PS_CVAT=" & iPCVAT & ",PS_CTax=" & iPCTAX & ",PS_CPAN=" & iPCPAN & ",PS_CTAN=" & iPCTAN & ",PS_CTIN=" & iPCTIN & ",PS_CCIN=" & iPCCIN & ",PS_BAddress=" & iPBAddress & ",PS_BPhoneNo=" & iPBPhNo & ",PS_BEmail=" & iPBEmail & ",PS_BVAT=" & iPBVAT & ",PS_BTax=" & iPBTAX & ",PS_BPAN=" & iPBPAN & ",PS_BTAN=" & iPBTAN & ",PS_BTIN=" & iPBTIN & ",PS_BCIN=" & iPBCIN & ",PS_Terms=" & iPTerms & ",PS_Receiver=" & iPReceivers & ",PS_Authorise=" & iPAuthorised & ",PS_RptType=" & iPSizeWiseReport & " Where PS_ID=" & iExistingID & " And PS_Status='P' And PS_CompID=" & sSession.AccessCodeID & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If

            Else
                iMaxID = objGenFun.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Print_Settings", "PS_ID", "PS_CompID")
                If sPath <> "" Then
                    ssql = "" : ssql = "insert into Print_Settings(PS_ID,PS_CAddress,PS_CPhoneNo,PS_CEmail,PS_CVAT,PS_CTax,PS_CPAN,PS_CTAN,PS_CTIN,PS_CCIN,PS_BAddress,PS_BPhoneNo,PS_BEmail,PS_BVAT,PS_BTax,PS_BPAN,PS_BTAN,PS_BTIN,PS_BCIN,PS_Terms,PS_Receiver,PS_Authorise,PS_BIGDATA,PS_SIZE,PS_Status,PS_RptType,PS_FileName,PS_Extn,PS_CompID) values (1," & iPCAddress & "," & iPCPhNo & "," & iPCEmail & "," & iPCVAT & "," & iPCTAX & "," & iPCPAN & "," & iPCTAN & "," & iPCTIN & "," & iPCCIN & "," & iPBAddress & "," & iPBPhNo & "," & iPBEmail & "," & iPBVAT & "," & iPBTAX & "," & iPBPAN & "," & iPBTAN & "," & iPBTIN & "," & iPBCIN & "," & iPTerms & "," & iPReceivers & "," & iPAuthorised & ",?,?,'P'," & iPSizeWiseReport & ",'" & sFileName & "','" & sExt & "'," & sSession.AccessCodeID & ")"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@PS_BIGDATA", ImgByte)
                    com.Parameters.Add("@PS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "insert into Print_Settings(PS_ID,PS_CAddress,PS_CPhoneNo,PS_CEmail,PS_CVAT,PS_CTax,PS_CPAN,PS_CTAN,PS_CTIN,PS_CCIN,PS_BAddress,PS_BPhoneNo,PS_BEmail,PS_BVAT,PS_BTax,PS_BPAN,PS_BTAN,PS_BTIN,PS_BCIN,PS_Terms,PS_Receiver,PS_Authorise,PS_Status,PS_RptType,PS_CompID) values (" & iMaxID & "," & iPCAddress & "," & iPCPhNo & "," & iPCEmail & "," & iPCVAT & "," & iPCTAX & "," & iPCPAN & "," & iPCTAN & "," & iPCTIN & "," & iPCCIN & "," & iPBAddress & "," & iPBPhNo & "," & iPBEmail & "," & iPBVAT & "," & iPBTAX & "," & iPBPAN & "," & iPBTAN & "," & iPBTIN & "," & iPBCIN & "," & iPTerms & "," & iPReceivers & "," & iPAuthorised & ",'P'," & iPSizeWiseReport & "," & sSession.AccessCodeID & ")"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If
            End If

            Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
            myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
            com.Connection = con
            com.Transaction = myTrans
            com.ExecuteNonQuery()
            myTrans.Commit()
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub lnkbtnSales_Click(sender As Object, e As EventArgs) Handles lnkbtnSales.Click
        Try
            lblTab.Text = 2
            liPurchase.Attributes.Remove("class")
            liSales.Attributes.Add("class", "active")

            divPurchase.Attributes.Add("class", "tab-pane")
            divSales.Attributes.Add("class", "tab-pane active")
            RetreiveDataSales()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSales_Click")
        End Try
    End Sub

    Private Sub lnkbtnPurchase_Click(sender As Object, e As EventArgs) Handles lnkbtnPurchase.Click
        Try
            lblTab.Text = 1
            liPurchase.Attributes.Add("class", "active")
            liSales.Attributes.Remove("class")

            divPurchase.Attributes.Add("class", "tab-pane active")
            divSales.Attributes.Add("class", "tab-pane")

            RetreiveDataPurchase()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPurchase_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim fileName As String = ""
        Try
            If lblTab.Text = "1" Then
                If FileUploadPurchase.FileName <> "" Then
                    fileName = Server.MapPath("~/Images/" + FileUploadPurchase.FileName)
                    Dim folderPath As String = Server.MapPath("~/Images/")
                    If Not Directory.Exists(folderPath) Then
                        Directory.CreateDirectory(folderPath)
                    End If
                    FileUploadPurchase.SaveAs(folderPath & Path.GetFileName(FileUploadPurchase.FileName))
                    lblError.Text = Path.GetFileName(FileUploadPurchase.FileName) + " has been uploaded."

                End If
                SaveToBigDataPurchase(fileName)
                If FileUploadPurchase.FileName <> "" Then
                    Dim sFileName As String = ""
                    sFileName = objPrint.getImageName(sSession.AccessCode, sSession.AccessCodeID, "P")
                    Dim imageDataURL As String = Server.MapPath("~/Images/" + sFileName)
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    myImgP.ImageUrl = imageDataURL1
                End If

            ElseIf lblTab.Text = "2" Then
                If FileUploadSales.FileName <> "" Then
                    fileName = Server.MapPath("~/Images/" + FileUploadSales.FileName)
                    Dim folderPath As String = Server.MapPath("~/Images/")
                    If Not Directory.Exists(folderPath) Then
                        Directory.CreateDirectory(folderPath)
                    End If
                    FileUploadSales.SaveAs(folderPath & Path.GetFileName(FileUploadSales.FileName))
                    lblError.Text = Path.GetFileName(FileUploadSales.FileName) + " has been uploaded."

                End If
                SaveToBigDataSales(fileName)

                If FileUploadSales.FileName <> "" Then
                    Dim sFileName As String = ""
                    sFileName = objPrint.getImageName(sSession.AccessCode, sSession.AccessCodeID, "S")
                    Dim imageDataURL As String = Server.MapPath("~/Images/" + sFileName)
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    myImgS.ImageUrl = imageDataURL1
                End If
            End If
            lblError.Text = "Successfully Saved."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            If lblTab.Text = "1" Then

                chkPCAddress.Checked = False : chkPCPhNo.Checked = False : chkPCEmail.Checked = False : chkPCVAT.Checked = False
                chkPCTAX.Checked = False : chkPCPAN.Checked = False : chkPCTAN.Checked = False : chkPCTIN.Checked = False : chkPCCIN.Checked = False
                '----------------'
                'Buyer'
                chkPBAddress.Checked = False : chkPBPhNo.Checked = False : chkPBEmail.Checked = False : chkPBVAT.Checked = False : chkPBTAX.Checked = False
                chkBPAN.Checked = False : chkPBTAN.Checked = False : chkPBTIN.Checked = False : chkPBCIN.Checked = False

                '-------------'
                chkPTerms.Checked = False : chkPReceivers.Checked = False : chkPAuthorised.Checked = False : chkPSizeWiseReport.Checked = False

            ElseIf lblTab.Text = "2" Then

                chkCAddress.Checked = False : chkCPhNo.Checked = False : chkCEmail.Checked = False : chkCVAT.Checked = False : chkCTAX.Checked = False
                chkCPAN.Checked = False : chkCTAN.Checked = False : chkCTIN.Checked = False : chkCCIN.Checked = False
                '----------------'

                'Buyer'
                chkBAddress.Checked = False : chkBPhNo.Checked = False : chkBEmail.Checked = False : chkBVAT.Checked = False
                chkBTAX.Checked = False : chkBPAN.Checked = False : chkBTAN.Checked = False : chkBTIN.Checked = False : chkBCIN.Checked = False
                '-------------'

                chkTerms.Checked = False : chkReceivers.Checked = False : chkAuthorised.Checked = False : chkSizeWiseReport.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
End Class
