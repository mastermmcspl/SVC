Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsPumpBilling

    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iLPB_ID As Integer
    Private iLPB_PumpID As Integer
    Private dLPB_FromDate As DateTime
    Private dLPB_ToDate As DateTime
    Private dLPB_BillDate As DateTime
    Private sLPB_BillNo As String
    Private dLPB_TotalLtr As Double
    Private dLPB_TotalDieselAmt As Double
    Private dLPB_AdvanceAmt As Double
    Private dLPB_OtherExpense As Double
    Private dLPB_TotalAmt As Double
    Private sLPB_TCS As String
    Private sLPB_Delflag As String
    Private iLPB_CompID As Integer
    Private iLPB_YearID As Integer
    Private sLPB_Status As String
    Private sLPB_Operation As String
    Private sLPB_IPAddress As String
    Private iLPB_CreatedBy As Integer
    Private dLPB_CreatedOn As DateTime
    Private iLPB_ApprovedBy As Integer
    Private dLPB_ApprovedOn As DateTime
    Private iLPB_DeletedBy As Integer
    Private dLPB_DeletedOn As DateTime
    Private iLPB_UpdatedBy As Integer
    Private dLPB_UpdatedOn As DateTime
    Private iLPB_RecalldBy As Integer
    Public Property LPB_ID() As Integer
        Get
            Return (iLPB_ID)
        End Get
        Set(ByVal Value As Integer)
            iLPB_ID = Value
        End Set
    End Property

    Public Property LPB_PumpID() As Integer
        Get
            Return (iLPB_PumpID)
        End Get
        Set(ByVal Value As Integer)
            iLPB_PumpID = Value
        End Set
    End Property
    Public Property LPB_FromDate() As Date
        Get
            Return (dLPB_FromDate)
        End Get
        Set(ByVal Value As Date)
            dLPB_FromDate = Value
        End Set
    End Property
    Public Property LPB_ToDate() As Date
        Get
            Return (dLPB_ToDate)
        End Get
        Set(ByVal Value As Date)
            dLPB_ToDate = Value
        End Set
    End Property
    Public Property LPB_BillDate() As Date
        Get
            Return (dLPB_BillDate)
        End Get
        Set(ByVal Value As Date)
            dLPB_BillDate = Value
        End Set
    End Property

    Public Property LPB_TCS() As String
        Get
            Return (sLPB_TCS)
        End Get
        Set(ByVal Value As String)
            sLPB_TCS = Value
        End Set
    End Property
    Public Property LPB_TotalLtr() As Double
        Get
            Return (dLPB_TotalLtr)
        End Get
        Set(ByVal Value As Double)
            dLPB_TotalLtr = Value
        End Set
    End Property
    Public Property LPB_TotalDieselAmt() As Double
        Get
            Return (dLPB_TotalDieselAmt)
        End Get
        Set(ByVal Value As Double)
            dLPB_TotalDieselAmt = Value
        End Set
    End Property
    Public Property LPB_AdvanceAmt() As Double
        Get
            Return (dLPB_AdvanceAmt)
        End Get
        Set(ByVal Value As Double)
            dLPB_AdvanceAmt = Value
        End Set
    End Property
    Public Property LPB_OtherExpense() As Double
        Get
            Return (dLPB_OtherExpense)
        End Get
        Set(ByVal Value As Double)
            dLPB_OtherExpense = Value
        End Set
    End Property
    Public Property LPB_TotalAmt() As Double
        Get
            Return (dLPB_TotalAmt)
        End Get
        Set(ByVal Value As Double)
            dLPB_TotalAmt = Value
        End Set
    End Property
    Public Property LPB_BillNo() As String
        Get
            Return (sLPB_BillNo)
        End Get
        Set(ByVal Value As String)
            sLPB_BillNo = Value
        End Set
    End Property
    Public Property LPB_Delflag() As String
        Get
            Return (sLPB_Delflag)
        End Get
        Set(ByVal Value As String)
            sLPB_Delflag = Value
        End Set
    End Property
    Public Property LPB_CompID() As Integer
        Get
            Return (iLPB_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLPB_CompID = Value
        End Set
    End Property
    Public Property LPB_YearID() As Integer
        Get
            Return (iLPB_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLPB_YearID = Value
        End Set
    End Property
    Public Property LPB_Status() As String
        Get
            Return (sLPB_Status)
        End Get
        Set(ByVal Value As String)
            sLPB_Status = Value
        End Set
    End Property
    Public Property LPB_Operation() As String
        Get
            Return (sLPB_Operation)
        End Get
        Set(ByVal Value As String)
            sLPB_Operation = Value
        End Set
    End Property
    Public Property LPB_IPAddress() As String
        Get
            Return (sLPB_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLPB_IPAddress = Value
        End Set
    End Property
    Public Property LPB_CreatedBy() As Integer
        Get
            Return (iLPB_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPB_CreatedBy = Value
        End Set
    End Property
    Public Property LPB_CreatedOn() As Date
        Get
            Return (dLPB_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLPB_CreatedOn = Value
        End Set
    End Property

    Public Property LPB_ApprovedBy() As Integer
        Get
            Return (iLPB_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPB_ApprovedBy = Value
        End Set
    End Property
    Public Property LPB_ApprovedOn() As Date
        Get
            Return (dLPB_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLPB_ApprovedOn = Value
        End Set
    End Property
    Public Property LPB_DeletedBy() As Integer
        Get
            Return (iLPB_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPB_DeletedBy = Value
        End Set
    End Property
    Public Property LPB_DeletedOn() As Date
        Get
            Return (dLPB_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLPB_DeletedOn = Value
        End Set
    End Property
    Public Property LPB_UpdatedBy() As Integer
        Get
            Return (iLPB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPB_UpdatedBy = Value
        End Set
    End Property
    Public Property LPB_UpdatedOn() As Date
        Get
            Return (dLPB_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLPB_UpdatedOn = Value
        End Set
    End Property
    Public Property LPB_RecalldBy() As Integer
        Get
            Return (iLPB_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLPB_RecalldBy = Value
        End Set
    End Property
    Public Function LoadDieselPump(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LPM_Id,LPM_PumpName from lgst_pump_master where LPM_CompID= " & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iPumpID As Integer, ByVal sFromDate As String, ByVal sToDate As String) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dTotalAmt As Double = 0.0
        Try
            dt.Columns.Add("PumpName")
            dt.Columns.Add("TotalLtr")
            dt.Columns.Add("TotalDieselAmt")
            dt.Columns.Add("AdvncAmt")
            dt.Columns.Add("OtherExpense")
            dt.Columns.Add("TotalAmt")
            sSql = "Select sum(LTGDD_DieselinLtrs) as TotalLtr,sum(LTGDD_DieselAmount) as TotalDieselAmt,sum(LTGDD_DriverAdvancGvnByPump) as AdvncAmt,sum(LTGDD_OtherExpenses) as OtherExpenses "
            sSql = sSql & " from Lgst_TripGenDiesel_Details Where LTGDD_YearID=" & iYearId & " and LTGDD_DelFlag <> 'D' and  LTGDD_PumpId=" & iPumpID & " And LTGDD_CompID=" & iCompID & "" ' and LTGDD_TripID in ("
            If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
                sSql = sSql & " and LTGDD_IndDate Between '" & sFromDate & "' And '" & sToDate & "' "
            End If
            '  sSql = sSql & "Select LTGM_ID from Lgst_TripGeneration_Master where LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearId & "") ' and LTGM_TripStatus=2 "

            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("PumpName") = GetDieselPumpName(sNameSpace, iPumpID, iCompID)
                    dRow("TotalLtr") = String.Format("{0:0.00}", Convert.ToDecimal(dr("TotalLtr")))
                    dRow("TotalDieselAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("TotalDieselAmt")))
                    dRow("AdvncAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("AdvncAmt")))
                    dRow("OtherExpense") = String.Format("{0:0.00}", Convert.ToDecimal(dr("OtherExpenses")))
                    dRow("TotalAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("TotalDieselAmt") + dr("AdvncAmt") + dr("OtherExpenses")))
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDieselPumpName(ByVal sNameSpace As String, ByVal iPumpID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID = " & iPumpID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LPM_PumpName").ToString()) = False Then
                    sPump = dt.Rows(0)("LPM_PumpName").ToString()
                Else
                    sPump = ""
                End If
            Else
                sPump = ""
            End If
            Return sPump
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function GetCOAHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_AccHead from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Try
            sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_id=" & iSubGL & " and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & " "
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartySubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As Integer
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID=" & iPartyID & " And LPM_CompID=" & iCompID & " and LPM_Delflag='A' "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHeadID As Integer, ByVal iGLID As Integer) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Try
            sSql = "Select gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iHeadID & " And gl_Id=" & iGLID & " order by gl_glcode"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartySubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID=" & iPartyID & " And LPM_CompID=" & iCompID & " and LPM_Delflag='A' "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePumpBilling(ByVal sNameSpace As String, ByVal objLPB As clsPumpBilling) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_PumpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_PumpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_FromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLPB.dLPB_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLPB.dLPB_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLPB.dLPB_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_BillNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPB.sLPB_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_TotalLtr", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLPB.dLPB_TotalLtr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_TotalDieselAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLPB.dLPB_TotalDieselAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_AdvanceAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLPB.dLPB_AdvanceAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_OtherExpense", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLPB.dLPB_OtherExpense
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_TotalAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLPB.dLPB_TotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_TCS", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLPB.sLPB_TCS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPB.sLPB_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPB.sLPB_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLPB.dLPB_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLPB.dLPB_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPB.iLPB_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPB.sLPB_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPB_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLPB.sLPB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_PumpBilling", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LPB_ID,LPB_BillNo From Lgst_PumpBilling Where LPB_CompID=" & iCompID & " and LPB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select count(LPB_ID) From Lgst_PumpBilling where  LPB_YearID=" & iYearId & "")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
            If sMaximumID = Nothing Then
                sMaxID = "0001"
            Else
                sLastID = sMaximumID + 1
                If sLastID.Length = 1 Then
                    sMaxID = "000" & "" & sLastID & ""
                ElseIf sLastID.Length = 2 Then
                    sMaxID = "00" & "" & sLastID & ""
                ElseIf sLastID.Length = 3 Then
                    sMaxID = "0" & "" & sLastID & ""
                End If
            End If

            If sMonth.Length = 1 Then
                sMonthCode = "0" & "" & sMonth & ""
            Else
                sMonthCode = sMonth
            End If

            If sDate.Length = 1 Then
                sSDate = "0" & "" & sDate & ""
            Else
                sSDate = sDate
            End If
            sStr = "PB-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPumpBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iBillId As Integer, ByVal iPumpID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dTotalAmt As Double = 0.0
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("PumpName")
            dtTab.Columns.Add("TotalLtr")
            dtTab.Columns.Add("TotalDieselAmt")
            dtTab.Columns.Add("AdvncAmt")
            dtTab.Columns.Add("OtherExpense")
            dtTab.Columns.Add("TotalAmt")

            If iPumpID > 0 Then
                sSql = "" : sSql = "Select * From Lgst_PumpBilling Where LPB_ID=" & iBillId & " and LPB_CompID =" & iCompID & " And LPB_YearID =" & iYearId & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("PumpName") = GetDieselPumpName(sNameSpace, iPumpID, iCompID)
                    dRow("TotalLtr") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LPB_TotalLtr")))
                    dRow("TotalDieselAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LPB_TotalDieselAmt")))
                    dRow("AdvncAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LPB_AdvanceAmt")))
                    dRow("OtherExpense") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LPB_OtherExpense")))
                    dRow("TotalAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LPB_TotalAmt")))
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPumpBillingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_PumpBilling Where LPB_ID=" & iInvid & "  And LPB_CompID=" & iCompID & " and LPB_Yearid= " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatusOfBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer, ByVal iPumpID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select LPB_Status From Lgst_PumpBilling Where LPB_ID=" & iInvoiceID & " And LPB_PumpID=" & iPumpID & " And LPB_YearID=" & iYearID & " And LPB_CompID=" & iCompID & " "
            GetStatusOfBillNo = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatusOfBillNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateInvStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvId As Integer, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Lgst_PumpBilling set LPB_Status='A',LPB_Delflag='A' where LPB_Id=" & iInvId & " and LPB_Compid=" & iCompID & " And LPB_YearID=" & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub


    '''''Dashboard
    Public Function LoadDriverBillingDetailsDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim sVehicleType As String = ""
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Pump", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TotalAmt", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Lgst_PumpBilling where LPB_CompID =" & iCompID & " and LPB_YearID=" & iYearID & " "
            If iStatus = 0 Then
                sSql = sSql & " And LPB_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LPB_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LPB_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LPB_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LPB_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LPB_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LPB_PumpID").ToString()) = False Then
                        dr("Pump") = GetDieselPumpName(sNameSpace, ds.Tables(0).Rows(i)("LPB_PumpID").ToString(), iCompID)
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPB_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("LPB_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPB_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("LPB_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPB_TotalAmt").ToString()) = False Then
                        dr("TotalAmt") = ds.Tables(0).Rows(i)("LPB_TotalAmt").ToString()
                    End If


                    If (ds.Tables(0).Rows(i)("LPB_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LPB_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LPB_DelFlag") = "D") Then
                        dr("Status") = "De-Activated"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDriverBillingMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_PumpBilling Set LPB_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LPB_Status='A',LPB_DelFlag='A'"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LPB_Status='D',LPB_DelFlag='D'"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LPB_Status='A',LPB_DelFlag='A' "
            End If
            sSql = sSql & " Where LPB_CompID=" & iCompID & " And LPB_ID = " & iMasId & "  and LPB_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDriverBillingStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_PumpBilling Where LPB_DelFlag='" & sSelectedStatus & "' And LPB_CompID=" & iCompID & "   and LPB_YearID=" & iYearID & ""
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_PumpBilling Set LPB_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LPB_Status='A',LPB_DelFlag='A'"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LPB_Status='D',LPB_DelFlag='D'"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LPB_Status='A',LPB_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LPB_CompID=" & iCompID & " And LPB_ID = " & iMasId & "  and LPB_YearID=" & iYearID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDatePumpCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select count(*) from Lgst_PumpBilling where LPB_PumpID=" & iPumpID & "  and LPB_Compid=" & iCompID & " and LPB_YearId=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRoutePumpdate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sDate As String = ""

        Try

            sSql = "" : sSql = "Select Top 1 LPB_ToDate From Lgst_PumpBilling where LPB_PumpID=" & iPumpID & "  and LPB_Compid=" & iCompID & " and LPB_YearId=" & iYearID & " order by lpb_id desc"
            sDate = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "SELECT DATEADD(day, 1, '" & objGen.FormatDtForRDBMS(sDate, "CT") & "')"
            GetRoutePumpdate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetRoutePumpdate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
