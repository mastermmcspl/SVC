Imports DatabaseLayer
Imports System
Imports System.Data
Public Class clsLogin
    Dim objDB As New DBHelper
    Dim objGen As New clsFASGeneral
    Private objEDICTGen As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iNoCases As Integer
    Private sErrorInLogin As String
    Private bLogin As Boolean
    Public Property NumberofCases() As Integer
        Get
            Return iNoCases
        End Get
        Set(ByVal Value As Integer)
            iNoCases = Value
        End Set
    End Property
    Public Property ErrorInLogin() As String
        Get
            ErrorInLogin = sErrorInLogin
        End Get
        Set(ByVal Value As String)
            sErrorInLogin = Value
        End Set
    End Property
    Public Property Login() As String
        Get
            Login = bLogin
        End Get
        Set(ByVal Value As String)
            bLogin = Value
        End Set
    End Property
    Public Function CheckUserIsValid(ByVal sNameSpace As String, ByVal sName As String, ByVal sPwd As String, ByRef sError As String) As Object
        Dim sSQl As String
        Dim iGrpId As Int16 = 0
        Dim MyDr As OleDb.OleDbDataReader
        Dim objClsLogin As New clsLogin
        Try
            If CheckNoOfattempts(sNameSpace, sName, "usr_NoOfUnSucsfAtteptts", "sas_noofloginattempt") = False Then
                sError = "Your password is blocked for security purpose. Contact System Administrator to remove blocking or get new password"
                Exit Function
            End If

            sSQl = "Select * from Sad_UserDetails where usr_LoginName = '" & objGen.SafeSQL(sName) & "'"
            MyDr = objDB.SQLDataReader(sNameSpace, sSQl)
            If MyDr.HasRows = True Then
                CheckUserIsValid = True
                While MyDr.Read
                    'If UCase(sPwd) <> UCase(deCryptPWD(MyDr("usr_Password").ToString.Remove(MyDr("usr_Password").ToString.LastIndexOf("-"), (MyDr("usr_Password").ToString.Length - MyDr("usr_Password").ToString.LastIndexOf("-"))), 1 & AscW(UCase(Trim(sName).Substring(0, 1))))) Then
                    '    CheckUserIsValid = False
                    '    sError = UpdateUsrSuccessfullcount(sName, "ChkAtteOnLogin")
                    '    If sError = String.Empty Then
                    '        sError = "Invalid password"
                    '    End If
                    '    Exit Try
                    'End If
                    If (sPwd.Length <= 0) Then
                        sError = "Please enter your password"
                    End If

                    If IsDBNull(MyDr("usr_LockStatus")) = False Then
                        If UCase(MyDr("usr_LockStatus")) = "LOCKED" Then
                            sError = "This is Locked User"
                            objClsLogin.bLogin = False
                            Exit Try
                        End If
                    End If
                    If IsDBNull(MyDr("usr_FullName")) = False Then
                        If UCase(MyDr("usr_DelStatus")) = "X" Then
                            sError = "This user is De-Activated"
                            objClsLogin.bLogin = False
                        End If
                    End If
                    If IsDBNull(MyDr("usr_DelStatus")) = False Then
                        If UCase(MyDr("usr_DelStatus")) = "X" Then
                            sError = "This user is De-Activated"
                            objClsLogin.bLogin = False
                        End If
                    End If

                End While
                MyDr.Close()
                objClsLogin.bLogin = True
            Else
                objClsLogin.bLogin = False
                sError = "Not a valid Login Name"
            End If
            Return objClsLogin
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckNoOfattempts(ByVal sNameSpace As String, ByVal LoginName As String, ByVal SadUsrFieldName As String, ByVal SadadmFieldname As String) As Boolean
        Dim NoOfAtmAllowed As String = "", NoAttmDone As String = "", sSQL As String = ""
        Try
            sSQL = "" : sSQL = "Select " & SadUsrFieldName & " from Sad_User_Admin where usr_id=(select usr_id from sad_userdetails where usr_loginname='" & LoginName & "')"
            NoAttmDone = objDB.SQLExecuteScalar(sNameSpace, sSQL)

            sSQL = "" : sSQL = "Select " & SadadmFieldname & " from sad_admin_settings"
            NoOfAtmAllowed = objDB.SQLExecuteScalar(sNameSpace, sSQL)

            If Val(NoAttmDone) < Val(NoOfAtmAllowed) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckUserApprovedOrNot(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginUserName As String) As Boolean
        Dim sSql As String
        Dim sStatus As String
        Try
            sSql = "Select Usr_DutyStatus from Sad_Userdetails where Usr_loginname='" & sLoginUserName & "' And Usr_CompID = " & iACID & ""
            sStatus = objDB.SQLExecuteScalar(sAC, sSql)
            If sStatus = "W" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Digital Filing,Search
    Public Sub UpdateLogin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iCount As Integer
        Try
            iCount = objDB.SQLExecuteScalarInt(sAC, "Select Usr_NoOfLogin from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & "")
            sSql = "Update Sad_Userdetails set Usr_Status='N',Usr_IPAddress='" & sIPAddress & "',Usr_NoOfUnSucsfAtteptts=0,Usr_IsPasswordReset=0,Usr_DutyStatus='A',Usr_NoOfLogin=" & iCount + 1 & ","
            sSql = sSql & "Usr_LastLoginDate=GetDate() where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            objDB.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub UpdateLogoff(ByVal sAC As String, ByVal iUserID As Integer)
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Max(adt_keyID) As adt_keyID from audit_log where adt_userID = " & iUserID & ""
            dr = objDB.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "Update audit_log set ADT_LOGOUT=GetDate() where adt_userID = " & iUserID & " and adt_keyID=" & dr("adt_keyID") & ""
                objDB.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
End Class
