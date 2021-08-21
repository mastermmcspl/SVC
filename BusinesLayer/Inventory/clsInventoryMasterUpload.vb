Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsInventoryMasterUpload
    Dim objDb As New DBHelper
    Dim objFasgnrl As New clsFASGeneral
    Dim objgnrl As New clsGeneralFunctions
    Public Function SaveInvenotryMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sStr As String)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iParent, iMax As Integer
        Dim iUnit As Integer = 0
        Dim iAlternative As Integer = 0
        Dim sCommidity As String = ""
        Dim sVat As String = ""
        Dim sCst As String = ""
        Dim iExistID As Integer
        Dim sExcise As String = ""
        Dim iHistoryID As Integer = 0
        Dim iPerPiece As Integer = 0
        Dim dMRP As Double = "0.0"
        Dim dRetail As Double = "0.0"
        Dim dPreDetermined As Double = "0.0"
        Dim dOthers As Double = "0.0"
        Dim dEffectiveFrom As Date
        Dim dEffectiveTo As Date

        Dim sColor As String = ""
        Dim sSize As String = ""
        Dim sACode As String = ""
        Try
            For i = 0 To dtUpload.Rows.Count - 1

                'Commodity
                If dtUpload.Rows(i)(0).ToString() = "" Then
                    dtUpload.Rows(i)(0) = sCommidity
                Else
                    sCommidity = dtUpload.Rows(i)(0).ToString()
                End If

                'VAT
                If dtUpload.Rows(i)(5).ToString() <> "" Then
                    iPerPiece = dtUpload.Rows(i)(5).ToString()
                Else
                    iPerPiece = "0"
                End If

                'VAT
                If dtUpload.Rows(i)(6).ToString() <> "" Then
                    sVat = dtUpload.Rows(i)(6).ToString()
                Else
                    sVat = ""
                End If

                'Excise
                If dtUpload.Rows(i)(7).ToString() <> "" Then
                    sExcise = dtUpload.Rows(i)(7).ToString()
                Else
                    sExcise = ""
                End If

                'sCst
                If dtUpload.Rows(i)(8).ToString() <> "" Then
                    sCst = dtUpload.Rows(i)(8).ToString()
                Else
                    sCst = ""
                End If

                'MRP
                If dtUpload.Rows(i)(9).ToString() <> "" Then
                    dMRP = Convert.ToDouble(dtUpload.Rows(i)(9).ToString())
                Else
                    dMRP = "0.0"
                End If

                'Retail
                If dtUpload.Rows(i)(10).ToString() <> "" Then
                    dRetail = Convert.ToDouble(dtUpload.Rows(i)(10).ToString())
                Else
                    dRetail = "0.0"
                End If

                'PreDetermined
                If dtUpload.Rows(i)(13).ToString() <> "" Then
                    dPreDetermined = Convert.ToDouble(dtUpload.Rows(i)(13).ToString())
                Else
                    dPreDetermined = "0.0"
                End If

                'Others
                If dtUpload.Rows(i)(14).ToString() <> "" Then
                    dOthers = Convert.ToDouble(dtUpload.Rows(i)(14).ToString())
                Else
                    dOthers = "0.0"
                End If

                'Effective From
                If dtUpload.Rows(i)(11).ToString() = "" Then
                    dEffectiveFrom = "01/01/1900"
                Else
                    If dtUpload.Rows(i)(11).ToString().Length > 10 Then
                        dEffectiveFrom = DateTime.ParseExact(dtUpload.Rows(i)(11).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        dEffectiveFrom = DateTime.ParseExact(dtUpload.Rows(i)(11).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                End If

                'Effective To
                If dtUpload.Rows(i)(12).ToString() = "" Then
                    dEffectiveTo = "01/01/1900"
                Else
                    If dtUpload.Rows(i)(12).ToString().Length > 10 Then
                        dEffectiveTo = DateTime.ParseExact(dtUpload.Rows(i)(12).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        dEffectiveTo = DateTime.ParseExact(dtUpload.Rows(i)(12).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                End If
                If dtUpload.Rows(i)(15).ToString() <> "" Then
                    sColor = dtUpload.Rows(i)(15).ToString()
                Else
                    sColor = ""
                End If

                If dtUpload.Rows(i)(16).ToString() <> "" Then
                    sSize = dtUpload.Rows(i)(16).ToString()
                Else
                    sSize = ""
                End If

                If dtUpload.Rows(i)(17).ToString() <> "" Then
                    sACode = dtUpload.Rows(i)(17).ToString()
                Else
                    sACode = ""
                End If
                'End If
                sSql = "Select * from inventory_master where Inv_Description ='" & dtUpload.Rows(i)(0).ToString() & "' and INV_CompID=" & iCompID & " and Inv_Parent=0"
                dr = objDb.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    dr.Read()
                    iParent = dr("Inv_ID")
                Else
                    iMax = objgnrl.GetMaxID(sNameSpace, iCompID, "inventory_master", "Inv_ID", "Inv_CompID")
                    sSql = "" : sSql = "Insert into inventory_master(Inv_ID,Inv_code,Inv_Description,Inv_Parent,Inv_Flag,"
                    sSql = sSql & "Inv_CreatedBy,Inv_CreatedOn,Inv_CompID,INV_Size,INV_Color,INV_Acode)Values(" & iMax & ",'" & dtUpload.Rows(i)(2).ToString() & "',"
                    sSql = sSql & " '" & dtUpload.Rows(i)(0).ToString() & "',0,'X',"
                    sSql = sSql & "" & iUserID & ",GetDate()," & iCompID & ",'" & sSize & "','" & sColor & "','" & sACode & "')"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                    iParent = iMax
                End If
                dr.Close()

                ' Inventory Master
                sSql = "" : sSql = "Select * from inventory_master where Inv_Code ='" & dtUpload.Rows(i)(2).ToString() & "' and INV_CompID=" & iCompID & " and Inv_Parent=" & iParent & ""
                dr = objDb.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    'iExistID = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select * from inventory_master where Inv_Description ='" & dtUpload.Rows(i)(1) & "' and INV_COmpID=" & iCompID & " and Inv_Parent=" & iParent & "")
                    'sSql = "" : sSql = "Update inventory_master Set Inv_code='" & dtUpload.Rows(i)(2).ToString() & "',"
                    'sSql = sSql & "Inv_Description ='" & dtUpload.Rows(i)(1).ToString() & "',Inv_Parent=" & iParent & ","
                    'sSql = sSql & "Inv_Flag ='X' "
                    'sSql = sSql & "Where INV_ID=" & iExistID & " And Inv_CompID=" & iCompID & ""
                    'DBHelper.ExecuteNoNQuery(sNameSpace, sSql)
                    'iHistoryID = iExistID
                Else
                    iMax = objgnrl.GetMaxID(sNameSpace, iCompID, "inventory_master", "Inv_ID", "Inv_CompID")
                    sSql = "" : sSql = "Insert into inventory_master(Inv_ID,Inv_code,Inv_Description,Inv_Parent,Inv_Flag,"
                    sSql = sSql & "Inv_CreatedBy,Inv_CreatedOn,Inv_CompID,INV_Size,INV_Color,INV_Acode)Values(" & iMax & ",'" & dtUpload.Rows(i)(2).ToString() & "',"
                    sSql = sSql & "'" & dtUpload.Rows(i)(1).ToString() & "'," & iParent & ",'X',"
                    sSql = sSql & "" & iUserID & ",GetDate()," & iCompID & ",'" & sSize & "','" & sColor & "','" & sACode & "')"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                    iHistoryID = iMax


                    'Inventory Master History
                    iUnit = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_Id from Acc_General_master where Mas_Desc ='" & dtUpload.Rows(i)(3).ToString() & "' and Mas_Master=1 and Mas_CompID=" & iCompID & "")
                    iAlternative = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_Id from Acc_General_master where Mas_Desc ='" & dtUpload.Rows(i)(4).ToString() & "' and Mas_Master=1 and Mas_CompID=" & iCompID & "")

                    iMax = objgnrl.GetMaxID(sNameSpace, iCompID, "inventory_Master_History", "InvH_ID", "InvH_CompID")
                    sSql = "" : sSql = "Insert into inventory_Master_History(InvH_ID,InvH_INV_ID,InvH_Flag,"
                    sSql = sSql & "InvH_Unit,InvH_AlterUnit,InvH_Excise,InvH_Vat,"
                    sSql = sSql & "InvH_CreatedBy,InvH_CreatedOn,InvH_CompID,"
                    sSql = sSql & "InvH_PerPieces,InvH_MRP,InvH_Retail,InvH_PreDeterminedPrice,"
                    sSql = sSql & "InvH_EffeFrom,InvH_EffeTo,InvH_Others,INVH_Color,INVH_Size,InvH_Cst)Values(" & iMax & "," & iHistoryID & ",'X',"
                    sSql = sSql & "" & iUnit & "," & iAlternative & ",'" & sExcise & "','" & sVat & "',"
                    sSql = sSql & "" & iUserID & ",GetDate()," & iCompID & ","
                    sSql = sSql & "" & iPerPiece & ",'" & dMRP & "','" & dRetail & "','" & dPreDetermined & "',"
                    sSql = sSql & "" & objFasgnrl.FormatDtForRDBMS(dEffectiveFrom, "I") & "," & objFasgnrl.FormatDtForRDBMS(dEffectiveTo, "I") & ",'" & dOthers & "','" & sColor & "','" & sSize & "','" & sCst & "')"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
                dr.Close()


            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckUnitOfMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnit As String) As Boolean
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select * from acc_general_master where Mas_Desc ='" & sUnit & "' and mas_Master = 1 and Mas_CompID = " & iCompID & ""
            dr = objDb.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckInventoryCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Inventory_Master where Inv_code ='" & sCode & "' and Inv_CompID = " & iCompID & ""
            dr = objDb.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                CheckInventoryCode = 1
            Else
                CheckInventoryCode = 0
            End If
            Return CheckInventoryCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFoodORNonFood(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AS_Food,AS_NonFood From Application_Settings Where AS_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("AS_Food")) = False And dt.Rows(0)("AS_Food") > 0 And (dt.Rows(0)("AS_NonFood") = 0 Or IsDBNull(dt.Rows(0)("AS_NonFood")) <> False) Then
                    sStr = "Food"
                ElseIf IsDBNull(dt.Rows(0)("AS_NonFood")) = False And dt.Rows(0)("AS_NonFood") > 0 And (IsDBNull(dt.Rows(0)("AS_Food")) <> False Or dt.Rows(0)("AS_Food") = 0) Then
                    sStr = "NonFood"
                ElseIf IsDBNull(dt.Rows(0)("AS_NonFood")) = False And dt.Rows(0)("AS_NonFood") > 0 And IsDBNull(dt.Rows(0)("AS_Food")) = False And dt.Rows(0)("AS_Food") > 0 Then
                    sStr = "BothFoodAndNonFood"
                End If
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
