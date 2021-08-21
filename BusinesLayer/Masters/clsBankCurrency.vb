Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Imports System.IO
Public Structure strBankCurrencyMaster
    Private BCM_PKID As Integer
    Private BCM_Currency As Integer
    Private BCM_OperateOn As Integer
    Private BCM_Date As String
    Private BCM_Time As String
    Private BCM_CreatedBy As Integer
    Private BCM_CreatedOn As DateTime
    Private BCM_IPAddress As String
    Private BCM_TTBuy As Double
    Private BCM_TTSell As Double
    Private BCM_Buy As Double
    Private BCM_Sell As Double
    Private BCM_BankID As Integer
    Private BCM_CompID As Integer
    Public Property iBCM_PKID() As Integer
        Get
            Return (BCM_PKID)
        End Get
        Set(ByVal Value As Integer)
            BCM_PKID = Value
        End Set
    End Property
    Public Property sBCM_Currency() As Integer
        Get
            Return (BCM_Currency)
        End Get
        Set(ByVal Value As Integer)
            BCM_Currency = Value
        End Set
    End Property
    Public Property sBCM_OperateOn() As Integer
        Get
            Return (BCM_OperateOn)
        End Get
        Set(ByVal Value As Integer)
            BCM_OperateOn = Value
        End Set
    End Property
    Public Property sBCM_Date() As String
        Get
            Return (BCM_Date)
        End Get
        Set(ByVal Value As String)
            BCM_Date = Value
        End Set
    End Property
    Public Property sBCM_Time() As String
        Get
            Return (BCM_Time)
        End Get
        Set(ByVal Value As String)
            BCM_Time = Value
        End Set
    End Property
    Public Property sBCM_CreatedBy() As Integer
        Get
            Return (BCM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            BCM_CreatedBy = Value
        End Set
    End Property
    Public Property sBCM_CreatedOn() As DateTime
        Get
            Return (BCM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            BCM_CreatedOn = Value
        End Set
    End Property
    Public Property sBCM_IPAddress() As String
        Get
            Return (BCM_IPAddress)
        End Get
        Set(ByVal Value As String)
            BCM_IPAddress = Value
        End Set
    End Property
    Public Property dBCM_TTBUY() As Double
        Get
            Return (BCM_TTBuy)
        End Get
        Set(ByVal Value As Double)
            BCM_TTBuy = Value
        End Set
    End Property
    Public Property dBCM_TTSell() As Double
        Get
            Return (BCM_TTSell)
        End Get
        Set(ByVal Value As Double)
            BCM_TTSell = Value
        End Set
    End Property
    Public Property dBCM_BUY() As Double
        Get
            Return (BCM_Buy)
        End Get
        Set(ByVal Value As Double)
            BCM_Buy = Value
        End Set
    End Property
    Public Property dBCM_Sell() As Double
        Get
            Return (BCM_Sell)
        End Get
        Set(ByVal Value As Double)
            BCM_Sell = Value
        End Set
    End Property
    Public Property iBCM_BankID() As Integer
        Get
            Return (BCM_BankID)
        End Get
        Set(ByVal Value As Integer)
            BCM_BankID = Value
        End Set
    End Property
    Public Property iBCM_CompID() As Integer
        Get
            Return (BCM_CompID)
        End Get
        Set(ByVal Value As Integer)
            BCM_CompID = Value
        End Set
    End Property
End Structure
Public Class clsBankCurrency
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared objclsFASGeneral As New clsFASGeneral
    Public Function LoadBankCurrencyDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iBank As Integer, ByVal iOperateOn As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim sToday As String = "", sFrom As String = "", sTo As String = "", sOpSymbol As String = ""
        Dim sTTBuy As String = "", sTTSell As String = "", sBuy As String = "", sSell As String = ""
        Try
            dtDisplay.Columns.Add("SrNo")
            dtDisplay.Columns.Add("ID")
            dtDisplay.Columns.Add("CurID")
            dtDisplay.Columns.Add("CurrID")
            dtDisplay.Columns.Add("Currency")
            dtDisplay.Columns.Add("OperateOn")
            dtDisplay.Columns.Add("TTBuy")
            dtDisplay.Columns.Add("TTSell")
            dtDisplay.Columns.Add("TBuy")
            dtDisplay.Columns.Add("TSell")
            dtDisplay.Columns.Add("BankName")
            dtDisplay.Columns.Add("Date")
            dtDisplay.Columns.Add("Time")
            dtDisplay.Columns.Add("CreatedBy")
            dtDisplay.Columns.Add("Status")

            sToday = objGen.GetCurrentDate(sNameSpace)
            If dFrom <> "" Then
                sFrom = FormatDt(dFrom, "D")
                sTo = FormatDt(dTo, "D")
            End If
            sSql = "" : sSql = " Select (A.CUR_CODE + ' [' + A.CUR_CountryName + ']') as OperateOn,B.BCM_TTBuy,B.BCM_TTSell,B.BCM_Buy,B.BCM_Sell,B.BCM_Date,B.BCM_Time,"
            sSql = sSql & " B.BCM_PKID,B.BCM_Currency,B.BCM_OperateOn,C.Usr_FullName,( D.CUR_CODE + ' [' + D.CUR_CountryName + ']') as Currency,D.CUR_CODE as CCode,"
            sSql = sSql & " B.BCM_DelFlag,(gl_glCode + ' - ' + gl_desc) as BankName From Currency_master A Join MST_BankCurrency_Masters B On A.CUR_ID = B.BCM_OperateOn"
            sSql = sSql & " And B.BCM_CreatedBy =" & iUserID & " And B.BCM_CompID=" & iCompID & " Join "
            sSql = sSql & " sad_UserDetails C On B.BCM_CreatedBy = C.Usr_ID Join Currency_master D On D.CUR_ID = B.BCM_Currency"
            sSql = sSql & " join Chart_Of_Accounts On gl_id=B.BCM_BankID And gl_CompId=" & iCompID & ""
            If dFrom = "" Then
                sSql = sSql & " Where B.BCM_Date='" & sToday & "'"
            Else
                sSql = sSql & " Where BCM_CreatedON BETWEEN '" & sFrom & "' AND DATEADD(s,-1,DATEADD(d,1,'" & sTo & "'))"
            End If
            If iBank > 0 Then
                sSql = sSql & " And B.BCM_BankID=" & iBank & ""
            End If
            If iOperateOn > 0 Then
                sSql = sSql & " And B.BCM_OperateOn=" & iOperateOn & ""
            End If
            sSql = sSql & " Order by Currency"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("BCM_PKID")
                    dRow("CurID") = dt.Rows(i)("BCM_Currency")
                    dRow("CurrID") = dt.Rows(i)("BCM_OperateOn")
                    dRow("Currency") = dt.Rows(i)("Currency")
                    dRow("OperateOn") = dt.Rows(i)("OperateOn")
                    sOpSymbol = Trail(sNameSpace, dRow("OperateOn"))
                    sTTBuy = Convert.ToDecimal(dt.Rows(i)("BCM_TTBuy")).ToString("#,##0.00")
                    dRow("TTBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTBuy
                    sTTSell = Convert.ToDecimal(dt.Rows(i)("BCM_TTSell")).ToString("#,##0.00")
                    dRow("TTSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTSell
                    sBuy = Convert.ToDecimal(dt.Rows(i)("BCM_Buy")).ToString("#,##0.00")
                    dRow("TBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sBuy
                    sSell = Convert.ToDecimal(dt.Rows(i)("BCM_Sell")).ToString("#,##0.00")
                    dRow("TSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sSell
                    If IsDBNull(dt.Rows(i)("BankName")) = False Then
                        dRow("BankName") = dt.Rows(i)("BankName")
                    End If
                    dRow("Date") = dt.Rows(i)("BCM_Date")
                    dRow("Time") = dt.Rows(i)("BCM_Time")
                    dRow("CreatedBy") = dt.Rows(i)("Usr_FullName")
                    If IsDBNull(dt.Rows(i)("BCM_DelFlag")) = False Then
                        If dt.Rows(i)("BCM_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("BCM_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("BCM_DelFlag") = "W" Then
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
    Public Function SaveBankCurrencyMaster(ByVal sAC As String, ByVal objBankCurrency As strBankCurrencyMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objBankCurrency.iBCM_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_Currency", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_OperateOn", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_OperateOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_Date", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_Time", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_Time
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objBankCurrency.sBCM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_TTBuy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objBankCurrency.dBCM_TTBUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_TTSell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objBankCurrency.dBCM_TTSell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_Buy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objBankCurrency.dBCM_BUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_Sell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objBankCurrency.dBCM_Sell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_BankID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objBankCurrency.iBCM_BankID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BCM_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objBankCurrency.iBCM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_BankCurrency_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sNameSpace As String, ByVal sTable As String, ByVal sColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ") + 1,1) FROM " & sTable & ""
            objMax = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentTime(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate(),108)"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSymbolPath(ByVal sAC As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from sad_config_settings where sad_Config_Key='TempPath'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("sad_Config_Value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Trail(ByVal sAC As String, ByVal sCountry As String)
        Dim sOpCountry As String = "", Symbol As String = "", Spath As String = ""
        Try
            Spath = GetSymbolPath(sAC) & "Trail.txt"
            sOpCountry = sCountry.Remove(3)
            Dim s2 As String = ""
            Using reader As New StreamReader(Spath)
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    If line.Contains(sOpCountry) Then
                        s2 = line
                        Exit While
                    End If
                End While
            End Using
            Symbol = s2.Remove(0, 3)
            Return Symbol.ToString()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function FormatDt(ByVal dtDate As DateTime, ByVal sPurpose As String) As String
        Dim sTempDate As String = ""
        Try
            Select Case UCase(Trim(sPurpose))
                Case "D"
                    sTempDate = Format(dtDate, "yyyy-MM-dd")
            End Select
            FormatDt = sTempDate
        Catch exp As System.Exception
            Throw
        End Try
    End Function
    Public Function LoadBankCurrencyPKID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer, ByVal iOperateOn As Integer)
        Dim sSql As String = ""
        Dim sToday As String = "", sFrom As String = "", sTo As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = "" : sSql = "Select BCM_PKID From MST_BankCurrency_Masters Where BCM_Date='" & sToday & "' And BCM_OperateOn=" & iOperateOn & " And BCM_CompID=" & iCompID & ""
            If iBankID > 0 Then
                sSql = sSql & " And BCM_BankID=" & iBankID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDataIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal sDate As String, ByVal iBankID As Integer, ByVal iCurrID As Integer, ByVal iOperateOn As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'Bank Currency
            sSql = "Select CM_PKID from MST_Currency_Masters Where CM_Date='" & sDate & "' And CM_BankID=" & iBankID & " And CM_Currency=" & iCurrID & ""
            sSql = sSql & " And CM_OperateOn=" & iOperateOn & " And CM_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub BankCurrencyApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update MST_BankCurrency_Masters set"
            If sType = "Created" Then
                sSql = sSql & " BCM_DelFlag='A',BCM_AppBy=" & iSessionUsrID & ", BCM_AppOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " BCM_DelFlag='D',BCM_DisableBy=" & iSessionUsrID & ", BCM_DisableOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " BCM_DelFlag='A',BCM_EnableBy=" & iSessionUsrID & ", BCM_EnableOn=Getdate(),"
            End If
            sSql = sSql & "BCM_IPAddress='" & sIPAddress & "' Where BCM_CompID=" & iACID & " And BCM_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadBankCurrencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer, ByVal iOperateOn As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = " Select * from MST_BankCurrency_Masters Where BCM_Date='" & sToday & "' And BCM_OperateOn=" & iOperateOn & " And BCM_CompID=" & iCompID & ""
            If iBankID > 0 Then
                sSql = sSql & " And BCM_BankID=" & iBankID & ""
            End If
            If iID > 0 Then
                sSql = sSql & " And BCM_PKID=" & iID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBankName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select (gl_glCode + ' - ' + gl_desc) as BankName FROM Chart_Of_Accounts Where gl_id=" & iBankID & " And gl_CompId=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
