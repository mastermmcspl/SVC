Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesOrderUpload
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As String
        Dim sSql As String = ""
        Dim sCode As String = ""
        Try
            sCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iParty & " And BM_CompID=" & iCompID & " ")
            Return sCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindStockItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try

            dtTab.Columns.Add("ItemCode")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("UnitOfMeassurement")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("Quantity")

            If sCode.StartsWith("P") Then
                sSql = "" : sSql = "Select Distinct(a.INV_ID),a.INV_Code,a.INV_Description,b.INVH_Retail As Rate,d.Mas_Desc
                                    From Inventory_Master a 
                                    Left join Inventory_Master_History b On b.INVH_INV_ID=a.INV_ID   
                                    Left join Stock_Ledger c on c.SL_HistoryID=b.INVH_ID And c.SL_ItemID=b.INVH_INV_ID
                                    Join Acc_General_Master d On d.Mas_ID=b.INVH_Unit
                                    where SL_CompiD=" & iCompID & " "
            ElseIf sCode.StartsWith("C") Then
                sSql = "" : sSql = "Select Distinct(a.INV_ID),a.INV_Code,a.INV_Description,b.INVH_MRP As Rate,d.Mas_Desc
                                    From Inventory_Master a 
                                    Left join Inventory_Master_History b On b.INVH_INV_ID=a.INV_ID   
                                    Left join Stock_Ledger c on c.SL_HistoryID=b.INVH_ID And c.SL_ItemID=b.INVH_INV_ID
                                    Join Acc_General_Master d On d.Mas_ID=b.INVH_Unit
                                    where SL_CompiD=" & iCompID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow

                    dRow("ItemCode") = dt.Rows(i)("INV_Code")
                    dRow("Goods") = dt.Rows(i)("INV_Description")
                    dRow("UnitOfMeassurement") = dt.Rows(i)("Mas_Desc")
                    dRow("Rate") = dt.Rows(i)("Rate")
                    dRow("Quantity") = ""

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sDesc & "' and Mas_Master = " & iMaster & " And MAS_CompID=" & iCompID & " "
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCityExistOrNot = dr("Mas_Id")
            Else
                CheckCityExistOrNot = 0
            End If
            dr.Close()
            Return CheckCityExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesOrderMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iPartyID As Integer)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0

        Dim iMaxOrderID As Integer = 0
        Dim sOrderCode, sPartyCode As String

        Dim iCommodityID, iItemID, iUnit, iHistoryID, iVATID, iCSTID, iExciseID, iQuantity As Integer
        Dim dRate, dDiscount, dRateAmount As Double

        Dim dDiscountAmount As Double = 0.0
        Dim dVATAmount As Double = 0.0
        Dim dCSTAmount As Double = 0.0
        Dim dExciseAmount As Double = 0.0
        Dim dNetAmount As Double = 0.0

        Dim sCode As String = ""
        Dim sArray As String()
        Dim sbret As String
        Dim iDefaultBranch As Integer
        Dim iSPO_ZoneID, iSPO_RegionID, iSPO_AreaID, iSPO_BranchID, iParent As Integer
        Try
            iDefaultBranch = GetDefaultBranch(sNameSpace, iCompID)
            iSPO_BranchID = iDefaultBranch

            iParent = getOrgParent(sNameSpace, iCompID, iDefaultBranch)
            iSPO_AreaID = iParent

            iParent = getOrgParent(sNameSpace, iCompID, iSPO_AreaID)
            iSPO_RegionID = iParent

            iParent = getOrgParent(sNameSpace, iCompID, iSPO_RegionID)
            iSPO_ZoneID = iParent

            sOrderCode = GenerateOrderCode(sNameSpace, iCompID)

            'PartyCode
            sPartyCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " ")

            sbret = sPartyCode
            sArray = sbret.Split("-")
            For i = 0 To sArray.Length - 1
                sCode = Trim(sArray(i))
                If sCode = "P" Then
                    GoTo ItemCode
                ElseIf sCode = "C" Then
                    GoTo ItemCode
                End If
            Next

            '--- Master table ---'

