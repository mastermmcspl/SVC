Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Public Class clsGeneralFunctions
    Private objDBL As New DBHelper
    Private objGen As New clsFASGeneral
    Public Function GetDescription(ByVal sAc As String, ByVal iAcID As Integer, ByVal sSql As String) As String
        Dim dt As New DataTable
        Dim sDesc As String = ""
        Try
            dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            If dt.Rows.Count > 0 Then
                sDesc = dt.Rows(0)(0).ToString()
            End If
            Return sDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CreateWorkingDir(ByVal sAC As String, ByVal iACID As Integer, ByVal sUserName As String, ByVal sKey As String) As String
        Dim objFileInfo As System.IO.FileInfo()
        Dim objDirInfo As DirectoryInfo
        Dim iIndxFiles As Integer
        Dim sSql As String, sGetImgPath As String, sPaths As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where SAD_CompID=" & iACID & " And sad_Config_Key='" & sKey & "'"
            sGetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            sPaths = sGetImgPath & "\" & sUserName
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            Else
                objDirInfo = New IO.DirectoryInfo(sPaths)
                objFileInfo = objDirInfo.GetFiles()
                For iIndxFiles = 0 To objFileInfo.Length - 1
                    Try
                        objFileInfo(iIndxFiles).Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSQLDate(ByVal sNameSpace As String)
        Try
            Return objDBL.SQLExecuteScalar(sNameSpace, "Select GetDate()")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUserFullName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As String) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName from Sad_Userdetails where usr_ID = '" & iUserID & "' And Usr_CompID = " & iCompID & ""
            GetUserFullName = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function GetEDICTSettingValue(ByVal sAC As String, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_config_value from sad_config_settings where sad_Config_Key ='" & sKey & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndCreateWorkingDirFromPath(ByVal sAC As String, ByVal sGetImgPath As String) As String
        Dim sPaths As String
        Try
            If sGetImgPath.EndsWith("\") = False Then
                sPaths = sGetImgPath & "\"
            Else
                sPaths = sGetImgPath
            End If
            If Not Directory.Exists(sPaths) Then
                Directory.CreateDirectory(sPaths)
            End If
            Return sPaths
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ClearBrowseDirectory(ByVal sBrowse As String)
        Try
            If System.IO.Directory.Exists(sBrowse) = True Then
                Dim files() As String
                files = Directory.GetFileSystemEntries(sBrowse)
                For Each element As String In files
                    If System.IO.File.Exists(element) = True Then
                        Try
                            My.Computer.FileSystem.DeleteFile(System.IO.Path.Combine(sBrowse, System.IO.Path.GetFileName(element)))
                        Catch ex As Exception
                        End Try
                    End If
                Next
            Else
                System.IO.Directory.CreateDirectory(sBrowse)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String, ByVal sCompColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & "  Where " & sCompColumn & "=" & iACID & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCodeID(ByVal sAC As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Cust_ID from mst_Customer_Master where Cust_Code ='" & sAC & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentMonthName(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select DateName(Month,Getdate())"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentMonthID(ByVal sAC As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Datepart(Month,Getdate())"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentDate(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate(),103)"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultYear(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select YMS_ID from Acc_Year_Master where YMS_Default=1 and YMS_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select YMS_ID from Acc_Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select (Convert(nvarchar(50),YMS_From_Year) + ' - ' + Convert(nvarchar(50),YMS_To_Year)) as year from Acc_Year_Master where YMS_ID = " & iYearID & " and YMS_CompId=" & iACID & " order by YMS_ID asc"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAddYearToFinancialYear1(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Try
            sSql = "Select YMS_YearID FROM Acc_Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql1 = "Select YMS_ID,YMS_YearID FROM Acc_Year_Master where YMS_YearID<=" & iDefaultYearID & "+ " & iNo & " And  YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID DESC"
            Return objDBL.SQLExecuteDataTable(sAC, sSql1)
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function GetAddYearTo2DigitFinancialYear(ByVal sAC As String, ByVal iACID As Integer, ByVal iNo As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iDefaultYearID As Integer
        Try
            sSql = "Select YMS_YearID FROM Acc_Year_Master where YMS_default=1 And YMS_CompID=" & iACID & " And YMS_Delflag='A'"
            iDefaultYearID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql1 = "Select YMS_YearID,substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) As YMS_ID FROM Acc_Year_Master where YMS_YearID<=" & iDefaultYearID & "+ " & iNo & " And  YMS_CompID=" & iACID & " And YMS_Delflag='A' ORDER BY YMS_YearID DESC"
            Return objDBL.SQLExecuteDataTable(sAC, sSql1)
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Function
    Public Function Get2DigitFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select substring(YMS_ID,3,2)+ '-' +substring(YMS_ID,8,2) from Acc_Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLast2DigitFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select substring(YMS_ID,3,2) from Acc_Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Get4DigitFinancialYearName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select substring(YMS_ID,1,4) from Acc_Year_Master where YMS_CompID=" & iACID & " And YMS_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMonthNameFromMothID(ByVal iMonthID As String) As String
        Dim sMonth As String = ""
        Try
            If iMonthID = 1 Then
                sMonth = "January"
            ElseIf iMonthID = 2 Then
                sMonth = "February"
            ElseIf iMonthID = 3 Then
                sMonth = "March"
            ElseIf iMonthID = 4 Then
                sMonth = "April"
            ElseIf iMonthID = 5 Then
                sMonth = "May"
            ElseIf iMonthID = 6 Then
                sMonth = "June"
            ElseIf iMonthID = 7 Then
                sMonth = "July"
            ElseIf iMonthID = 8 Then
                sMonth = "August"
            ElseIf iMonthID = 9 Then
                sMonth = "September"
            ElseIf iMonthID = 10 Then
                sMonth = "October"
            ElseIf iMonthID = 11 Then
                sMonth = "November"
            ElseIf iMonthID = 12 Then
                sMonth = "December"
            End If
            Return sMonth
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFASSettingValue(ByVal sAC As String, ByVal iACID As Integer, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key='" & sKey & "' and sad_compid=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserIDFromFullName(ByVal sAC As String, ByVal iACID As Integer, ByVal sUser As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_FullName='" & sUser & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserIDFromLoginName(ByVal sAC As String, ByVal iACID As Integer, ByVal sUser As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_LoginName='" & sUser & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserIDFromUserCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_Userdetails where Usr_Code='" & sCode & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserFullNameFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserLoginNameFromUserID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_LoginName from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserNameAndCodeFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName + ' - ' + Usr_Code From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllModuleJobCode(ByVal sAC As String, ByVal sModule As String, ByVal iYearID As Integer, ByVal sYearName As String, ByVal iNameID As Integer) As String
        Dim iMaxID As Integer
        Dim sMaxID As String = "", sJobCode As String = "", sModuleCode As String = ""
        Try
            Select Case sModule
                Case "BCM" 'RLIC/BCM/15-16/01
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from CMA_Assignment_Details where CAD_AuditYear=" & iYearID & "")
                    sModuleCode = "BCM"
                Case "AUDIT"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select ATYP_ID from Audit_ThreeYearPlan where ATYP_YearID=" & iYearID & " and ATYP_ID=" & iNameID & " ")
                    sModuleCode = "Audit"
                Case "RCSA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RCSA where RCSA_FinancialYear=" & iYearID & "")
                    sModuleCode = "RCSA"
                Case "RA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RA where RA_FinancialYear=" & iYearID & "")
                    sModuleCode = "RA"
                Case "FRR"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_RRF_PlanningSchecduling_Details where RPD_YearID=" & iYearID & "")
                    sModuleCode = "FRR"
                Case "KCC"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_KCC_PlanningSchecduling_Details where KCC_YearID=" & iYearID & "")
                    sModuleCode = "KCC"
                Case "BRR"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Risk_BRRSchedule where BRRS_FinancialYear=" & iYearID & "")
                    sModuleCode = "BRR"
                Case "BIA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Audit_BAnnualAuditSchedule where BAAS_FinancialYear=" & iYearID & "")
                    sModuleCode = "BIA"
                Case "COMPLIANCE"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select CP_ID from Compliance_Plan where CP_YearID=" & iYearID & " and CP_ID=" & iNameID & "  ")
                    sModuleCode = "Compliance"
                Case "CRSA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Compliance_CRSA where CRSA_FinancialYear=" & iYearID & "")
                    sModuleCode = "CRSA"
                Case "CRA"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from Compliance_CRA where CRA_FinancialYear=" & iYearID & "")
                    sModuleCode = "CRA"
                Case "LC"
                    iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select Count(*)+1 from LC_Schedule where LCS_YearID=" & iYearID & "")
                    sModuleCode = "LC"
            End Select
            If iMaxID = 0 Then
                sMaxID = "001"
            ElseIf iMaxID > 0 And iMaxID < 10 Then
                sMaxID = "00" & iMaxID
            ElseIf iMaxID >= 10 And iMaxID < 100 Then
                sMaxID = "0" & iMaxID
            Else
                sMaxID = iMaxID
            End If
            sJobCode = "RLIC/" & sModuleCode & "/" & sYearName & "/" & sMaxID
            Return sJobCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveFASFormOperations(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModule As String, ByVal sForm As String, ByVal sEvent As String,
                                       ByVal iMasterID As Integer, ByVal sMasterName As String, ByVal iSubMasterID As Integer, ByVal sSubMasterName As String, ByVal sIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_UserID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iUserID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Module", OleDb.OleDbType.VarChar, 50)
            ObjParam(iRCSADParamCount).Value = sModule
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Form", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sForm
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Event", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sEvent
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iSubMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sSubMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iRCSADParamCount).Value = sIPAddress
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iACID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spAudit_Log_Form_Operations", ObjParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveUserLogOperations(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sLoginUserName As String, ByVal sLogType As String, ByVal sIPAddress As String, ByVal sPassword As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sAC, "Select ISNULL(MAX(ALP_PKID )+1,1) From audit_log_operations")
            sSql = "Insert Into Audit_Log_Operations (ALP_PKID,ALP_UserName,ALP_UserID,ALP_Password,ALP_Date,ALP_LogType,ALP_IPAddress,ALP_CompID )"
            sSql = sSql & "Values(" & iMaxID & ",'" & sLoginUserName & "'," & iUserID & ",'" & sPassword & "',GetDate(),'" & sLogType & "','" & sIPAddress & "'," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadColors(ByVal sAc As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select TC_Color_Name,TC_KeyCode from Trace_Color_Master Where TC_CompID=" & iACID & " "
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetColorNameFromPKID(ByVal sAc As String, ByVal iAcID As Integer, iColor As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select TC_Color_Name From Trace_Color_Master where TC_CompID=" & iAcID & " And  TC_KeyCode=" & iColor & ""
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetColorIDFromName(ByVal sAc As String, ByVal iAcID As Integer, ByVal sColors As String) As String
        Dim sSql As String
        Try
            sSql = "Select TC_KeyCode from Trace_Color_Master Where Upper(TC_Color_Name)=Upper('" & sColors & "') and TC_CompID=" & iAcID & " "
            Return objDBL.SQLExecuteScalar(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDesignatedUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrDesgination As Integer, ByVal sSearchUser As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select usr_id,usr_LoginName,usr_FullName from sad_userdetails Where Usr_Designation=" & iUsrDesgination & " and (Usr_DutyStatus='A' Or Usr_DutyStatus='L' And Usr_DutyStatus='B')"
            If sSearchUser <> "" Then
                sSql = sSql & " And usr_FullName like '" & sSearchUser & "%'"
            End If
            sSql = sSql & " order by usr_FullName Asc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select ENT_ENTITYName from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iSubFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select SEM_NAME from MST_SUBENTITY_MASTER where SEM_compid=" & iACID & " And SEM_ID=" & iSubFunID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetProcessNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iProID As String) As String
        Dim sSql As String
        Try
            sSql = "Select PM_NAME from MST_PROCESS_MASTER where PM_COMPID=" & iACID & " And PM_ID=" & iProID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionOwnerHODIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunOwnerID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionOwnerHODFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select ENT_FunownerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionOwnerHODNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails  where usr_compID=" & iACID & "  and usr_ID in (Select ENT_FunownerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionManagerIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunManagerID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionManagerNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails  where usr_compID=" & iACID & "  and usr_ID in (Select Ent_FunManagerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionManagersFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select Ent_FunManagerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionSPOCIDFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Ent_FunSPOCID from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID=" & iFunID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionSPOCNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails where usr_compID=" & iACID & "  and usr_ID in (Select Ent_FunSPOCID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFunctionSPOCsFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (Select Ent_FunSPOCID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFUNOwnerHODManagerSPOCIDs(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Convert(Varchar(10),ENT_FunownerID) + ',' +  Convert(Varchar(10),Ent_FunManagerID) + ',' +  Convert(Varchar(10),Ent_FunSPOCID) from mst_Entity_master where ENT_ID=" & iFunID & " And ENT_CompID=" & iACID & ""
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
                sStr = objDBL.SQLExecuteScalar(sAC, sSql)
                If sStr <> "" Then
                    sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID in (" & sStr & ")"
                    dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                End If
            Else
                sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from sad_userdetails  where usr_compID=" & iACID & " and usr_ID=0"
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllUsers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_Id,(Usr_Fullname +' - ' + Usr_Code) as Usr_Fullname from sad_userdetails Where Usr_CompId=" & iACID & " order by Usr_Fullname"
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskAndAuditMembers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, USr_fullName from sAd_userdetails where usr_compID=" & iACID & " And (Usr_riskModule=1 Or Usr_AuditModule=1) And usr_delflag='A' "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskAndComplianceMembers(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, USr_fullName from sAd_userdetails where usr_category=1 and usr_compID=" & iACID & " And (Usr_riskModule=1 Or Usr_ComplianceModule=1) And usr_delflag='A' "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserEMailFromID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select usr_Email from sad_userdetails where usr_category=1 and usr_ID=" & iUserID & " and Usr_CompID =" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionSPOCHODManagerFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iFunOwnerID As Integer = 0, iSPOCID As Integer = 0, iManagerID As Integer = 0
        Dim sIDs As String
        Dim dtIDs As New DataTable, dtUSers As New DataTable
        Try
            sSql = "select ENT_FunOwnerID,ENT_FunManagerID,ENT_FunSPOCID from mst_entity_master where ENT_ID=" & iFunctionID & " and ENT_CompID=" & iACID & ""
            dtIDs = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtIDs.Rows.Count > 0 Then
                If IsDBNull(dtIDs.Rows(0)("ENT_FunOwnerID")) = False Then
                    iFunOwnerID = dtIDs.Rows(0)("ENT_FunOwnerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunManagerID")) = False Then
                    iManagerID = dtIDs.Rows(0)("ENT_FunManagerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunSPOCID")) = False Then
                    iSPOCID = dtIDs.Rows(0)("ENT_FunSPOCID")
                End If
                sIDs = iFunOwnerID & "," & iManagerID & "," & iSPOCID

                sSql1 = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where USr_ID In (" & sIDs & ") And Usr_CompID = " & iACID & " order by Usr_FullName"
                dtUSers = objDBL.SQLExecuteDataTable(sAC, sSql1)
            End If
            Return dtUSers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionSPOCHODManagerWithEmailFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String, sSql1 As String
        Dim iFunOwnerID As Integer = 0, iSPOCID As Integer = 0, iManagerID As Integer = 0
        Dim sIDs As String
        Dim dtIDs As New DataTable, dtUSers As New DataTable
        Try
            sSql = "select ENT_FunOwnerID,ENT_FunManagerID,ENT_FunSPOCID from mst_entity_master where ENT_ID=" & iFunctionID & " and ENT_CompID=" & iACID & ""
            dtIDs = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtIDs.Rows.Count > 0 Then
                If IsDBNull(dtIDs.Rows(0)("ENT_FunOwnerID")) = False Then
                    iFunOwnerID = dtIDs.Rows(0)("ENT_FunOwnerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunManagerID")) = False Then
                    iManagerID = dtIDs.Rows(0)("ENT_FunManagerID")
                End If
                If IsDBNull(dtIDs.Rows(0)("ENT_FunSPOCID")) = False Then
                    iSPOCID = dtIDs.Rows(0)("ENT_FunSPOCID")
                End If
                sIDs = iFunOwnerID & "," & iManagerID & "," & iSPOCID

                sSql1 = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where USr_ID In (" & sIDs & ") And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_CompID = " & iACID & " order by Usr_FullName  "
                dtUSers = objDBL.SQLExecuteDataTable(sAC, sSql1)
            End If
            Return dtUSers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllRiskUsersWithEmail(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, (Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where  Usr_CompID=" & iACID & " And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_RiskModule=1 order by Usr_FullName  "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllComplianceUsersWithEmail(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Usr_ID, (Usr_FullName + ' - ' + Usr_Code) as Name from sad_userdetails where  Usr_CompID=" & iACID & " And usr_category=1 and Usr_Email like '%@%' And Usr_Email like '%.%' And Usr_ComplianceModule=1 order by Usr_FullName  "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Name from sad_org_Structure where org_node=" & iZoneID & " And Org_LevelCode=1 And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRegionNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRegionID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Name from sad_org_Structure where org_node=" & iRegionID & " And Org_LevelCode=2 And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneIDFromName(ByVal sAC As String, ByVal iACID As Integer, ByVal sZone As String) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select org_node from sad_org_Structure where Org_Name='" & sZone & "' And Org_LevelCode=1  And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRegionIDFromName(ByVal sAC As String, ByVal iACID As Integer, ByVal sRegion As String) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select org_node from sad_org_Structure where Org_Name='" & sRegion & "' And Org_LevelCode=2 And Org_DelFlag='A' and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchNameFromPKID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer) As String
        Dim sSql As String
        Try
            sSql = "select org_Name from sad_org_structure  where org_node=" & iBranchID & " and org_CompID=" & iACID & " and Org_LevelCode=4 And Org_DelFlag='A'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllZOMs(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer) As DataTable
        Dim sSql As String, sSqlSub As String
        Dim dt As New DataTable
        Dim sRegionIDs As String = "", sAreaIDs As String = "", sBranchIDs As String = ""
        Dim dtTab As New DataTable
        Try
            sSql = "Select usr_ID,usr_fullname from sad_USERDETAILS "
            If iZoneID > 0 Then
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent=" & iZoneID & " And Org_LevelCode=2 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sRegionIDs = sRegionIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sRegionIDs.StartsWith(",") = True Then
                    sRegionIDs = sRegionIDs.Remove(0, 1)
                End If
                If sRegionIDs.EndsWith(",") = True Then
                    sRegionIDs = sRegionIDs.Remove(Len(sRegionIDs) - 1, 1)
                End If
                If sRegionIDs <> "" Then
                    dtTab = Nothing
                    sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sRegionIDs & ") And Org_LevelCode=3 And Org_CompID=" & iACID & ""
                    dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                    For i = 0 To dtTab.Rows.Count - 1
                        sAreaIDs = sAreaIDs & "," & dtTab.Rows(i)("Org_Node")
                    Next
                    If sAreaIDs.StartsWith(",") = True Then
                        sAreaIDs = sAreaIDs.Remove(0, 1)
                    End If
                    If sAreaIDs.EndsWith(",") = True Then
                        sAreaIDs = sAreaIDs.Remove(Len(sAreaIDs) - 1, 1)
                    End If
                End If

                If sAreaIDs <> "" Then
                    dtTab = Nothing
                    sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sAreaIDs & ") And Org_LevelCode=4 And Org_CompID=" & iACID & ""
                    dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                    For i = 0 To dtTab.Rows.Count - 1
                        sBranchIDs = sBranchIDs & "," & dtTab.Rows(i)("Org_Node")
                    Next
                    If sBranchIDs <> "" Then
                        If sBranchIDs.StartsWith(",") = True Then
                            sBranchIDs = sBranchIDs.Remove(0, 1)
                        End If
                        If sBranchIDs.EndsWith(",") = True Then
                            sBranchIDs = sBranchIDs.Remove(Len(sBranchIDs) - 1, 1)
                        End If
                    End If
                End If

                sSql = sSql & " WHERE Usr_OrgnID In (" & iZoneID & ""
                If sRegionIDs <> "" Then
                    sSql = sSql & "," & sRegionIDs & ""
                End If
                If sAreaIDs <> "" Then
                    sSql = sSql & " ," & sAreaIDs & ""
                End If
                If sBranchIDs <> "" Then
                    sSql = sSql & "," & sBranchIDs & ""
                End If
                sSql = sSql & ") And "
            End If
            sSql = sSql & " Usr_CompID=" & iACID & " And Usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master  where (mas_Code=Lower('ZOM') or mas_Code=Upper('ZOM')) and mas_compID=" & iACID & ") order by usr_fullname"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllBranches(ByVal sAC As String, ByVal iACID As Integer, ByVal iRegionID As Integer) As DataTable
        Dim sSql As String = "", sSqlSub As String
        Dim sAreaIDs As String = "", sBranchIDs As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt.Columns.Add("Org_Node")
            dt.Columns.Add("org_name")

            If iRegionID > 0 Then
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & iRegionID & ") And Org_LevelCode=3 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sAreaIDs = sAreaIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sAreaIDs.StartsWith(",") = True Then
                    sAreaIDs = sAreaIDs.Remove(0, 1)
                End If
                If sAreaIDs.EndsWith(",") = True Then
                    sAreaIDs = sAreaIDs.Remove(Len(sAreaIDs) - 1, 1)
                End If
            End If

            If sAreaIDs <> "" Then
                dtTab = Nothing
                sSqlSub = "Select Org_Node From Sad_Org_Structure Where Org_Parent In (" & sAreaIDs & ") And Org_LevelCode=4 And Org_CompID=" & iACID & ""
                dtTab = objDBL.SQLExecuteDataSet(sAC, sSqlSub).Tables(0)
                For i = 0 To dtTab.Rows.Count - 1
                    sBranchIDs = sBranchIDs & "," & dtTab.Rows(i)("Org_Node")
                Next
                If sBranchIDs <> "" Then
                    If sBranchIDs.StartsWith(",") = True Then
                        sBranchIDs = sBranchIDs.Remove(0, 1)
                    End If
                    If sBranchIDs.EndsWith(",") = True Then
                        sBranchIDs = sBranchIDs.Remove(Len(sBranchIDs) - 1, 1)
                    End If
                    sSql = "Select Org_Code +' - '+ Org_Name As Org_Name,Org_Node from Sad_Org_Structure Left Join Risk_BRRPlanning On BRRP_BranchID=Org_Node"
                    sSql = sSql & " where Org_Node in (" & sBranchIDs & ") And BRRP_Status='S' And Org_CompID=" & iACID & ""
                End If
            End If

            If iRegionID = 0 Or sAreaIDs = "" Or sBranchIDs = "" Then
                sSql = "Select Org_Code +' - '+ Org_Name As Org_Name,Org_Node from Sad_Org_Structure Left Join Risk_BRRPlanning On BRRP_BranchID=Org_Node"
                sSql = sSql & " where Org_LevelCode=4 And BRRP_Status='S' And Org_CompID=" & iACID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranchManager(ByVal sAC As String, ByVal iACID As Integer, ByVal sBranchID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select usr_ID, usr_fullname from sad_USERDETAILS WHERE "
            If sBranchID > 0 Then
                sSql = sSql & " Usr_OrgnID In('" & sBranchID & "') And "
            End If
            sSql = sSql & "usr_compID=" & iACID & " And usr_designation In(Select mas_ID from SAD_GRPDESGN_General_Master  where mas_Code=Lower('BM') or mas_Code=Upper('BM') and mas_compID=" & iACID & ") order by usr_fullname"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlLibraryID(ByVal sAC As String, ByVal iACID As Integer, ByVal sControlName As String, ByVal sCheckIsKey As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select MCL_PKID From MST_Control_Library Where MCl_CompID=" & iACID & " And Upper(MCL_ControlName)=Upper('" & sControlName & "')"
            If sCheckIsKey = "YES" Then
                sSql = sSql & " And MCL_IsKey=1 "
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskLibraryID(ByVal sAC As String, ByVal iACID As Integer, ByVal sRiskLibrary As String, ByVal sCheckIsKey As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select MRL_PKID From MST_RISK_Library Where MRL_CompID=" & iACID & " And Upper(MRL_RiskName)=Upper('" & sRiskLibrary & "')"
            If sCheckIsKey = "YES" Then
                sSql = sSql & " And MRL_IsKey=1 "
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneNameFromBranchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Org_Name from sad_org_structure where org_levelcode=1 and org_node in(Select org_parent from sad_org_structure"
            sSql = sSql & " where  org_levelcode=2 And org_node In (Select Org_Parent from sad_org_structure where  org_levelcode=3 And org_node In (Select Org_Parent from sad_org_structure"
            sSql = sSql & " where org_levelcode=4 and org_node=" & iBranchID & " And Org_CompId=" & iACID & ")))"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRegionNameFromBranchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iBranchID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Org_Name from sad_org_structure where org_levelcode=2 and org_node in(Select org_parent from sad_org_structure where org_levelcode=3 And org_node In"
            sSql = sSql & " (Select Org_Parent from sad_org_structure where org_levelcode=4 and org_node=" & iBranchID & " And Org_CompId=" & iACID & "))"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SaveGRACeFormOperations(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModule As String, ByVal sForm As String, ByVal sEvent As String,
                                     ByVal iMasterID As Integer, ByVal sMasterName As String, ByVal iSubMasterID As Integer, ByVal sSubMasterName As String, ByVal sIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_UserID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iUserID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Module", OleDb.OleDbType.VarChar, 50)
            ObjParam(iRCSADParamCount).Value = sModule
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Form", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sForm
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_Event", OleDb.OleDbType.VarChar, 500)
            ObjParam(iRCSADParamCount).Value = sEvent
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_MasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iSubMasterID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_SubMasterName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iRCSADParamCount).Value = sSubMasterName
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iRCSADParamCount).Value = sIPAddress
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjParam(iRCSADParamCount) = New OleDb.OleDbParameter("@ALFO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iRCSADParamCount).Value = iACID
            ObjParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spAudit_Log_Form_Operations", ObjParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetYearID(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String, status As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AS_YearID From Application_Settings Where  AS_CompID=" & iCompID & " and AS_Default = 1"
            status = objDBL.SQLGetDescription(sNameSpace, sSql)
            If status Then
                GetYearID = status
            Else
                GetYearID = 0
            End If
            Return GetYearID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTempPath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As String
        Dim sSql As String = "", sValue As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select sad_config_value from sad_config_settings where Sad_CompID =" & iCompID & " and Sad_Config_Key = '" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sValue = dt.Rows(0)(0).ToString()
            End If
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStartDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "SElect YMS_FromDate From Acc_Year_Master where YMS_ID=" & iYearID & " "
            GetStartDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            'sSql = "" : sSql = "Select Datepart(day,'" & objGen.FormatDtForRDBMS(dSDate, "CT") & "')"
            'GetStartDate = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'GetStartDate = objGen.FormatDtForRDBMS(dSDate, "D")
            Return GetStartDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEndDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        'Dim dEDate As String
        Try
            sSql = "" : sSql = "SElect YMS_ToDate From Acc_Year_Master where YMS_ID=" & iYearID & " "
            GetEndDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            'sSql = "" : sSql = "Select Datepart(day,'" & objGen.FormatDtForRDBMS(dEDate, "CT") & "')"
            'GetEndDate = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'GetEndDate = objGen.FormatDtForRDBMS(dEDate, "D")
            Return GetEndDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserPwd(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_Password from Sad_Userdetails where Usr_ID='" & iUserID & "' And Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Digital Filing,Search
    Public Shared Function FormatMyDate(ByVal sDate As String)
        Dim sFromDte As String
        Dim sArry() As String
        Dim i As Int16
        Dim iValue As String
        Try
            If Len(Trim(sDate)) <> 0 Then
                sArry = Split(sDate, "/")
                If sArry.Length = 1 Then
                    FormatMyDate = String.Empty
                    Exit Try
                End If
                For i = 0 To UBound(sArry)
                    If Len(Trim(sArry(i))) = 1 Then
                        iValue = "0" & sArry(i)
                    Else
                        iValue = sArry(i)
                    End If

                    Select Case i
                        Case 0
                            sFromDte = iValue
                        Case 1
                            sFromDte = iValue & "/" & sFromDte
                        Case 2
                            sFromDte = sFromDte & "/" & iValue
                    End Select
                Next
                Return sFromDte
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------
    Public Function GetEdictMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal sTable As String, ByVal sColumn As String) As Integer
        Dim sSql As String
        Dim objMax As Object
        Try
            sSql = "Select ISNULL(MAX(" & sColumn & ")+1,1) FROM " & sTable & " "
            objMax = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If Not objMax Is DBNull.Value Then
                Return Integer.Parse(objMax.ToString())
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBanksName(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iAcc_GL As New Integer

        Try
            sSql = "select Acc_GL From Acc_Application_Settings Where Acc_Types='Bank' And Acc_LedgerType='Bank'"
            iAcc_GL = objDBL.SQLExecuteScalar(sNameSpace, sSql)

            sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_parent=" & iAcc_GL & " and gl_CompId=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFinancialYearFromDate(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As String) As String
        Dim sSql As String
        Try
            sSql = "Select YMS_FromDate from Acc_Year_Master where YMS_CompID=" & iACID & " And YMS_ID=" & iYearID & " and YMS_Default=1"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
