Imports System.IO
Public Structure strAgencyCurrencyMaster
    Private ACM_PKID As Integer
    Private ACM_Agency As Integer
    Private ACM_Currency As Integer
    Private ACM_OperateOn As Integer
    Private ACM_Date As String
    Private ACM_Time As String
    Private ACM_CRBY As Integer
    Private ACM_UpdatedBy As Integer
    Private ACM_IPAddress As String
    Private ACM_TTBuy As Double
    Private ACM_TTSell As Double
    Private ACM_Buy As Double
    Private ACM_Sell As Double
    Private ACM_BankID As Integer
    Private ACM_CompID As Integer
    Public Property iACM_PKID() As Integer
        Get
            Return (ACM_PKID)
        End Get
        Set(ByVal Value As Integer)
            ACM_PKID = Value
        End Set
    End Property
    Public Property iACM_Agency() As Integer
        Get
            Return (ACM_Agency)
        End Get
        Set(ByVal Value As Integer)
            ACM_Agency = Value
        End Set
    End Property
    Public Property iACM_Currency() As Integer
        Get
            Return (ACM_Currency)
        End Get
        Set(ByVal Value As Integer)
            ACM_Currency = Value
        End Set
    End Property
    Public Property iACM_OperateOn() As Integer
        Get
            Return (ACM_OperateOn)
        End Get
        Set(ByVal Value As Integer)
            ACM_OperateOn = Value
        End Set
    End Property
    Public Property sACM_Date() As String
        Get
            Return (ACM_Date)
        End Get
        Set(ByVal Value As String)
            ACM_Date = Value
        End Set
    End Property
    Public Property sACM_Time() As String
        Get
            Return (ACM_Time)
        End Get
        Set(ByVal Value As String)
            ACM_Time = Value
        End Set
    End Property
    Public Property iACM_CRBY() As Integer
        Get
            Return (ACM_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ACM_CRBY = Value
        End Set
    End Property
    Public Property iACM_UpdatedBy() As Integer
        Get
            Return (ACM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ACM_UpdatedBy = Value
        End Set
    End Property
    Public Property sACM_IPAddress() As String
        Get
            Return (ACM_IPAddress)
        End Get
        Set(ByVal Value As String)
            ACM_IPAddress = Value
        End Set
    End Property
    Public Property dACM_TTBUY() As Double
        Get
            Return (ACM_TTBuy)
        End Get
        Set(ByVal Value As Double)
            ACM_TTBuy = Value
        End Set
    End Property
    Public Property dACM_TTSell() As Double
        Get
            Return (ACM_TTSell)
        End Get
        Set(ByVal Value As Double)
            ACM_TTSell = Value
        End Set
    End Property
    Public Property dACM_BUY() As Double
        Get
            Return (ACM_Buy)
        End Get
        Set(ByVal Value As Double)
            ACM_Buy = Value
        End Set
    End Property
    Public Property dACM_Sell() As Double
        Get
            Return (ACM_Sell)
        End Get
        Set(ByVal Value As Double)
            ACM_Sell = Value
        End Set
    End Property
    Public Property iACM_BankID() As Integer
        Get
            Return (ACM_BankID)
        End Get
        Set(ByVal Value As Integer)
            ACM_BankID = Value
        End Set
    End Property
    Public Property iACM_CompID() As Integer
        Get
            Return (ACM_CompID)
        End Get
        Set(ByVal Value As Integer)
            ACM_CompID = Value
        End Set
    End Property
End Structure
Public Class clsAgencyCurrency
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared objclsFASGeneral As New clsFASGeneral
    Public Function LoadAgentsCurrencyDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iAgency As Integer, ByVal iOperateOn As Integer, ByVal iCurrency As Integer, ByVal dFrom As String, ByVal dTo As String, sToday As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim sFrom As String = "", sTo As String = "", sOpSymbol As String = ""
        Dim sTTBuy As String = "", sTTSell As String = "", sBuy As String = "", sSell As String = ""
        Try
            dtDisplay.Columns.Add("SrNo")
            dtDisplay.Columns.Add("ID")
            dtDisplay.Columns.Add("CurID")
            dtDisplay.Columns.Add("CurrID")
            dtDisplay.Columns.Add("AgencyName")
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
            If dFrom <> "" Then
                sFrom = FormatDt(dFrom, "D")
                sTo = FormatDt(dTo, "D")
            End If
            sSql = "" : sSql = "Select FE_AgencyName,B.ACM_Agency,(A.CUR_CODE + ' [' + A.CUR_CountryName + ']') as OperateOn,B.ACM_TTBuy,B.ACM_TTSell,B.ACM_Buy,B.ACM_Sell,B.ACM_Date,B.ACM_Time,"
            sSql = sSql & " B.ACM_PKID,B.ACM_Currency,B.ACM_OperateOn,C.Usr_FullName,( D.CUR_CODE + ' [' + D.CUR_CountryName + ']') as Currency,D.CUR_CODE as CCode,"
            sSql = sSql & " B.ACM_DelFlag,FE_Bank as BankName From Currency_master A Join MST_AgentsCurrency_Masters B On A.CUR_ID = B.ACM_OperateOn"
            sSql = sSql & " And B.ACM_CRBY =" & iUserID & " And B.ACM_CompID=" & iCompID & " Join "
            sSql = sSql & " sad_UserDetails C On B.ACM_CRBY = C.Usr_ID Join Currency_master D On D.CUR_ID = B.ACM_Currency"
            sSql = sSql & " join MST_ForeignExchange_Agents On FE_ID=B.ACM_Agency And FE_CompID=" & iCompID & ""
            If dFrom <> "" Then
                sSql = sSql & " Where ACM_CRON BETWEEN '" & sFrom & "' AND DATEADD(s,-1,DATEADD(d,1,'" & sTo & "'))"
            Else
                sSql = sSql & " Where B.ACM_Date='" & sToday & "'"
            End If
            If iAgency > 0 Then
                sSql = sSql & " And B.ACM_Agency=" & iAgency & ""
            End If
            If iOperateOn > 0 Then
                sSql = sSql & " And B.ACM_OperateOn=" & iOperateOn & ""
            End If
            If iCurrency > 0 Then
                sSql = sSql & " And B.ACM_Currency=" & iCurrency & ""
            End If
            sSql = sSql & " Order by FE_AgencyName"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i + 1
                    dRow("ID") = dt.Rows(i)("ACM_PKID")
                    dRow("CurID") = dt.Rows(i)("ACM_Currency")
                    dRow("CurrID") = dt.Rows(i)("ACM_OperateOn")
                    dRow("AgencyName") = dt.Rows(i)("FE_AgencyName")
                    dRow("Currency") = dt.Rows(i)("Currency")
                    dRow("OperateOn") = dt.Rows(i)("OperateOn")
                    sOpSymbol = Trail(sNameSpace, dRow("OperateOn"))
                    sTTBuy = Convert.ToDecimal(dt.Rows(i)("ACM_TTBuy")).ToString("#,##0.00")
                    dRow("TTBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTBuy
                    sTTSell = Convert.ToDecimal(dt.Rows(i)("ACM_TTSell")).ToString("#,##0.00")
                    dRow("TTSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sTTSell
                    sBuy = Convert.ToDecimal(dt.Rows(i)("ACM_Buy")).ToString("#,##0.00")
                    dRow("TBuy") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sBuy
                    sSell = Convert.ToDecimal(dt.Rows(i)("ACM_Sell")).ToString("#,##0.00")
                    dRow("TSell") = sOpSymbol & "1" & " = " & dt.Rows(i)("CCode") & " " & sSell
                    If IsDBNull(dt.Rows(i)("BankName")) = False Then
                        dRow("BankName") = dt.Rows(i)("BankName")
                    End If
                    dRow("Date") = dt.Rows(i)("ACM_Date")
                    dRow("Time") = dt.Rows(i)("ACM_Time")
                    dRow("CreatedBy") = dt.Rows(i)("Usr_FullName")
                    If IsDBNull(dt.Rows(i)("ACM_DelFlag")) = False Then
                        If dt.Rows(i)("ACM_DelFlag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dt.Rows(i)("ACM_DelFlag") = "D" Then
                            dRow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("ACM_DelFlag") = "W" Then
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
    Public Function SaveAgentsCurrencyMaster(ByVal sAC As String, ByVal objAgencyCurrencyMaster As strAgencyCurrencyMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Agency", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_Agency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Currency", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_OperateOn", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_OperateOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Date", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.sACM_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Time", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.sACM_Time
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_TTBuy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.dACM_TTBUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_TTSell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.dACM_TTSell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Buy", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.dACM_BUY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Sell", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.dACM_Sell
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.sACM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objAgencyCurrencyMaster.iACM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spMST_AgentsCurrency_Masters", 1, Arr, ObjParam)
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
    Public Function LoadAgencyCurrencyPKID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAgencyID As Integer, ByVal iOperateOn As Integer, ByVal iCurrency As Integer)
        Dim sSql As String = ""
        Dim sToday As String = "", sFrom As String = "", sTo As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = "" : sSql = "Select ACM_PKID From MST_AgentsCurrency_Masters Where ACM_Date='" & sToday & "' And ACM_OperateOn=" & iOperateOn & " And ACM_Currency=" & iCurrency & " And ACM_CompID=" & iCompID & ""
            If iAgencyID > 0 Then
                sSql = sSql & " And ACM_Agency=" & iAgencyID & ""
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
            'Agency Currency
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
    Public Sub AgencyCurrencyApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update MST_AgentsCurrency_Masters set"
            If sType = "Created" Then
                sSql = sSql & " ACM_DelFlag='A',ACM_APPROVEDBY=" & iSessionUsrID & ", ACM_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " ACM_DelFlag='D',ACM_DeletedBy=" & iSessionUsrID & ", ACM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " ACM_DelFlag='A',ACM_RecallBy=" & iSessionUsrID & ", ACM_RecallOn=Getdate(),"
            End If
            sSql = sSql & " ACM_IPAddress='" & sIPAddress & "' Where ACM_CompID=" & iACID & " And ACM_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAgencyCurrencyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAgencyID As Integer, ByVal iOperateOn As Integer, ByVal iCurrency As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Dim sToday As String = ""
        Try
            sToday = objGen.GetCurrentDate(sNameSpace)
            sSql = " Select * from MST_AgentsCurrency_Masters Where ACM_Date='" & sToday & "' And ACM_OperateOn=" & iOperateOn & " And ACM_Currency=" & iCurrency & " And ACM_CompID=" & iCompID & ""
            If iAgencyID > 0 Then
                sSql = sSql & " And ACM_Agency=" & iAgencyID & ""
            End If
            If iID > 0 Then
                sSql = sSql & " And ACM_PKID=" & iID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAgencyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAgencyID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select FE_AgencyName FROM MST_ForeignExchange_Agents Where FE_ID=" & iAgencyID & " And FE_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAgenctsName(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select FE_ID,FE_AgencyName from MST_ForeignExchange_Agents where FE_CompID=" & iCompID & " And FE_DelFlag='A' Order by FE_AgencyName"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCurrencyType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CUR_ID,CUR_CODE + ' [' + CUR_CountryName + ']' as CUR_Code From Currency_master where CUR_Status = 'A'"
            sSql = sSql & " And CUR_ID In(Select FEA_Currency From MST_FEAgents_Currency Where FEA_FEID=" & iID & " And FEA_CompID=" & iCompID & ")"
            sSql = sSql & " Order by CUR_CountryName"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
