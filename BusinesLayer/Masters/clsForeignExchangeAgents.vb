Imports System.IO
Public Class clsForeignExchangeAgents
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared objclsFASGeneral As New clsFASGeneral

    Private iFE_ID As Integer
    Private sFE_AgencyName As String
    Private sFE_ContactName As String
    Private dFE_MobileNo As Double
    Private sFE_Address As String
    Private iFE_Country As Integer
    Private sFE_FAX As String
    Private sFE_PostalCode As String
    Private iFE_GSTNCategory As Integer
    Private dFE_PhoneNo As Double
    Private sFE_GSTNRegNO As String
    Private iFE_City As Integer
    Private iFE_State As Integer
    Private sFE_Website As String
    Private sFE_EMail As String
    Private sFE_Bank As String
    Private dFE_ACCNO As Double
    Private sFE_IFSC As String
    Private sFE_BranchName As String
    Private sFE_Status As String
    Private iFE_CRBY As Integer
    Private iFE_UpdatedBy As Integer
    Private sFE_IPAddress As String
    Private iFE_CompID As Integer

    Private iFEA_ID As Integer
    Private iFEA_FEID As Integer
    Private iFEA_Currency As Integer
    Private iFEA_CRBY As Integer
    Private iFEA_CompID As Integer

    Public Property FE_ID() As Integer
        Get
            Return (iFE_ID)
        End Get
        Set(ByVal Value As Integer)
            iFE_ID = Value
        End Set
    End Property
    Public Property FE_AgencyName() As String
        Get
            Return (sFE_AgencyName)
        End Get
        Set(ByVal Value As String)
            sFE_AgencyName = Value
        End Set
    End Property

    Public Property FE_ContactName() As String
        Get
            Return (sFE_ContactName)
        End Get
        Set(ByVal Value As String)
            sFE_ContactName = Value
        End Set
    End Property
    Public Property FE_MobileNo() As Double
        Get
            Return (dFE_MobileNo)
        End Get
        Set(ByVal Value As Double)
            dFE_MobileNo = Value
        End Set
    End Property
    Public Property FE_Address() As String
        Get
            Return (sFE_Address)
        End Get
        Set(ByVal Value As String)
            sFE_Address = Value
        End Set
    End Property
    Public Property FE_Country() As Integer
        Get
            Return (iFE_Country)
        End Get
        Set(ByVal Value As Integer)
            iFE_Country = Value
        End Set
    End Property
    Public Property FE_FAX() As String
        Get
            Return (sFE_FAX)
        End Get
        Set(ByVal Value As String)
            sFE_FAX = Value
        End Set
    End Property
    Public Property FE_PostalCode() As String
        Get
            Return (sFE_PostalCode)
        End Get
        Set(ByVal Value As String)
            sFE_PostalCode = Value
        End Set
    End Property
    Public Property FE_GSTNCategory() As Integer
        Get
            Return (iFE_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iFE_GSTNCategory = Value
        End Set
    End Property
    Public Property FE_PhoneNo() As Double
        Get
            Return (dFE_PhoneNo)
        End Get
        Set(ByVal Value As Double)
            dFE_PhoneNo = Value
        End Set
    End Property
    Public Property FE_GSTNRegNO() As String
        Get
            Return (sFE_GSTNRegNO)
        End Get
        Set(ByVal Value As String)
            sFE_GSTNRegNO = Value
        End Set
    End Property
    Public Property FE_City() As Integer
        Get
            Return (iFE_City)
        End Get
        Set(ByVal Value As Integer)
            iFE_City = Value
        End Set
    End Property
    Public Property FE_State() As Integer
        Get
            Return (iFE_State)
        End Get
        Set(ByVal Value As Integer)
            iFE_State = Value
        End Set
    End Property
    Public Property FE_Website() As String
        Get
            Return (sFE_Website)
        End Get
        Set(ByVal Value As String)
            sFE_Website = Value
        End Set
    End Property
    Public Property FE_EMail() As String
        Get
            Return (sFE_EMail)
        End Get
        Set(ByVal Value As String)
            sFE_EMail = Value
        End Set
    End Property
    Public Property FE_Bank() As String
        Get
            Return (sFE_Bank)
        End Get
        Set(ByVal Value As String)
            sFE_Bank = Value
        End Set
    End Property
    Public Property FE_ACCNO() As Double
        Get
            Return (dFE_ACCNO)
        End Get
        Set(ByVal Value As Double)
            dFE_ACCNO = Value
        End Set
    End Property
    Public Property FE_IFSC() As String
        Get
            Return (sFE_IFSC)
        End Get
        Set(ByVal Value As String)
            sFE_IFSC = Value
        End Set
    End Property
    Public Property FE_BranchName() As String
        Get
            Return (sFE_BranchName)
        End Get
        Set(ByVal Value As String)
            sFE_BranchName = Value
        End Set
    End Property
    Public Property FE_Status() As String
        Get
            Return (sFE_Status)
        End Get
        Set(ByVal Value As String)
            sFE_Status = Value
        End Set
    End Property
    Public Property FE_CRBY() As Integer
        Get
            Return (iFE_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iFE_CRBY = Value
        End Set
    End Property
    Public Property FE_UpdatedBy() As Integer
        Get
            Return (iFE_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iFE_UpdatedBy = Value
        End Set
    End Property
    Public Property FE_IPAddress() As String
        Get
            Return (sFE_IPAddress)
        End Get
        Set(ByVal Value As String)
            sFE_IPAddress = Value
        End Set
    End Property
    Public Property FE_CompID() As Integer
        Get
            Return (iFE_CompID)
        End Get
        Set(ByVal Value As Integer)
            iFE_CompID = Value
        End Set
    End Property
    Public Property FEA_ID() As Integer
        Get
            Return (iFEA_ID)
        End Get
        Set(ByVal Value As Integer)
            iFEA_ID = Value
        End Set
    End Property
    Public Property FEA_FEID() As Integer
        Get
            Return (iFEA_FEID)
        End Get
        Set(ByVal Value As Integer)
            iFEA_FEID = Value
        End Set
    End Property
    Public Property FEA_Currency() As Integer
        Get
            Return (iFEA_Currency)
        End Get
        Set(ByVal Value As Integer)
            iFEA_Currency = Value
        End Set
    End Property
    Public Property FEA_CRBY() As Integer
        Get
            Return (iFEA_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iFEA_CRBY = Value
        End Set
    End Property
    Public Property FEA_CompID() As Integer
        Get
            Return (iFEA_CompID)
        End Get
        Set(ByVal Value As Integer)
            iFEA_CompID = Value
        End Set
    End Property
    Public Function LoadAgentsForeignExchangeDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim sToday As String = "", sFrom As String = "", sTo As String = "", sOpSymbol As String = ""
        Dim sTTBuy As String = "", sTTSell As String = "", sBuy As String = "", sSell As String = ""
        Try
            dtDisplay.Columns.Add("SrNo")
            dtDisplay.Columns.Add("ID")
            dtDisplay.Columns.Add("AgencyName")
            dtDisplay.Columns.Add("ContactPerson")
            dtDisplay.Columns.Add("Email")
            dtDisplay.Columns.Add("AgencyAddress")
            dtDisplay.Columns.Add("AgencyBank")
            dtDisplay.Columns.Add("Status")

            sSql = "" : sSql = " Select FE_ID,FE_AgencyName,FE_ContactName,FE_EMail,FE_Address,FE_Bank,FE_DelFlag From MST_ForeignExchange_Agents"
            sSql = sSql & " Where FE_CompID=" & iCompID & " Order By FE_AgencyName"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("FE_ID")
                    If IsDBNull(dt.Rows(i)("FE_AgencyName")) = False Then
                        dRow("AgencyName") = dt.Rows(i)("FE_AgencyName")
                    End If
                    If IsDBNull(dt.Rows(i)("FE_ContactName")) = False Then
                        dRow("ContactPerson") = dt.Rows(i)("FE_ContactName")
                    End If
                    If IsDBNull(dt.Rows(i)("FE_EMail")) = False Then
                        dRow("Email") = dt.Rows(i)("FE_EMail")
                    End If
                    If IsDBNull(dt.Rows(i)("FE_Address")) = False Then
                        dRow("AgencyAddress") = dt.Rows(i)("FE_Address")
                    End If
                    If IsDBNull(dt.Rows(i)("FE_Bank")) = False Then
                        dRow("AgencyBank") = dt.Rows(i)("FE_Bank")
                    End If
                    If IsDBNull(dt.Rows(i)("FE_DelFlag")) = False Then
                        If dt.Rows(i)("FE_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("FE_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("FE_DelFlag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        End If
                    End If
                    dtDisplay.Rows.Add(dRow)
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 3 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadState(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 4 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCountry(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 5 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GC_ID,GC_GSTCategory From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAgencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From MST_ForeignExchange_Agents Where FE_ID=" & iPKID & " And FE_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAgentsDetails(ByVal sNameSpace As String, ByVal objForeignExchangeAgents As clsForeignExchangeAgents) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_AgencyName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_AgencyName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_ContactName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_ContactName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_PhoneNo", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.dFE_PhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_MobileNo", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.dFE_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_Address", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_PostalCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_PostalCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_City", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_State", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_Country", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_Country
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_FAX", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_FAX
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_GSTNCategory", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_GSTNRegNO", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_GSTNRegNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_Website", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_Website
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_EMail", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_EMail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_Bank", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_Bank
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_ACCNO", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.dFE_ACCNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_IFSC", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_IFSC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_BranchName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_CRBY", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_UpdatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.sFE_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFE_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spMST_ForeignExchange_Agents", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAgencyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sAgency As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select FE_ID From MST_ForeignExchange_Agents Where FE_AgencyName='" & sAgency & "' And FE_CompID=" & iCompID & " "
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveAgencyStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update MST_ForeignExchange_Agents set"
            If sType = "Created" Then
                sSql = sSql & " FE_DelFlag='A',FE_Status='A',FE_APPROVEDBY=" & iSessionUsrID & ", FE_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " FE_DelFlag='D',FE_Status='AD',FE_DeletedBy=" & iSessionUsrID & ", FE_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " FE_DelFlag='A',FE_Status='AR',FE_RecallBy=" & iSessionUsrID & ", FE_RecallOn=Getdate(),"
            End If
            sSql = sSql & "FE_IPAddress='" & sIPAddress & "' Where FE_CompID=" & iACID & " And FE_ID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveAgentsCurrencyDetails(ByVal sNameSpace As String, ByVal objForeignExchangeAgents As clsForeignExchangeAgents) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(6) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FEA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFEA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FEA_FEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFEA_FEID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FEA_Currency", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFEA_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FEA_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFEA_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FEA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objForeignExchangeAgents.iFEA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spMST_FEAgents_Currency", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteAgencyCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from MST_FEAgents_Currency Where FEA_FEID =" & iID & " And FEA_CompID =" & iCompID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAgencyCurrencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From MST_FEAgents_Currency Where FEA_FEID=" & iPKID & " And FEA_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAgencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select FE_ID,FE_AgencyName From MST_ForeignExchange_Agents Where FE_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class