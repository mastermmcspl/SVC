Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsApplicationSettings
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private objGenFas As New clsFASGeneral
    Private MPS_MinimumChar As Integer
    Private MPS_MaximumChar As Integer
    Private MPS_RecoveryAttempts As Integer
    Private MPS_UnsuccessfulAttempts As Integer
    Private MPS_PasswordExpiryDays As Integer
    Private MPS_NotLoginDays As Integer
    Private MPS_Password_Contains As String
    Private MPS_PasswordExpiryAlertDays As Integer
    Private MPS_CompID As Integer

    Private Conf_IPAddress As String
    Private conf_Port As Integer
    Private Conf_From As String
    Private conf_SenderID As String
    Private conf_UpdatedBy As Integer
    Private conf_CompID As Integer
    Private Conf_Status As String
    Private Conf_INS_IPAddress As String

    Public Property iMPS_CompID() As Integer
        Get
            Return (MPS_CompID)
        End Get
        Set(ByVal Value As Integer)
            MPS_CompID = Value
        End Set
    End Property
    Public Property iMPS_PasswordExpiryAlertDays() As Integer
        Get
            Return (MPS_PasswordExpiryAlertDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_PasswordExpiryAlertDays = Value
        End Set
    End Property
    Public Property sMPS_Password_Contains() As String
        Get
            Return (MPS_Password_Contains)
        End Get
        Set(ByVal Value As String)
            MPS_Password_Contains = Value
        End Set
    End Property
    Public Property iMPS_NotLoginDays() As Integer
        Get
            Return (MPS_NotLoginDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_NotLoginDays = Value
        End Set
    End Property
    Public Property iMPS_PasswordExpiryDays() As Integer
        Get
            Return (MPS_PasswordExpiryDays)
        End Get
        Set(ByVal Value As Integer)
            MPS_PasswordExpiryDays = Value
        End Set
    End Property
    Public Property iMPS_UnsuccessfulAttempts() As Integer
        Get
            Return (MPS_UnsuccessfulAttempts)
        End Get
        Set(ByVal Value As Integer)
            MPS_UnsuccessfulAttempts = Value
        End Set
    End Property
    Public Property iMPS_RecoveryAttempts() As Integer
        Get
            Return (MPS_RecoveryAttempts)
        End Get
        Set(ByVal Value As Integer)
            MPS_RecoveryAttempts = Value
        End Set
    End Property
    Public Property iMPS_MaximumChar() As Integer
        Get
            Return (MPS_MaximumChar)
        End Get
        Set(ByVal Value As Integer)
            MPS_MaximumChar = Value
        End Set
    End Property
    Public Property iMPS_MinimumChar() As Integer
        Get
            Return (MPS_MinimumChar)
        End Get
        Set(ByVal Value As Integer)
            MPS_MinimumChar = Value
        End Set
    End Property
    Public Property sConf_IPAddress() As String
        Get
            Return (Conf_IPAddress)
        End Get
        Set(ByVal Value As String)
            Conf_IPAddress = Value
        End Set
    End Property
    Public Property iconf_Port() As Integer
        Get
            Return (conf_Port)
        End Get
        Set(ByVal Value As Integer)
            conf_Port = Value
        End Set
    End Property
    Public Property sConf_From() As String
        Get
            Return (Conf_From)
        End Get
        Set(ByVal Value As String)
            Conf_From = Value
        End Set
    End Property
    Public Property sconf_SenderID() As String
        Get
            Return (conf_SenderID)
        End Get
        Set(ByVal Value As String)
            conf_SenderID = Value
        End Set
    End Property
    Public Property iconf_UpdatedBy() As Integer
        Get
            Return (conf_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            conf_UpdatedBy = Value
        End Set
    End Property
    Public Property iconf_CompID() As Integer
        Get
            Return (conf_CompID)
        End Get
        Set(ByVal Value As Integer)
            conf_CompID = Value
        End Set
    End Property
    Public Property sConf_Status() As String
        Get
            Return (Conf_Status)
        End Get
        Set(ByVal Value As String)
            Conf_Status = Value
        End Set
    End Property
    Public Property sConf_INS_IPAddress() As String
        Get
            Return (Conf_INS_IPAddress)
        End Get
        Set(ByVal Value As String)
            Conf_INS_IPAddress = Value
        End Set
    End Property
    Public Function BindCurrencyType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CUR_ID,CUR_CODE + ' [' + CUR_CountryName + ']' as CUR_Code From Currency_master where CUR_Status = 'A' Order by CUR_CountryName"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SaveSettings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String, ByVal sValue As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            If CheckForExitstingRecord(sNameSpace, iCompID, sCode) = False Then
                iMaxID = objGen.GetMaxID(sNameSpace, iCompID, "sad_config_settings", "sad_config_ID", "SAD_CompID")
                sSql = "Insert into sad_config_settings(sad_config_ID,sad_Config_Key,sad_Config_Value,SAD_CompID)"
                sSql = sSql & "Values(" & iMaxID & ", '" & sCode & "','" & sValue & "'," & iCompID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                sSql = "Update sad_config_settings set sad_Config_Value = '" & sValue & "'"
                sSql = sSql & "where SAD_CompID=" & iCompID & " And sad_Config_Key ='" & sCode & "'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveApplicationSettings(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String, ByVal sValue As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_Key", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Config_Value", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_CompID", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spApplicationSettings", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAppSettings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iIPAddress As String, ByVal iYearID As Integer, ByRef sdate As Date)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMaxId As Integer
        Try
            sSql = "Select * from Application_Settings where AS_CompID  =" & iCompID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "" : sSql = "Update Application_Settings set AS_YearID = '" & iYearID & "',AS_Default =1, "
                sSql = sSql & "AS_Operation='U',AS_IPAddress='" & iIPAddress & "',AS_StartDate =" & objGenFas.FormatDtForRDBMS(sdate, "I") & " Where AS_CompID =" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMaxId = Convert.ToInt32(objDBL.SQLExecuteScalar(sNameSpace, "Select IsNull(MAX(AS_ID),0)+1 from Application_Settings"))
                sSql = "" : sSql = "Insert into Application_Settings(AS_ID,AS_YearID,AS_Delflag,AS_CompID,"
                sSql = sSql & "AS_Default,AS_Operation,AS_IPAddress,AS_StartDate)Values(" & iMaxId & "," & iYearID & ",'A'," & iCompID & ","
                sSql = sSql & "1,'C','" & iIPAddress & "'," & objGenFas.FormatDtForRDBMS(sdate, "I") & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForExitstingRecord(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Sad_config_ID from sad_config_settings where SAD_CompID=" & iCompID & " And sad_Config_Key ='" & sCode & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetApllicationSettingsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from sad_config_settings Where SAD_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePasswordDetails(ByVal sAC As String, ByVal objPassword As clsApplicationSettings)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_MinimumChar", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_MinimumChar
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_MaximumChar", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_MaximumChar
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MSP_RecoveryAttempts", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_RecoveryAttempts
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_UnsuccessfulAttempts", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_UnsuccessfulAttempts
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_PasswordExpiryDays", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = objPassword.iMPS_PasswordExpiryDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_NotLoginDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_NotLoginDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_Password_Contains", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objPassword.sMPS_Password_Contains
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_PasswordExpiryAlertDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPassword.iMPS_PasswordExpiryAlertDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@MPS_CompID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPassword.iMPS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spPasswordManagement", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmailSettings(ByVal sAC As String, ByVal objEmail As clsApplicationSettings)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objEmail.sConf_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_Port", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_Port
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_From", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objEmail.sConf_From
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@conf_SenderID", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objEmail.sconf_SenderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_INS_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objEmail.sConf_INS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objEmail.sConf_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Conf_CompID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmail.iconf_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sAC, "spEmailSettings", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPasswordSettings(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from MST_Password_Setting Where MPS_CompID=" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmailsettings(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select conf_IPAddress,Conf_port,Conf_From,Conf_SenderID,conf_CompID from INS_COnfig Where conf_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAppSatartDate(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Application_Settings Where AS_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
