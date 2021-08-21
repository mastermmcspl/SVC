Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Imports System.IO
Public Structure strCurrencyMaster
    Private CM_PKID As Integer
    Private CM_Currency As Integer
    Private CM_OperateOn As Integer
    Private CM_Date As String
    Private CM_Time As String
    Private CM_CreatedBy As Integer
    Private CM_CreatedOn As DateTime
    Private CM_IPAddress As String
    Private CM_TTBuy As Double
    Private CM_TTSell As Double
    Private CM_Buy As Double
    Private CM_Sell As Double
    Private CM_Type As Integer
    Private CM_CompID As Integer
    Private CM_BankID As Integer
    Public Property iCM_PKID() As Integer
        Get
            Return (CM_PKID)
        End Get
        Set(ByVal Value As Integer)
            CM_PKID = Value
        End Set
    End Property
    Public Property sCM_Currency() As Integer
        Get
            Return (CM_Currency)
        End Get
        Set(ByVal Value As Integer)
            CM_Currency = Value
        End Set
    End Property
    Public Property sCM_OperateOn() As Integer
        Get
            Return (CM_OperateOn)
        End Get
        Set(ByVal Value As Integer)
            CM_OperateOn = Value
        End Set
    End Property
    Public Property sCM_Date() As String
        Get
            Return (CM_Date)
        End Get
        Set(ByVal Value As String)
            CM_Date = Value
        End Set
    End Property
    Public Property sCM_Time() As String
        Get
            Return (CM_Time)
        End Get
        Set(ByVal Value As String)
            CM_Time = Value
        End Set
    End Property
    Public Property sCM_CreatedBy() As Integer
        Get
            Return (CM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            CM_CreatedBy = Value
        End Set
    End Property
    Public Property sCM_CreatedOn() As DateTime
        Get
            Return (CM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            CM_CreatedOn = Value
        End Set
    End Property
    Public Property sCM_IPAddress() As String
        Get
            Return (CM_IPAddress)
        End Get
        Set(ByVal Value As String)
            CM_IPAddress = Value
        End Set
    End Property
    Public Property dCM_TTBUY() As Double
        Get
            Return (CM_TTBuy)
        End Get
        Set(ByVal Value As Double)
            CM_TTBuy = Value
        End Set
    End Property
    Public Property dCM_TTSell() As Double
        Get
            Return (CM_TTSell)
        End Get
        Set(ByVal Value As Double)
            CM_TTSell = Value
        End Set
    End Property
    Public Property dCM_BUY() As Double
        Get
            Return (CM_Buy)
        End Get
        Set(ByVal Value As Double)
            CM_Buy = Value
        End Set
    End Property
    Public Property dCM_Sell() As Double
        Get
            Return (CM_Sell)
        End Get
        Set(ByVal Value As Double)
            CM_Sell = Value
        End Set
    End Property
    Public Property iCM_Type() As Integer
        Get
            Return (CM_Type)
        End Get
        Set(ByVal Value As Integer)
            CM_Type = Value
        End Set
    End Property
    Public Property iCM_CompID() As Integer
        Get
            Return (CM_CompID)
        End Get
        Set(ByVal Value As Integer)
            CM_CompID = Value
        End Set
    End Property
    Public Property iCM_BankID() As Integer
        Get
            Return (CM_BankID)
        End Get
        Set(ByVal Value As Integer)
            CM_BankID = Value
        End Set
    End Property
End Structure
Public Class clsCurrencyMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared objclsFASGeneral As New clsFASGeneral
    Public Function LoadCurrencyDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCurrency As Integer, ByVal iOperateOn As Integer, ByVal dFrom As String, ByVal dTo As String)
        Dim sSql As String = "", sOpSymbol As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim sToday As String = "", sFrom As String = "", sTo As String = ""
        Dim sTTBuy As String = "", sTTSell As String = "", sBuy As String = "", sSell As String = ""
        Try
            dtDisplay.Columns.Add("SrNo")
            dtDisplay.Columns.Add("ID")
            dtDisplay.Columns.Add("CurrencyID")
            dtDisplay.Columns.Add("OperatedOnID")
            dtDisplay.Columns.Add("Currency")
            dtDisplay.Columns.Add("OperateOn")
            dtDisplay.Columns.Add("TTBuy")
            dtDisplay.Columns.Add("TTSell")
            dtDisplay.Columns.Add("TBuy")
            dtDisplay.Columns.Add("TSell")
            dtDisplay.Columns.Add("BankID")
            dtDisplay.Columns.Add("BankName")
            dtDisplay.Columns.Add("Type")
            dtDisplay.Columns.Add("Date")
            dtDisplay.Columns.Add("Time")
            dtDisplay.Columns.Add("CreatedBy")
            dtDisplay.Columns.Add("Status")

            sToday = objGen.GetCurrentDate(sNameSpace)
            If dFrom <> "" Then
                sFrom = FormatDt(dFrom, "D")
                sTo = FormatDt(dTo, "D")
            End If
            sSql = "" : sSql = " Select (A.CUR_CODE + ' [' + A.CUR_CountryName + ']') as OperateOn,B.CM_TTBuy,B.CM_TTSell,B.CM_Buy,B.CM_Sell,B.CM_Date,B.CM_Time,"
            sSql = sSql & " B.CM_PKID,B.CM_Currency,B.CM_OperateOn,C.Usr_FullName,( D.CUR_CODE + ' [' + D.CUR_CountryName + ']') as Currency, B.CM_Type,D.CUR_CODE as CCode,"
            sSql = sSql & " B.CM_DelFlag,(gl_glCode + ' - ' + gl_desc) as BankName,CM_BankID,FE_AgencyName From Currency_master A"
            sSql = sSql & " Join MST_Currency_Masters B On A.CUR_ID = B.CM_OperateOn And B.CM_CreatedBy =" & iUserID & " And B.CM_CompID=" & iCompID & " Join "
            sSql = sSql & " sad_UserDetails C On B.CM_CreatedBy= C.Usr_ID Join Currency_master D On D.CUR_ID= B.CM_Currency"
            sSql = sSql & " Left join Chart_Of_Accounts On gl_id=B.CM_BankID And gl_CompId=" & iCompID & ""
            sSql = sSql & " Left Join MST_ForeignExchange_Agents On FE_ID=B.CM_BankID And FE_CompID=" & iCompID & ""
            If dFrom = "" Then
                sSql = sSql & " Where B.CM_Date='" & sToday & "'"
            Else
                sSql = sSql & " WHERE cm_createdon BETWEEN '" & sFrom & "' AND DATEADD(s,-1,DATEADD(d,1,'" & sTo & "'))"
            End If
            If iCurrency > 0 Then
                sSql = sSql & " And B.CM_Currency=" & iCurrency & ""
            End If
            If iOperateOn > 0 Then
                sSql = sSql & " And B.CM_OperateOn=" & iOperateOn & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("CM_PKID")
                    dRow("CurrencyID") = dt.Rows(i)("CM_Currency")
                    dRow("OperatedOnID") = dt.Rows(i)("CM_OperateOn")
                    dRow("Currency") = dt.Rows(i)("Currency")
                    dRow("OperateOn") = dt.Rows(i)("OperateOn")
                    sOpSymbol = Trail(sNameSpace, dRow("OperateOn"))
                    sTTBuy = Convert.ToDecimal(dt.Rows(i)("CM_TTBuy")).ToString("#,##0.00")
                    dRow("TTBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTBuy
                    sTTSell = Convert.ToDecimal(dt.Rows(i)("CM_TTSell")).ToString("#,##0.00")
                    dRow("TTSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTSell
                    sBuy = Convert.ToDecimal(dt.Rows(i)("CM_Buy")).ToString("#,##0.00")
                    dRow("TBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sBuy
                    sSell = Convert.ToDecimal(dt.Rows(i)("CM_Sell")).ToString("#,##0.00")
                    dRow("TSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sSell
                    If IsDBNull(dt.Rows(i)("CM_BankID")) = False Then
                        dRow("BankID") = dt.Rows(i)("CM_BankID")
                    End If

                    If IsDBNull(dt.Rows(i)("CM_Type")) = False Then
                        If dt.Rows(i)("CM_Type") = "1" Then
                            dRow("Type") = "Web"
                            dRow("BankName") = ""
                        ElseIf dt.Rows(i)("CM_Type") = "2" Then
                            dRow("Type") = "Agency"
                            If IsDBNull(dt.Rows(i)("FE_AgencyName")) = False Then
                                dRow("BankName") = dt.Rows(i)("FE_AgencyName")
                            End If
                        Else
                            dRow("Type") = "Bank"
                            If IsDBNull(dt.Rows(i)("BankName")) = False Then
                                dRow("BankName") = dt.Rows(i)("BankName")
                            End If
                        End If
                    End If
                    dRow("Date") = dt.Rows(i)("CM_Date")
                    dRow("Time") = dt.Rows(i)("CM_Time")
                    dRow("CreatedBy") = dt.Rows(i)("Usr_FullName")
                    If IsDBNull(dt.Rows(i)("CM_DelFlag")) = False Then
                        If dt.Rows(i)("CM_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("CM_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("CM_DelFlag") = "W" Then
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
    Public Function SaveCurrencyMaster(ByVal sAC As String, ByVal objCurrency As strCurrencyMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCurrency.iCM_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Currency", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCurrency.sCM_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_OperateOn", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCurrency.sCM_OperateOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Date", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCurrency.sCM_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Time", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCurrency.sCM_Time
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCurrency.sCM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objCurrency.sCM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_TTBuy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objCurrency.dCM_TTBUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_TTSell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objCurrency.dCM_TTSell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Buy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objCurrency.dCM_BUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Sell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objCurrency.dCM_Sell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Type", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objCurrency.iCM_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objCurrency.iCM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_BankID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objCurrency.iCM_BankID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_Currency_Masters", 1, Arr, ObjParam)
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
    Public Function LoadCurrencyPKID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer, ByVal iCurrID As Integer, ByVal iOperateOn As Integer)
        Dim sSql As String = ""
        Dim sToday As String = "", sFrom As String = "", sTo As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = "" : sSql = "Select CM_PKID From MST_Currency_Masters Where CM_Date='" & sToday & "' And CM_OperateOn=" & iOperateOn & " And CM_CompID=" & iCompID & ""
            If iBankID > 0 Then
                sSql = sSql & " And CM_BankID=" & iBankID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub CurrencyApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update MST_Currency_Masters set"
            If sType = "Created" Then
                sSql = sSql & " CM_DelFlag='A',CM_AppBy=" & iSessionUsrID & ", CM_AppOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CM_DelFlag='D',CM_DisableBy=" & iSessionUsrID & ", CM_DisableOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CM_DelFlag='A',CM_EnableBy=" & iSessionUsrID & ", CM_EnableOn=Getdate(),"
            End If
            sSql = sSql & "CM_IPAddress='" & sIPAddress & "' Where CM_CompID=" & iACID & " And CM_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadBankCurrencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer, ByVal iCurrID As Integer, ByVal iOperateOn As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = " Select * from MST_BankCurrency_Masters Where BCM_Date='" & sToday & "' And BCM_Currency=" & iCurrID & ""
            sSql = sSql & " And BCM_OperateOn=" & iOperateOn & " And BCM_CompID=" & iCompID & ""
            If iBankID > 0 Then
                sSql = sSql & " And BCM_BankID=" & iBankID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCurrencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrID As Integer, ByVal iOperateOn As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = " Select * from MST_Currency_Masters Where CM_Date='" & sToday & "' And CM_Currency=" & iCurrID & ""
            sSql = sSql & " And CM_OperateOn=" & iOperateOn & " And CM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBankWeekOff(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Try
            sToday = objclsFASGeneral.FormatDtForRDBMS(objGen.GetCurrentDate(sNameSpace), "CT")
            sSql = " Select Hol_YearId from Holiday_Master Where Hol_Date='" & sToday & "' And Hol_YearId=" & iYearID & " And Hol_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAgencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrID As Integer, ByVal iOperateOn As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Dim dtAgency As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtAgency.Columns.Add("SrNo")
            dtAgency.Columns.Add("ID")
            dtAgency.Columns.Add("FEID")
            dtAgency.Columns.Add("AgencyName")
            dtAgency.Columns.Add("TTBuy")
            dtAgency.Columns.Add("TTSell")
            dtAgency.Columns.Add("TBuy")
            dtAgency.Columns.Add("TSell")
            dtAgency.Columns.Add("BankName")
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = "Select ACM_PKID,ACM_Agency,ACM_Date,ACM_TTBuy,ACM_TTSell,ACM_Buy,ACM_Sell,FE_AgencyName,FE_Bank from MST_AgentsCurrency_Masters"
            sSql = sSql & " Left Join MST_ForeignExchange_Agents On FE_ID=ACM_Agency And FE_CompID=" & iCompID & ""
            sSql = sSql & " Where ACM_Date='" & sToday & "' And ACM_Currency=" & iCurrID & ""
            sSql = sSql & " And ACM_OperateOn=" & iOperateOn & " And ACM_CompID=" & iCompID & " And ACM_DelFlag='A' Order by ACM_TTBuy"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtAgency.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("ACM_PKID")
                    dRow("FEID") = dt.Rows(i)("ACM_Agency")
                    dRow("AgencyName") = dt.Rows(i)("FE_AgencyName")
                    dRow("TTBuy") = dt.Rows(i)("ACM_TTBuy")
                    dRow("TTSell") = dt.Rows(i)("ACM_TTSell")
                    dRow("TBuy") = dt.Rows(i)("ACM_Buy")
                    dRow("TSell") = dt.Rows(i)("ACM_Sell")
                    If IsDBNull(dt.Rows(i)("FE_Bank")) = False Then
                        dRow("BankName") = dt.Rows(i)("FE_Bank")
                    End If
                    dtAgency.Rows.Add(dRow)
                Next
            End If
            Return dtAgency
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
