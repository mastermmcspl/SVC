Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsGeneralMaster
    Dim objDB As New DBHelper
    Public Function LoadMasterType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Type from ACC_Master_Type where mas_category =" & iMasterType & " and Mas_Delflag ='X' "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer, ByVal sSearchText As String, ByVal iMasterType As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Description", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from ACC_General_Master where Mas_master = " & iMasterType & " and Mas_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Mas_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Mas_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Mas_DelFlag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And Mas_Desc like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By Mas_Desc ASC"

            ds = objDB.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("SrNo") = i + 1
                    dr("Id") = ds.Tables(0).Rows(i)("Mas_Id").ToString()
                    dr("Description") = ds.Tables(0).Rows(i)("Mas_Desc").ToString()

                    If (ds.Tables(0).Rows(i)("Mas_delflag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Mas_delflag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Mas_delflag") = "D") Then
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
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from ACC_General_Master where Mas_master =" & iMaster & " and Mas_CompID =" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescID As Integer, ByVal iMasterType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from ACC_General_Master where Mas_id =" & iDescID & " and Mas_master = " & iMasterType & " and Mas_CompID =" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasterType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterType As Integer) As String
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Mas_Type from ACC_Master_Type where Mas_Id =" & iMasterType & " and Mas_Delflag ='X'"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("Mas_Type")) = False Then
                    GetMasterType = dr("Mas_Type")
                Else
                    GetMasterType = ""
                End If
            Else
                GetMasterType = ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckExistingDetails(ByVal sNamSpace As String, ByVal icompId As Integer, ByVal sDesc As String, ByVal IMasterId As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from ACC_General_Master where  Mas_Desc = '" & sDesc & "' and Mas_CompID =" & icompId & " and Mas_Delflag ='A' and Mas_master=" & IMasterId & ""
            CheckExistingDetails = objDB.SQLCheckForRecord(sNamSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveGeneralMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iMasterID As Integer, ByVal sDescription As String, ByVal sRemarks As String, ByVal iMasID As Integer, ByVal iIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iMasID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Desc", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = sDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "W"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Master", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iMasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = sRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CrOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Status", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = iIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spGeneralMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckDeleteorNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDesc As Integer, ByVal iMasID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from ACC_General_Master where  Mas_ID = " & iDesc & " and Mas_Master =" & iMasID & " and Mas_Delflag= 'D'"
            Return objDB.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateGeneralMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update ACC_General_Master Set Mas_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Mas_delflag='A',Mas_Status='A',Mas_AppBy= " & iUserID & ",Mas_AppOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Mas_delflag='D',Mas_Status='AD',Mas_DeletedBy= " & iUserID & ",Mas_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Mas_delflag='A',Mas_Status='AR',Mas_RecalledBy= " & iUserID & ",Mas_RecalledOn=GetDate()"
            End If
            sSql = sSql & " Where Mas_Id= " & iMasId & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function CheckGeneralMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_Id = " & iMasterID & " and Mas_CompID =  " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadForms(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from Acc_General_Forms_GST where Mas_Gen_ID =" & iMaster & " and Mas_CompID =" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPeriodicity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from Acc_General_Periodicity_GST where Mas_Gen_ID =" & iMaster & " and Mas_CompID =" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveForms(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iGenID As Integer, ByVal sFormDesc As String, ByVal iID As Integer)
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            If iID > 0 Then
                sSql = "" : sSql = "Update Acc_General_Forms_GST Set MAS_Desc='" & sFormDesc & "' Where Mas_Gen_ID=" & iGenID & " And Mas_ID=" & iID & " And Mas_CompID=" & iCompID & " "
            Else
                iMaxID = objDB.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(MAS_ID)+1,1) from Acc_General_Forms_GST")
                sSql = "" : sSql = "Insert into Acc_General_Forms_GST(MAS_ID,MAS_Gen_ID,MAS_Desc,MAS_DelFlag,MAS_CompID,MAS_Createdby,MAS_CreatedOn)"
                sSql = sSql & "Values(" & iMaxID & "," & iGenID & ",'" & sFormDesc & "','A'," & iCompID & "," & iUserID & ",GetDate())"
            End If
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePeriodicity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iGenID As Integer, ByVal sFormDesc As String, ByVal iID As Integer)
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            If iID > 0 Then
                sSql = "" : sSql = "Update Acc_General_Periodicity_GST Set MAS_Desc='" & sFormDesc & "' Where Mas_Gen_ID=" & iGenID & " And Mas_ID=" & iID & " And Mas_CompID=" & iCompID & " "
            Else
                iMaxID = objDB.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(MAS_ID)+1,1) from Acc_General_Periodicity_GST")
                sSql = "Insert into Acc_General_Periodicity_GST(MAS_ID,MAS_Gen_ID,MAS_Desc,MAS_DelFlag,MAS_CompID,MAS_Createdby,MAS_CreatedOn)"
                sSql = sSql & "Values(" & iMaxID & "," & iGenID & ",'" & sFormDesc & "','A'," & iCompID & "," & iUserID & ",GetDate())"
            End If
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFormDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select Mas_Desc from Acc_General_Forms_GST where Mas_Id =" & iMaster & " and Mas_CompID =" & iCompID & " "
            GetFormDesc = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetFormDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPeriodicityDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select Mas_Desc from Acc_General_Periodicity_GST where Mas_Id =" & iMaster & " and Mas_CompID =" & iCompID & " "
            GetPeriodicityDesc = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetPeriodicityDesc
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function insertGstCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCompanyType As String, ByVal iCompanyTypeID As Integer)
        Dim ssql As String
        Dim iCount As Integer
        Try
            ssql = "select count(*) from GSTCategory_Table where GC_CompanyTypeID = '" & iCompanyTypeID & "'"
            iCount = objDB.SQLExecuteScalar(sNameSpace, ssql)
            If (iCount = 0) Then
                ' iMAXID = objDB.SQLExecuteScalarInt(sNameSpace, "select isnull(max(GC_ID) + 1,1) from GSTCategory_Table")
                ssql = " Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER LESS THAN 20 LAKHS','UNRIGISTERED DEALER','0','IN SALE ORDER FORM THE GST RATE = 0%  BY DEFAULT','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)

                ssql = "Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER UPTO 75 LAKHS','COMPOSITION DEALER','1','IN SALE ORDER FORM THE GST RATE = 1%  BY DEFAULT','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)

                ssql = "Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER MORE THAN 20 LAKHS','RIGISTERED DEALER','HSN','IN SALE ORDER FORM THE GST RATE  REGULAR RATE must be applied by default','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)

                ssql = "Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER LESS THAN 20 LAKHS','NORMAL GST DEALER','HSN','IN SALE ORDER FORM THE GST RATE = 1%  BY DEFAULT','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)

                ssql = "Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER LESS THAN 20 LAKHS','REDUCED GST DEALER','HSN','IN SALE ORDER FORM THE GST RATE = 1%  BY DEFAULT','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)

                ssql = "Insert Into GSTCategory_Table(GC_ID, GC_MasterID, GC_CompanyTypeID, GC_CompanyType, GC_CompanyCategoryID, GC_CompanyCategory, GC_GSTCategory, GC_GSTRate, GC_SalesOrderForm, GC_GSTNForms, GC_EffectFrom, GC_EffectTo, GC_CreatedBy, GC_CreatedOn, GC_DelFlag, GC_Status, GC_YearID, GC_CompID)"
                ssql = ssql & "Values((select isnull(max(GC_ID) + 1,1) from GSTCategory_Table), 1, '" & iCompanyTypeID & "','" & iCompanyType & "',0,'AGGREGATE TURNOVER LESS THAN 20 LAKHS','RCM DEALER','0','IN SALE ORDER FORM THE GST RATE = 0%  BY DEFAULT','','2017-07-01 00:00:00.000',Null,1,GetDate(),'X','W',0,1)"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)
            Else
                ssql = "Update GSTCategory_Table set GC_CompanyType='" & iCompanyType & "' where GC_CompanyTypeID = '" & iCompanyTypeID & "'"
                objDB.SQLExecuteNonQuery(sNameSpace, ssql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