ItemCode:   iMaxOrderID = objGenFun.GetMaxID(sNameSpace, iCompID, "sales_ProForma_Order", "SPO_ID", "SPO_CompID")
            sSql = "" : sSql = "Insert into sales_ProForma_Order(SPO_ID,SPO_OrderCode,SPO_OrderDate,SPO_PartyCode,SPO_PartyName,"
            sSql = sSql & "SPO_Status,SPO_CompID,SPO_CreatedBy,SPO_CreatedOn,SPO_YearID,SPO_Operation,SPO_IPAddress,SPO_OrderType,SPO_DispatchFlag,SPO_SalesManID,SPO_GrandDiscount,SPO_GrandDiscountAmt,SPO_GrandTotal,SPO_GrandTotalAmt,SPO_ZoneID,SPO_RegionID,SPO_AreaID,SPO_BranchID)"
            sSql = sSql & "Values(" & iMaxOrderID & ",'" & RemoveQuote(sOrderCode) & "',GetDate(),'" & sPartyCode & "'," & iPartyID & ","
            sSql = sSql & "'A'," & iCompID & "," & iUserID & ",GetDate()," & iYearID & ",'C','" & sIPAddress & "','S',0,0,0,0,0,0," & iSPO_ZoneID & "," & iSPO_RegionID & "," & iSPO_AreaID & "," & iSPO_BranchID & ")"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            '--- Master table ---'

            'iVATID = GetVATZeroID(sNameSpace, iCompID)
            'iCSTID = GetCSTZeroID(sNameSpace, iCompID)
            'iExciseID = GetExciseZeroID(sNameSpace, iCompID)

            iVATID = 0
            iCSTID = 0
            iExciseID = 0

            For i = 0 To dtUpload.Rows.Count - 1

                '--= Detail table ---'

                iCommodityID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_Code='" & dtUpload.Rows(i)(0).ToString() & "' And INV_CompID=" & iCompID & " ")

                'Goods
                If dtUpload.Rows(i)(0).ToString() = "" Then
                    iItemID = 0
                Else
                    iItemID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INV_ID From Inventory_Master Where INV_Parent=" & iCommodityID & " And INV_Code='" & dtUpload.Rows(i)(0).ToString() & "' And INV_CompID=" & iCompID & " ")
                End If

                'UnitOfMeassurement
                If dtUpload.Rows(i)(2).ToString() = "" Then
                    iUnit = 0
                Else
                    iUnit = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(2).ToString(), 1)
                End If

                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & " ")

                'Rate
                If dtUpload.Rows(i)(3).ToString() = "" Then
                    dRate = 0.0
                Else
                    dRate = dtUpload.Rows(i)(3).ToString()
                End If

                'Quantity
                If dtUpload.Rows(i)(4).ToString() = "" Then
                    iQuantity = 0
                Else
                    iQuantity = dtUpload.Rows(i)(4).ToString()
                End If

                dRateAmount = iQuantity * dRate
                dNetAmount = iQuantity * dRate

                dDiscount = 0
                dDiscountAmount = 0

                If iQuantity > 0 Then
                    sSql = "" : sSql = "Select * from sales_ProForma_Order_Details where SPOD_SOID =" & iMaxOrderID & " and SPOD_CommodityID=" & iCommodityID & " and SPOD_ItemID=" & iItemID & " and SPOD_YearID=" & iYearID & " and SPOD_CompID=" & iCompID & " "
                    dr = objDBL.SQLDataReader(sNameSpace, sSql)
                    If dr.HasRows = True Then
                        sSql = "" : sSql = "Update sales_ProForma_Order_Details set SPOD_Quantity=" & iQuantity & ",SPOD_Discount=" & dDiscount & ",SPOD_VAT=" & iVATID & ",SPOD_CST=" & iCSTID & ",SPOD_Excise=" & iExciseID & ""
                        sSql = sSql & " where SPOD_SOID =" & iMaxOrderID & " and SPOD_CommodityID=" & iCommodityID & " and SPOD_ItemID=" & iItemID & " and SPOD_YearID=" & iYearID & " and SPOD_CompID=" & iCompID & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Else
                        iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "sales_ProForma_Order_Details", "SPOD_ID", "SPOD_CompID")
                        sSql = "" : sSql = "Insert into sales_ProForma_Order_Details(SPOD_ID,SPOD_SOID,SPOD_CommodityID,SPOD_ItemID,SPOD_UnitOfMeasurement,SPOD_HistoryID,SPOD_MRPRate,SPOD_Quantity,SPOD_Discount,SPOD_RateAmount,SPOD_DiscountRate,SPOD_TotalAmount,SPOD_Status,SPOD_CompID,SPOD_VAT,SPOD_VATAmount,SPOD_CreatedBy,SPOD_CreatedOn,SPOD_Operation,SPOD_IPAddress,SPOD_YearID,SPOD_CST,SPOD_CSTAmount,SPOD_Excise,SPOD_ExciseAmount)"
                        sSql = sSql & "Values(" & iMax & "," & iMaxOrderID & "," & iCommodityID & "," & iItemID & "," & iUnit & "," & iHistoryID & "," & dRate & "," & iQuantity & "," & dDiscount & "," & dRateAmount & "," & dDiscountAmount & "," & dNetAmount & ",'A'," & iCompID & "," & iVATID & "," & dVATAmount & "," & iUserID & ",GetDate(),'C','" & sIPAddress & "'," & iYearID & "," & iCSTID & "," & dCSTAmount & "," & iExciseID & "," & dExciseAmount & ") "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                End If

            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iNodeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Parent From Sad_Org_Structure Where Org_Node=" & iNodeID & " And Org_CompID=" & iCompID & " "
            getOrgParent = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getOrgParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node From Sad_Org_Structure Where Org_LevelCode=4 And Org_Default=1 And Org_CompID=" & iCompID & " "
            GetDefaultBranch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDefaultBranch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=14 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetVATZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATZeroID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=15 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetCSTZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetCSTZeroID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExciseZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=16 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetExciseZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetExciseZeroID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SPO_ID From Sales_Proforma_Order Order By SPO_ID Desc")
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
            sStr = "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckGoodsExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try

            sSql = "" : sSql = "Select * from Inventory_master where INV_Description ='" & sDesc & "' And INV_CompID=" & iCompID & " "
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckGoodsExistOrNot = dr("INV_Id")
            Else
                CheckGoodsExistOrNot = 0
            End If
            dr.Close()

            Return CheckGoodsExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc in(Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & ") And MAS_Master=14 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetVATID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & " "
            GetVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckItemCodeExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sItemCode As String) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try

            sSql = "" : sSql = "Select * from Inventory_master where INV_Code='" & sItemCode & "' And INV_CompID=" & iCompID & " "
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckItemCodeExistOrNot = dr("INV_Id")
            Else
                CheckItemCodeExistOrNot = 0
            End If
            dr.Close()

            Return CheckItemCodeExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
