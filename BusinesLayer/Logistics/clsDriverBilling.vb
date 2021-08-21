Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsDriverBilling
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iLDB_ID As Integer
    Private iLDB_DriverID As Integer
    Private dLDB_FromDate As DateTime
    Private dLDB_ToDate As DateTime
    Private dLDB_BillDate As DateTime
    Private sLDB_BillNo As String
    Private dLDB_TotalAmt As Double
    Private dLDB_AdvanceGvnAmt As Double
    Private dLDB_PendingAmt As Double
    Private sLDB_Delflag As String
    Private iLDB_CompID As Integer
    Private iLDB_YearID As Integer
    Private sLDB_Status As String
    Private sLDB_Operation As String
    Private sLDB_IPAddress As String
    Private iLDB_CreatedBy As Integer
    Private dLDB_CreatedOn As DateTime
    Private iLDB_ApprovedBy As Integer
    Private dLDB_ApprovedOn As DateTime
    Private iLDB_DeletedBy As Integer
    Private dLDB_DeletedOn As DateTime
    Private iLDB_UpdatedBy As Integer
    Private dLDB_UpdatedOn As DateTime
    Private iLDB_RecalldBy As Integer
    Public Property LDB_ID() As Integer
        Get
            Return (iLDB_ID)
        End Get
        Set(ByVal Value As Integer)
            iLDB_ID = Value
        End Set
    End Property

    Public Property LDB_DriverID() As Integer
        Get
            Return (iLDB_DriverID)
        End Get
        Set(ByVal Value As Integer)
            iLDB_DriverID = Value
        End Set
    End Property
    Public Property LDB_FromDate() As Date
        Get
            Return (dLDB_FromDate)
        End Get
        Set(ByVal Value As Date)
            dLDB_FromDate = Value
        End Set
    End Property
    Public Property LDB_ToDate() As Date
        Get
            Return (dLDB_ToDate)
        End Get
        Set(ByVal Value As Date)
            dLDB_ToDate = Value
        End Set
    End Property
    Public Property LDB_BillDate() As Date
        Get
            Return (dLDB_BillDate)
        End Get
        Set(ByVal Value As Date)
            dLDB_BillDate = Value
        End Set
    End Property

    Public Property LDB_TotalAmt() As Double
        Get
            Return (dLDB_TotalAmt)
        End Get
        Set(ByVal Value As Double)
            dLDB_TotalAmt = Value
        End Set
    End Property
    Public Property LDB_AdvanceGvnAmt() As Double
        Get
            Return (dLDB_AdvanceGvnAmt)
        End Get
        Set(ByVal Value As Double)
            dLDB_AdvanceGvnAmt = Value
        End Set
    End Property
    Public Property LDB_PendingAmt() As Double
        Get
            Return (dLDB_PendingAmt)
        End Get
        Set(ByVal Value As Double)
            dLDB_PendingAmt = Value
        End Set
    End Property
    Public Property LDB_BillNo() As String
        Get
            Return (sLDB_BillNo)
        End Get
        Set(ByVal Value As String)
            sLDB_BillNo = Value
        End Set
    End Property
    Public Property LDB_Delflag() As String
        Get
            Return (sLDB_Delflag)
        End Get
        Set(ByVal Value As String)
            sLDB_Delflag = Value
        End Set
    End Property
    Public Property LDB_CompID() As Integer
        Get
            Return (iLDB_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLDB_CompID = Value
        End Set
    End Property
    Public Property LDB_YearID() As Integer
        Get
            Return (iLDB_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLDB_YearID = Value
        End Set
    End Property
    Public Property LDB_Status() As String
        Get
            Return (sLDB_Status)
        End Get
        Set(ByVal Value As String)
            sLDB_Status = Value
        End Set
    End Property
    Public Property LDB_Operation() As String
        Get
            Return (sLDB_Operation)
        End Get
        Set(ByVal Value As String)
            sLDB_Operation = Value
        End Set
    End Property
    Public Property LDB_IPAddress() As String
        Get
            Return (sLDB_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLDB_IPAddress = Value
        End Set
    End Property
    Public Property LDB_CreatedBy() As Integer
        Get
            Return (iLDB_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDB_CreatedBy = Value
        End Set
    End Property
    Public Property LDB_CreatedOn() As Date
        Get
            Return (dLDB_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLDB_CreatedOn = Value
        End Set
    End Property

    Public Property LDB_ApprovedBy() As Integer
        Get
            Return (iLDB_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDB_ApprovedBy = Value
        End Set
    End Property
    Public Property LDB_ApprovedOn() As Date
        Get
            Return (dLDB_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLDB_ApprovedOn = Value
        End Set
    End Property
    Public Property LDB_DeletedBy() As Integer
        Get
            Return (iLDB_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDB_DeletedBy = Value
        End Set
    End Property
    Public Property LDB_DeletedOn() As Date
        Get
            Return (dLDB_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLDB_DeletedOn = Value
        End Set
    End Property
    Public Property LDB_UpdatedBy() As Integer
        Get
            Return (iLDB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDB_UpdatedBy = Value
        End Set
    End Property
    Public Property LDB_UpdatedOn() As Date
        Get
            Return (dLDB_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLDB_UpdatedOn = Value
        End Set
    End Property
    Public Property LDB_RecalldBy() As Integer
        Get
            Return (iLDB_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLDB_RecalldBy = Value
        End Set
    End Property
    Public Function LoadDriver(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LDM_ID,(LDM_DriverName + ' - ' + LDM_LicenseNo) as LDM_DriverName From Lgst_Driver_Master Where LDM_CompID=" & iCompID & " "
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
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select count(LDB_ID) From Lgst_DriverBilling where LDB_YearID=" & iYearId & "")
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
    Public Function LoadExistingBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LDB_ID,LDB_BillNo From Lgst_DriverBilling Where LDB_CompID=" & iCompID & " and LDB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDriverDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iDriverID As Integer, ByVal sFromDate As String, ByVal sToDate As String) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dRow As DataRow
        Dim dTotalAmt As Double = 0.0
        Try
            dtTab.Columns.Add("DriverName")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("AdvanceAmt")
            dtTab.Columns.Add("PendingAmount")
            sSql = "Select sum(LTGDD_DriverAdvancGvnByPump) as DriverAdvance "
            sSql = sSql & " from Lgst_TripGenDiesel_Details Where LTGDD_CompID=" & iCompID & "  and LTGDD_YearID=" & iYearId & " and  LTGDD_TripID in ("
            sSql = sSql & "Select LTGM_ID from Lgst_TripGeneration_Master where LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearId & " and LTGM_Driver='" & iDriverID & "'  and LTGM_TripStatus=2 "
            If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
                sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sToDate & "' ) "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                dRow = dtTab.NewRow
                dRow("DriverName") = GetDriverName(sNameSpace, iDriverID, iCompID)
                dRow("TotalAmount") = GetTotAmt(sNameSpace, iDriverID, iCompID, iYearId, sFromDate, sToDate)
                dRow("AdvanceAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(0)("DriverAdvance")))
                dRow("PendingAmount") = dRow("TotalAmount") - dRow("AdvanceAmt")
                dtTab.Rows.Add(dRow)
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDriverName(ByVal sNameSpace As String, ByVal iDriverID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sDriver As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LDM_DriverName from Lgst_Driver_Master where LDM_ID = " & iDriverID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LDM_DriverName").ToString()) = False Then
                    sDriver = dt.Rows(0)("LDM_DriverName").ToString()
                Else
                    sDriver = ""
                End If
            Else
                sDriver = ""
            End If
            Return sDriver
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotAmt(ByVal sNameSpace As String, ByVal iDriverId As Integer, ByVal iCompId As Integer, ByVal iYearId As Integer, ByVal sFromDate As String, ByVal sTodate As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select sum(LTGM_DriverAmount) from Lgst_TripGeneration_Master where LTGM_Driver = '" & iDriverId & "' and LTGM_CompID=" & iCompId & " and  LTGM_YearID=" & iYearId & " and LTGM_TripStatus=2"
            If (sFromDate <> "01/01/1900") And (sTodate <> "01-01-1900") Then
                sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sTodate & "'"
            End If
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriverBillingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_DriverBilling Where LDB_ID=" & iInvid & "  And LDB_CompID=" & iCompID & " and LDB_Yearid= " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDriverBilling(ByVal sNameSpace As String, ByVal objLDB As clsDriverBilling) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_DriverID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_DriverID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_FromDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLDB.dLDB_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_ToDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLDB.dLDB_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLDB.dLDB_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_BillNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLDB.sLDB_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_TotalAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLDB.dLDB_TotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_AdvanceGvnAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLDB.dLDB_AdvanceGvnAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_PendingAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLDB.dLDB_PendingAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLDB.sLDB_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLDB.sLDB_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLDB.dLDB_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objLDB.dLDB_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLDB.iLDB_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLDB.sLDB_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDB_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLDB.sLDB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_DriverBilling", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDriverBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iBillId As Integer, ByVal iDriverID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dTotalAmt As Double = 0.0
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("DriverName")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("AdvanceAmt")
            dtTab.Columns.Add("PendingAmount")
            If iDriverID > 0 Then
                sSql = "" : sSql = "Select * From Lgst_DriverBilling Where LDB_ID=" & iBillId & " and LDB_CompID =" & iCompID & " And LDB_YearID =" & iYearId & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("DriverName") = GetDriverName(sNameSpace, iDriverID, iCompID)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LDB_TotalAmt")))
                    dRow("AdvanceAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LDB_AdvanceGvnAmt")))
                    dRow("PendingAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LDB_PendingAmt")))
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatusOfBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer, ByVal iDriverID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select LDB_Status From Lgst_DriverBilling Where LDB_ID=" & iInvoiceID & " And LDB_DriverID=" & iDriverID & " And LDB_YearID=" & iYearID & " And LDB_CompID=" & iCompID & " "
            GetStatusOfBillNo = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatusOfBillNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateInvStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvId As Integer, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Lgst_DriverBilling set LDB_Status='A',LDB_Delflag='A' where LDB_Id=" & iInvId & " and LDB_Compid=" & iCompID & " And LDB_YearID=" & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub


    ''''''Dashboard
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
            dc = New DataColumn("Driver", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PendingAmt", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Lgst_DriverBilling where LDB_CompID =" & iCompID & "  and  LDB_YearID= " & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And LDB_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LDB_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LDB_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LDB_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LDB_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LDB_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LDB_DriverID").ToString()) = False Then
                        dr("Driver") = GetDriverName(sNameSpace, ds.Tables(0).Rows(i)("LDB_DriverID").ToString(), iCompID)
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LDB_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("LDB_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LDB_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("LDB_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LDB_PendingAmt").ToString()) = False Then
                        dr("PendingAmt") = ds.Tables(0).Rows(i)("LDB_PendingAmt").ToString()
                    End If


                    If (ds.Tables(0).Rows(i)("LDB_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LDB_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LDB_DelFlag") = "D") Then
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
            sSql = "Update Lgst_DriverBilling Set LDB_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LDB_Status='A',LDB_DelFlag='A'"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LDB_Status='D',LDB_DelFlag='D'"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LDB_Status='A',LDB_DelFlag='A' "
            End If
            sSql = sSql & " Where LDB_CompID=" & iCompID & " And LDB_ID = " & iMasId & "  and  LDB_YearID= " & iYearID & ""
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
            sSql = "" : sSql = "Select * From Lgst_DriverBilling Where LDB_DelFlag='" & sSelectedStatus & "' And LDB_CompID=" & iCompID & "  and  LDB_YearID= " & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_DriverBilling Set LDB_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LDB_Status='A',LDB_DelFlag='A'"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LDB_Status='D',LDB_DelFlag='D'"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LDB_Status='A',LDB_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LDB_CompID=" & iCompID & " And LDB_ID = " & iMasId & "  and  LDB_YearID= " & iYearID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDateDriverCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDriverID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select count(*) from Lgst_DriverBilling where LDB_DriverID=" & iDriverID & "  and LDB_Compid=" & iCompID & " and LDB_YearId=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRouteDriverdate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sDate As String = ""

        Try

            sSql = "" : sSql = "Select Top 1 LDB_ToDate From Lgst_DriverBilling where LDB_DriverID=" & iPumpID & "  and LDB_Compid=" & iCompID & " and LDB_YearId=" & iYearID & " order by ldb_id desc"
            sDate = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "SELECT DATEADD(day, 1, '" & objGen.FormatDtForRDBMS(sDate, "CT") & "')"
            GetRouteDriverdate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetRouteDriverdate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
