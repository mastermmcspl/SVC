Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsUsersUpload
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private Usr_ID As Integer
    Private Usr_Node As Integer
    Private Usr_Code As String
    Private Usr_FullName As String
    Private Usr_LoginName As String
    Private Usr_Password As String
    Private Usr_Email As String
    Private Usr_LevelGrp As Integer
    Private Usr_DutyStatus As String
    Private Usr_PhoneNo As String
    Private Usr_MobileNo As String
    Private Usr_OfficePhone As String
    Private Usr_OffPhExtn As String
    Private Usr_Designation As Integer
    Private Usr_CompanyID As Integer
    Private Usr_OrgID As Integer
    Private Usr_GrpOrUserLvlPerm As Integer
    Private Usr_NoOfUnsucsfAtteptts As Integer
    Private Usr_Ques As String
    Private Usr_Ans As String
    Private Usr_SentMail As Integer
    Private Usr_Partner As Integer
    Private Usr_NoOfLogin As Integer
    Private Usr_LastLoginDate As Date
    Private Usr_CreatedBy As Integer
    Private Usr_CreatedOn As Date
    Private Usr_UpdatedBy As Integer
    Private Usr_UpdatedOn As Date
    Private Usr_AppBy As Integer
    Private Usr_AppOn As Date
    Private Usr_DeletedBy As Integer
    Private Usr_DeletedOn As Date
    Private Usr_RecallBy As Integer
    Private Usr_RecallOn As Date
    Private Usr_Flag As String
    Private Usr_Status As String
    Private Usr_CompId As Integer
    Private Usr_Role As Integer
    Private Usr_MasterModule As Integer
    Private Usr_PurchaseModule As Integer
    Private Usr_SalesModule As Integer
    Private Usr_AccountsModule As Integer
    Private Usr_MasterRole As Integer
    Private Usr_PurchaseRole As Integer
    Private Usr_SalesRole As Integer
    Private Usr_AccountsRole As Integer
    Private Usr_IPAdress As String
    Private Usr_Address As String
    Public Property iUsrPurchaseModule() As Integer
        Get
            Return (Usr_PurchaseModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseModule = Value
        End Set
    End Property
    Public Property iUsrPurchaseRole() As Integer
        Get
            Return (Usr_PurchaseRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseRole = Value
        End Set
    End Property
    Public Property iUsrSalesRole() As Integer
        Get
            Return (Usr_SalesRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesRole = Value
        End Set
    End Property
    Public Property iUsrSalesModule() As Integer
        Get
            Return (Usr_SalesModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesModule = Value
        End Set
    End Property
    Public Property iUsrAccountsRole() As Integer
        Get
            Return (Usr_AccountsRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsRole = Value
        End Set
    End Property

    Public Property iUsrAccountsModule() As Integer
        Get
            Return (Usr_AccountsModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsModule = Value
        End Set
    End Property

    Public Property iUsrMasterModule() As Integer
        Get
            Return (Usr_MasterModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterModule = Value
        End Set
    End Property
    Public Property iUsrMasterRole() As Integer
        Get
            Return (Usr_MasterRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterRole = Value
        End Set
    End Property
    Public Property iUsrRole() As Integer
        Get
            Return (Usr_Role)
        End Get
        Set(ByVal Value As Integer)
            Usr_Role = Value
        End Set
    End Property
    Public Property iUsrCompID() As Integer
        Get
            Return (Usr_CompId)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompId = Value
        End Set
    End Property
    Public Property sUsrStatus() As String
        Get
            Return (Usr_Status)
        End Get
        Set(ByVal Value As String)
            Usr_Status = Value
        End Set
    End Property
    Public Property sUsrFlag() As String
        Get
            Return (Usr_Flag)
        End Get
        Set(ByVal Value As String)
            Usr_Flag = Value
        End Set
    End Property
    Public Property dUsrRecallOn() As Date
        Get
            Return (Usr_RecallOn)
        End Get
        Set(ByVal Value As Date)
            Usr_RecallOn = Value
        End Set
    End Property
    Public Property iUsrRecallBy() As Integer
        Get
            Return (Usr_RecallBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_RecallBy = Value
        End Set
    End Property
    Public Property dUsrDeletedOn() As Date
        Get
            Return (Usr_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_DeletedOn = Value
        End Set
    End Property
    Public Property iUsrDeletedBy() As Integer
        Get
            Return (Usr_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_DeletedBy = Value
        End Set
    End Property
    Public Property dUsrAppOn() As Date
        Get
            Return (Usr_AppOn)
        End Get
        Set(ByVal Value As Date)
            Usr_AppOn = Value
        End Set
    End Property
    Public Property iUsrAppBy() As Integer
        Get
            Return (Usr_AppBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_AppBy = Value
        End Set
    End Property
    Public Property dUsrUpdatedOn() As Date
        Get
            Return (Usr_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_UpdatedOn = Value
        End Set
    End Property
    Public Property iUsrUpdatedBy() As Integer
        Get
            Return (Usr_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_UpdatedBy = Value
        End Set
    End Property
    Public Property dUsrCreatedOn() As Date
        Get
            Return (Usr_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_CreatedOn = Value
        End Set
    End Property
    Public Property iUsrCreatedBy() As Integer
        Get
            Return (Usr_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_CreatedBy = Value
        End Set
    End Property
    Public Property dUsrLastLoginDate() As Date
        Get
            Return (Usr_LastLoginDate)
        End Get
        Set(ByVal Value As Date)
            Usr_LastLoginDate = Value
        End Set
    End Property
    Public Property iUsrNoOfLogin() As Integer
        Get
            Return (Usr_NoOfLogin)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfLogin = Value
        End Set
    End Property
    Public Property iUsrPartner() As Integer
        Get
            Return (Usr_Partner)
        End Get
        Set(ByVal Value As Integer)
            Usr_Partner = Value
        End Set
    End Property
    Public Property iUsrSentMail() As Integer
        Get
            Return (Usr_SentMail)
        End Get
        Set(ByVal Value As Integer)
            Usr_SentMail = Value
        End Set
    End Property
    Public Property sUsrAns() As String
        Get
            Return (Usr_Ans)
        End Get
        Set(ByVal Value As String)
            Usr_Ans = Value
        End Set
    End Property
    Public Property sUsrQues() As String
        Get
            Return (Usr_Ques)
        End Get
        Set(ByVal Value As String)
            Usr_Ques = Value
        End Set
    End Property
    Public Property iUsrNoOfUnsucsfAtteptts() As Integer
        Get
            Return (Usr_NoOfUnsucsfAtteptts)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfUnsucsfAtteptts = Value
        End Set
    End Property
    Public Property iUsrGrpOrUserLvlPerm() As Integer
        Get
            Return (Usr_GrpOrUserLvlPerm)
        End Get
        Set(ByVal Value As Integer)
            Usr_GrpOrUserLvlPerm = Value
        End Set
    End Property
    Public Property iUsrOrgID() As Integer
        Get
            Return (Usr_OrgID)
        End Get
        Set(ByVal Value As Integer)
            Usr_OrgID = Value
        End Set
    End Property
    Public Property iUsrCompanyID() As Integer
        Get
            Return (Usr_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompanyID = Value
        End Set
    End Property
    Public Property iUsrDesignation() As Integer
        Get
            Return (Usr_Designation)
        End Get
        Set(ByVal Value As Integer)
            Usr_Designation = Value
        End Set
    End Property
    Public Property sUsrOffPhExtn() As String
        Get
            Return (Usr_OffPhExtn)
        End Get
        Set(ByVal Value As String)
            Usr_OffPhExtn = Value
        End Set
    End Property
    Public Property sUsrOfficePhone() As String
        Get
            Return (Usr_OfficePhone)
        End Get
        Set(ByVal Value As String)
            Usr_OfficePhone = Value
        End Set
    End Property
    Public Property sUsrPhoneNo() As String
        Get
            Return (Usr_PhoneNo)
        End Get
        Set(ByVal Value As String)
            Usr_PhoneNo = Value
        End Set
    End Property
    Public Property sUsrMobileNo() As String
        Get
            Return (Usr_MobileNo)
        End Get
        Set(ByVal Value As String)
            Usr_MobileNo = Value
        End Set
    End Property
    Public Property sUsrDutyStatus() As String
        Get
            Return (Usr_DutyStatus)
        End Get
        Set(ByVal Value As String)
            Usr_DutyStatus = Value
        End Set
    End Property
    Public Property iUsrLevelGrp() As Integer
        Get
            Return (Usr_LevelGrp)
        End Get
        Set(ByVal Value As Integer)
            Usr_LevelGrp = Value
        End Set
    End Property
    Public Property sUsrEmail() As String
        Get
            Return (Usr_Email)
        End Get
        Set(ByVal Value As String)
            Usr_Email = Value
        End Set
    End Property
    Public Property sUsrPassword() As String
        Get
            Return (Usr_Password)
        End Get
        Set(ByVal Value As String)
            Usr_Password = Value
        End Set
    End Property
    Public Property sUsrLoginName() As String
        Get
            Return (Usr_LoginName)
        End Get
        Set(ByVal Value As String)
            Usr_LoginName = Value
        End Set
    End Property
    Public Property sUsrFullName() As String
        Get
            Return (Usr_FullName)
        End Get
        Set(ByVal Value As String)
            Usr_FullName = Value
        End Set
    End Property
    Public Property sUsrCode() As String
        Get
            Return (Usr_Code)
        End Get
        Set(ByVal Value As String)
            Usr_Code = Value
        End Set
    End Property
    Public Property iUsrNode() As Integer
        Get
            Return (Usr_Node)
        End Get
        Set(ByVal Value As Integer)
            Usr_Node = Value
        End Set
    End Property
    Public Property iUserID() As Integer
        Get
            Return (Usr_ID)
        End Get
        Set(ByVal Value As Integer)
            Usr_ID = Value
        End Set
    End Property
    Public Property sUsrIPAdress() As String
        Get
            Return (Usr_IPAdress)
        End Get
        Set(ByVal Value As String)
            Usr_IPAdress = Value
        End Set
    End Property
    Public Property sUsrAddress() As String
        Get
            Return (Usr_Address)
        End Get
        Set(ByVal Value As String)
            Usr_Address = Value
        End Set
    End Property


    Public Function SaveSupplierMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sCode As String = "", sName As String = "", sContactPerson As String = "", sEmail As String = ""
        Dim sMobileNo As String = "", sLandLineNo As String = "", sFax As String = "", sAddress As String = ""
        Dim sPincode As String = "", sCity As String = "", sState As String = ""
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            For i = 0 To dtUpload.Rows.Count - 1

                'Code
                If dtUpload.Rows(i)(0).ToString() = "" Then
                    sCode = ""
                Else
                    sCode = dtUpload.Rows(i)(0).ToString()
                End If

                'Name
                If dtUpload.Rows(i)(1).ToString() = "" Then
                    sName = ""
                Else
                    sName = dtUpload.Rows(i)(1).ToString()
                End If

                'ContactPerson
                If dtUpload.Rows(i)(2).ToString() = "" Then
                    sContactPerson = ""
                Else
                    sContactPerson = dtUpload.Rows(i)(2).ToString()
                End If

                'Email
                If dtUpload.Rows(i)(3).ToString() = "" Then
                    sEmail = ""
                Else
                    sEmail = dtUpload.Rows(i)(3).ToString()
                End If

                'MobileNo
                If dtUpload.Rows(i)(4).ToString() = "" Then
                    sMobileNo = ""
                Else
                    sMobileNo = dtUpload.Rows(i)(4).ToString()
                End If

                'LandLineNo
                If dtUpload.Rows(i)(5).ToString() = "" Then
                    sLandLineNo = ""
                Else
                    sLandLineNo = dtUpload.Rows(i)(5).ToString()
                End If

                'Fax
                If dtUpload.Rows(i)(6).ToString() = "" Then
                    sFax = ""
                Else
                    sFax = dtUpload.Rows(i)(6).ToString()
                End If

                'Address
                If dtUpload.Rows(i)(7).ToString() = "" Then
                    sAddress = ""
                Else
                    sAddress = dtUpload.Rows(i)(7).ToString()
                End If

                'Pincode
                If dtUpload.Rows(i)(8).ToString() = "" Then
                    sPincode = ""
                Else
                    sPincode = dtUpload.Rows(i)(8).ToString()
                End If

                'City
                If dtUpload.Rows(i)(9).ToString() = "" Then
                    sCity = "0"
                Else
                    sCity = "0" 'clsSupplierUpload.CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(9).ToString(), 0)
                End If

                'State
                If dtUpload.Rows(i)(10).ToString() = "" Then
                    sState = "0"
                Else
                    sState = "0" ' clsSupplierUpload.CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(10).ToString(), 1)
                End If

                sSql = "" : sSql = "Select * from CustomerSupplierMaster where CSM_Code ='" & sCode & "' and CSM_CompID=" & iCompID & " "
                dr = objDBL.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    sSql = "" : sSql = "Update CustomerSupplierMaster set CSM_Name = '" & sNameSpace & "',CSM_ContactPerson = '" & sContactPerson & "',CSM_EmailID = '" & sEmail & "',"
                    sSql = sSql & "CSM_MobileNo = '" & sMobileNo & "',CSM_LandLineNo = '" & sLandLineNo & "',CSM_Fax = '" & sFax & "',CSM_Address = '" & sAddress & "',"
                    sSql = sSql & "CSM_PinCode = '" & sPincode & "',CSM_City = " & sCity & ",CSM_State = " & sState & " where CSM_Code ='" & sCode & "' and CSM_CompID = " & iCompID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Else
                    iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "CustomerSupplierMaster", "CSM_ID", "CSM_CompID")
                    sSql = "" : sSql = "Insert into CustomerSupplierMaster(CSM_ID,CSM_Name,CSM_Code,CSM_Inventry,"
                    sSql = sSql & "CSM_ContactPerson,CSM_EmailID,CSM_MobileNo,CSM_LandLineNo,"
                    sSql = sSql & "CSM_Fax,CSM_Address,CSM_PinCode,CSM_City,CSM_State,"
                    sSql = sSql & "CSM_Delflag,CSM_CompID,CSM_Status,CSM_Operation,CSM_IPAddress,"
                    sSql = sSql & "CSM_CreatedBy,CSM_CreatedOn)"
                    sSql = sSql & "Values(" & iMax & ",'" & RemoveQuote(sName) & "','" & RemoveQuote(sCode) & "',0,"
                    sSql = sSql & "'" & RemoveQuote(sContactPerson) & "','" & RemoveQuote(sEmail) & "','" & sMobileNo & "','" & sLandLineNo & "',"
                    sSql = sSql & "'" & sFax & "','" & RemoveQuote(sAddress) & "','" & sPincode & "'," & sCity & "," & sState & ","
                    sSql = sSql & "'A'," & iCompID & ",'A','W','" & sIPAddress & "'," & iUserID & ",GetDate())"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCity As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 3"
            Else
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 4"
            End If

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCityExistOrNot = dr("Mas_Id")
            Else
                CheckCityExistOrNot = 0
            End If
            dr.Close()
            Return CheckCityExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveUserData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iYear As Integer, ByVal sCode As String, ByVal sName As String, ByVal sLoginname As String, ByVal sEmail As String, ByVal Designation As String, ByVal sMobile As String, ByVal Address As String, ByVal Squestion As String, ByVal Sanswer As String, ByVal Password As String)
        Dim ssql As String
        Dim iBM_Id As Integer
        Dim iBMId As Integer
        Dim sDescCode As String = ""
        Dim iParent As Integer
        Dim sSubGrp As String = ""
        Try
            iBM_Id = objDBL.SQLExecuteScalarInt(sNameSpace, "select usr_id from sad_UserDetails where usr_Code='" & sCode & "' and usr_CompId=" & iCompID & " ")
            If iBM_Id = 0 Then
                iBMId = objDBL.SQLExecuteScalar(sNameSpace, "Select  IsNull(MAX(usr_id),0)+1 from sad_UserDetails")
                'sCode = "C-" & "" & iBMId & ""
                'ssql = "Insert into Buyers_Masters(BM_ID,BM_IndType,BM_CustType,BM_Name,BM_Code,BM_Under,BM_Inventry,BM_MailName,BM_Address,BM_State,BM_Pincode,BM_PAN,BM_TIN,BM_Delflag,BM_CompID,BM_CreatedBy,BM_CreatedOn,BM_ApprovedBy,BM_ApprovedOn,BM_DeletedBy,BM_DeletedOn,BM_Status,BM_UpdatedBy,BM_UpdatedOn,BM_Contact,BM_City,BM_Country,BM_Office,BM_MobileNo,BM_Email,BM_Fax,BM_Year)"
                ssql = "Insert into sad_UserDetails(usr_id,usr_Code,usr_FullName,usr_LoginName,usr_Email,usr_DutyStatus,usr_Designation,usr_MobileNo,usr_DelFlag,usr_AppBy,usr_AppOn,usr_CreatedBy,usr_CreatedOn,USR_Address,Usr_CompID,usr_Que,usr_Ans,usr_PassWord)"
                ssql = ssql & "values(" & iBMId & ",'" & sCode & "','" & sName & "','" & sLoginname & "','" & sEmail & "','A','" & Designation & "','" & sMobile & "','A'," & iUserID & ",GetDate()," & iUserID & ",GetDate(),'" & Address & "'," & iCompID & ",'" & Squestion & "','" & Sanswer & "','" & Password & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            Else
                'ssql = "Update sad_UserDetails set BM_Name='" & sName & "',BM_Address='" & sAddress & "',BM_Pincode='" & sPinCode & "',BM_ContactPerson='" & sContact & "',BM_LandLineNo='" & sOffice & "',BM_MobileNo='" & sMobile & "',BM_EmailID='" & sMail & "',BM_City= '" & city & "',BM_State='" & State & "',BM_Fax='" & FaxNo & "',BM_UpdatedBy=" & iUserID & ",BM_UpdatedOn=GetDate() where BM_Code='" & sCode & "' And BM_Id=" & iBM_Id & " and BM_CompID=" & iCompID & " and BM_Year=" & iYear & " "
                'objDBL.ExecuteNoNQuery(sNameSpace, ssql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckUserDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnit As String) As Boolean
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select * from acc_general_master where Mas_Desc ='" & sUnit & "' and mas_Master = 6 and Mas_CompID = " & iCompID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmployeeDetails(ByVal sAC As String, ByVal objEmp As clsUsersUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(33) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Node", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrNode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_FullName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrFullName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LoginName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrLoginName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Password", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrPassword
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Email", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LevelGrp", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrLevelGrp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DutyStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objEmp.sUsrDutyStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PhoneNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrPhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MobileNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrMobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OfficePhone", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrOfficePhone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OffPhExtn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrOffPhExtn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Designation", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrDesignation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OrgnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrOrgID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_GrpOrUserLvlPerm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrGrpOrUserLvlPerm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Role", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrMasterModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrPurchaseModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrSalesModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrAccountsModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrMasterRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrPurchaseRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrSalesRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrAccountsRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objEmp.sUsrFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objEmp.sUsrStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_IPAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.Usr_IPAdress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CompId", OleDb.OleDbType.Integer, 50)
            ObjParam(iParamCount).Value = objEmp.iUsrCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Address", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objEmp.sUsrAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEmployeeMasterUserUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
