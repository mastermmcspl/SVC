Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDepreciationComputation
    Private objDBL As New DatabaseLayer.DBHelper
    Public objFAS As New clsFASGeneral


    Private iADep_ID As Integer
    Private iADep_Asset_MasterID As Integer
    Private iADep_AssetID As Integer
    Private sADep_Description As String
    Private dADep_AssetAge As Double
    Private iADep_Quantity As Integer
    Private dADep_CommissionDate As DateTime
    Private dADep_PurchaseAmount As Double
    Private dADep_Depreciation_rate As Double
    Private iADep_NoOfDays As Integer
    Private dADep_Depreciationfor_theyear As Double
    Private dADep_YTD As Double
    Private dADep_WDV As Double
    Private dADep_ResidualValue As Double
    Private iADep_CreatedBy As Integer
    Private dADep_CreatedOn As DateTime
    Private iADep_UpdatedBy As Integer
    Private dADep_UpdatedOn As DateTime
    Private sADep_DelFlag As String
    Private sADep_Status As String
    Private iADep_YearID As Integer
    Private iADep_CompID As Integer
    Private sADep_Opeartion As String
    Private sADep_IPAddress As String

    Private iAIT_ID As Integer
    Private iAIT_Asset_MasterID As Integer
    Private iAIT_AssetID As Integer
    Private sAIT_Description As String
    Private dAIT_AssetAge As Double
    Private iAIT_Quantity As Integer
    Private dAIT_CommissionDate As DateTime
    Private dAIT_PurchaseAmount As Double
    Private dAIT_IncomeTax_rate As Double
    Private iAIT_NoOfDays As Integer
    Private dAIT_IncomeTaxfor_theyear As Double
    Private dAIT_YTD As Double
    Private dAIT_WDV As Double
    Private dAIT_ResidualValue As Double
    Private iAIT_CreatedBy As Integer
    Private dAIT_CreatedOn As DateTime
    Private iAIT_UpdatedBy As Integer
    Private dAIT_UpdatedOn As DateTime
    Private sAIT_DelFlag As String
    Private sAIT_Status As String
    Private iAIT_YearID As Integer
    Private iAIT_CompID As Integer
    Private sAIT_Opeartion As String
    Private sAIT_IPAddress As String

    Public Property AIT_ID() As Integer
        Get
            Return (iAIT_ID)
        End Get
        Set(ByVal Value As Integer)
            iAIT_ID = Value
        End Set
    End Property
    Public Property AIT_Asset_MasterID() As Integer
        Get
            Return (iAIT_Asset_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iAIT_Asset_MasterID = Value
        End Set
    End Property
    Public Property AIT_AssetID() As Integer
        Get
            Return (iAIT_AssetID)
        End Get
        Set(ByVal Value As Integer)
            iAIT_AssetID = Value
        End Set
    End Property
    Public Property AIT_Description() As String
        Get
            Return (sAIT_Description)
        End Get
        Set(ByVal Value As String)
            sAIT_Description = Value
        End Set
    End Property
    Public Property AIT_AssetAge() As Double
        Get
            Return (dAIT_AssetAge)
        End Get
        Set(ByVal Value As Double)
            dAIT_AssetAge = Value
        End Set
    End Property
    Public Property AIT_CommissionDate() As DateTime
        Get
            Return (dAIT_CommissionDate)
        End Get
        Set(ByVal Value As DateTime)
            dAIT_CommissionDate = Value
        End Set
    End Property
    Public Property AIT_Quantity() As Integer
        Get
            Return (iAIT_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iAIT_Quantity = Value
        End Set
    End Property
    Public Property AIT_PurchaseAmount() As Double
        Get
            Return (dAIT_PurchaseAmount)
        End Get
        Set(ByVal Value As Double)
            dAIT_PurchaseAmount = Value
        End Set
    End Property
    Public Property AIT_IncomeTax_rate() As Double
        Get
            Return (dAIT_IncomeTax_rate)
        End Get
        Set(ByVal Value As Double)
            dAIT_IncomeTax_rate = Value
        End Set
    End Property
    Public Property AIT_NoOfDays() As Integer
        Get
            Return (iAIT_NoOfDays)
        End Get
        Set(ByVal Value As Integer)
            iAIT_NoOfDays = Value
        End Set
    End Property
    Public Property AIT_IncomeTaxfor_theyear() As Double
        Get
            Return (dAIT_IncomeTaxfor_theyear)
        End Get
        Set(ByVal Value As Double)
            dAIT_IncomeTaxfor_theyear = Value
        End Set
    End Property

    Public Property AIT_YTD() As Double
        Get
            Return (dAIT_YTD)
        End Get
        Set(ByVal Value As Double)
            dAIT_YTD = Value
        End Set
    End Property

    Public Property AIT_WDV() As Double
        Get
            Return (dAIT_WDV)
        End Get
        Set(ByVal Value As Double)
            dAIT_WDV = Value
        End Set
    End Property
    Public Property AIT_ResidualValue() As Double
        Get
            Return (dAIT_ResidualValue)
        End Get
        Set(ByVal Value As Double)
            dAIT_ResidualValue = Value
        End Set
    End Property

    Public Property AIT_CreatedBy() As Integer
        Get
            Return (iAIT_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAIT_CreatedBy = Value
        End Set
    End Property
    Public Property AIT_CreatedOn() As DateTime
        Get
            Return (dAIT_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAIT_CreatedOn = Value
        End Set
    End Property
    Public Property AIT_UpdatedBy() As Integer
        Get
            Return (iAIT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAIT_UpdatedBy = Value
        End Set
    End Property
    Public Property AIT_UpdatedOn() As DateTime
        Get
            Return (dAIT_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAIT_UpdatedOn = Value
        End Set
    End Property
    Public Property AIT_DelFlag() As String
        Get
            Return (sAIT_DelFlag)
        End Get
        Set(ByVal Value As String)
            sAIT_DelFlag = Value
        End Set
    End Property
    Public Property AIT_Status() As String
        Get
            Return (sAIT_Status)
        End Get
        Set(ByVal Value As String)
            sAIT_Status = Value
        End Set
    End Property
    Public Property AIT_YearID() As Integer
        Get
            Return (iAIT_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAIT_YearID = Value
        End Set
    End Property

    Public Property AIT_CompID() As Integer
        Get
            Return (iAIT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAIT_CompID = Value
        End Set
    End Property
    Public Property AIT_Opeartion() As String
        Get
            Return (sAIT_Opeartion)
        End Get
        Set(ByVal Value As String)
            sAIT_Opeartion = Value
        End Set
    End Property

    Public Property AIT_IPAddress() As String
        Get
            Return (sAIT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAIT_IPAddress = Value
        End Set
    End Property


    Public Property ADep_ID() As Integer
        Get
            Return (iADep_ID)
        End Get
        Set(ByVal Value As Integer)
            iADep_ID = Value
        End Set
    End Property
    Public Property ADep_Asset_MasterID() As Integer
        Get
            Return (iADep_Asset_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iADep_Asset_MasterID = Value
        End Set
    End Property
    Public Property ADep_AssetID() As Integer
        Get
            Return (iADep_AssetID)
        End Get
        Set(ByVal Value As Integer)
            iADep_AssetID = Value
        End Set
    End Property
    Public Property ADep_Description() As String
        Get
            Return (sADep_Description)
        End Get
        Set(ByVal Value As String)
            sADep_Description = Value
        End Set
    End Property
    Public Property ADep_AssetAge() As Double
        Get
            Return (dADep_AssetAge)
        End Get
        Set(ByVal Value As Double)
            dADep_AssetAge = Value
        End Set
    End Property
    Public Property ADep_CommissionDate() As DateTime
        Get
            Return (dADep_CommissionDate)
        End Get
        Set(ByVal Value As DateTime)
            dADep_CommissionDate = Value
        End Set
    End Property
    Public Property ADep_Quantity() As Integer
        Get
            Return (iADep_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iADep_Quantity = Value
        End Set
    End Property
    Public Property ADep_PurchaseAmount() As Double
        Get
            Return (dADep_PurchaseAmount)
        End Get
        Set(ByVal Value As Double)
            dADep_PurchaseAmount = Value
        End Set
    End Property
    Public Property ADep_Depreciation_rate() As Double
        Get
            Return (dADep_Depreciation_rate)
        End Get
        Set(ByVal Value As Double)
            dADep_Depreciation_rate = Value
        End Set
    End Property
    Public Property ADep_NoOfDays() As Integer
        Get
            Return (iADep_NoOfDays)
        End Get
        Set(ByVal Value As Integer)
            iADep_NoOfDays = Value
        End Set
    End Property
    Public Property ADep_Depreciationfor_theyear() As Double
        Get
            Return (dADep_Depreciationfor_theyear)
        End Get
        Set(ByVal Value As Double)
            dADep_Depreciationfor_theyear = Value
        End Set
    End Property

    Public Property ADep_YTD() As Double
        Get
            Return (dADep_YTD)
        End Get
        Set(ByVal Value As Double)
            dADep_YTD = Value
        End Set
    End Property

    Public Property ADep_WDV() As Double
        Get
            Return (dADep_WDV)
        End Get
        Set(ByVal Value As Double)
            dADep_WDV = Value
        End Set
    End Property
    Public Property ADep_ResidualValue() As Double
        Get
            Return (dADep_ResidualValue)
        End Get
        Set(ByVal Value As Double)
            dADep_ResidualValue = Value
        End Set
    End Property

    Public Property ADep_CreatedBy() As Integer
        Get
            Return (iADep_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iADep_CreatedBy = Value
        End Set
    End Property
    Public Property ADep_CreatedOn() As DateTime
        Get
            Return (dADep_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dADep_CreatedOn = Value
        End Set
    End Property
    Public Property ADep_UpdatedBy() As Integer
        Get
            Return (iADep_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iADep_UpdatedBy = Value
        End Set
    End Property
    Public Property ADep_UpdatedOn() As DateTime
        Get
            Return (dADep_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dADep_UpdatedOn = Value
        End Set
    End Property
    Public Property ADep_DelFlag() As String
        Get
            Return (sADep_DelFlag)
        End Get
        Set(ByVal Value As String)
            sADep_DelFlag = Value
        End Set
    End Property
    Public Property ADep_Status() As String
        Get
            Return (sADep_Status)
        End Get
        Set(ByVal Value As String)
            sADep_Status = Value
        End Set
    End Property
    Public Property ADep_YearID() As Integer
        Get
            Return (iADep_YearID)
        End Get
        Set(ByVal Value As Integer)
            iADep_YearID = Value
        End Set
    End Property

    Public Property ADep_CompID() As Integer
        Get
            Return (iADep_CompID)
        End Get
        Set(ByVal Value As Integer)
            iADep_CompID = Value
        End Set
    End Property
    Public Property ADep_Opeartion() As String
        Get
            Return (sADep_Opeartion)
        End Get
        Set(ByVal Value As String)
            sADep_Opeartion = Value
        End Set
    End Property

    Public Property ADep_IPAddress() As String
        Get
            Return (sADep_IPAddress)
        End Get
        Set(ByVal Value As String)
            sADep_IPAddress = Value
        End Set
    End Property
    Public Function LoadDepreciationComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Try

            dt.Columns.Add("AssetMasterPKID")
            dt.Columns.Add("AssetTypeID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DepreciationRate")
            dt.Columns.Add("OrignalCoast")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("DepYear")
            dt.Columns.Add("YTDDep")
            dt.Columns.Add("wrtnvalue")
            dt.Columns.Add("Rsdulvalue")
            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_YearID ='" & iYearId & "' order by AFAM_AssetType asc"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow

                    dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
                    dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")

                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select GL_Desc From Chart_Of_Accounts Where GL_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and gl_CompId=" & iCompID & "")
                    'objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_CompID=" & iCompID & " ")
                    dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                        dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    End If
                    dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

                    'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")

                    dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_Deprate From Acc_AssetMaster Where AM_AssetID=" & dt1.Rows(i)("AFAM_AssetType") & "  And AM_CompID=" & iCompID & "")

                    dr("OrignalCoast") = dt1.Rows(i)("AFAM_PurchaseAmount")
                    dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    dr("NoOfDays") = ""
                    dr("DepYear") = ""
                    dr("YTDDep") = ""
                    dr("wrtnvalue") = ""
                    dr("Rsdulvalue") = ""
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItRateComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Try

            dt.Columns.Add("AssetMasterPKID")
            dt.Columns.Add("AssetTypeID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("ItRate")
            dt.Columns.Add("OrignalCoast")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("DepYear")
            dt.Columns.Add("YTDDep")
            dt.Columns.Add("wrtnvalue")
            dt.Columns.Add("Rsdulvalue")
            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_YearID='" & iYearid & "' order by AFAM_AssetType asc"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow

                    dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
                    dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")

                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select GL_Desc From Chart_Of_Accounts Where GL_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and gl_CompId=" & iCompID & "")
                    'objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_CompID=" & iCompID & " ")
                    dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                        dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    End If
                    dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

                    'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")

                    dr("ItRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_Itrate From Acc_AssetMaster Where AM_AssetID=" & dt1.Rows(i)("AFAM_AssetType") & "  And AM_CompID=" & iCompID & "")

                    dr("OrignalCoast") = dt1.Rows(i)("AFAM_PurchaseAmount")
                    dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    dr("NoOfDays") = ""
                    dr("DepYear") = ""
                    dr("YTDDep") = ""
                    dr("wrtnvalue") = ""
                    dr("Rsdulvalue") = ""
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDepreciationComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sErrortext As String) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Dim ToDate As Date
        Try
            dt.Columns.Add("AssetMasterPKID")
            dt.Columns.Add("AssetTypeID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DepreciationRate")
            dt.Columns.Add("OrignalCoast")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("DepYear")
            dt.Columns.Add("YTDDep")
            dt.Columns.Add("wrtnvalue")
            dt.Columns.Add("Rsdulvalue")

            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_YearID ='" & iYearID & "' order by AFAM_AssetType asc"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow

                    dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
                    dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")
                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select GL_Desc From Chart_Of_Accounts Where GL_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and gl_CompId=" & iCompID & "")
                    dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                        dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    End If
                    dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

                    'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")


                    dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_Deprate From Acc_AssetMaster Where AM_AssetID=" & dt1.Rows(i)("AFAM_AssetType") & " And AM_CompID=" & iCompID & "")

                    dr("OrignalCoast") = dt1.Rows(i)("AFAM_PurchaseAmount")
                    dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iYearID & "")


                    Dim Fromdate As Date
                    Fromdate = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")

                    Dim diff As Integer = DateDiffComputation(sNameSpace, iCompID, Fromdate, ToDate)

                    dr("NoOfDays") = diff

                    dr("DepYear") = String.Format("{0:0.00}", Convert.ToDecimal(((dr("DepreciationRate") * dr("OrignalCoast") / 100) * dr("NoOfDays") / 365)))
                    dr("YTDDep") = String.Format("{0:0.00}", Convert.ToDecimal(dr("DepYear")))
                    dr("wrtnvalue") = String.Format("{0:0.00}", Convert.ToDecimal(dr("OrignalCoast") - dr("YTDDep")))
                    dr("Rsdulvalue") = dr("OrignalCoast") * 5 / 100
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateItRateComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sErrortext As String) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Dim ToDate As Date
        Try
            dt.Columns.Add("AssetMasterPKID")
            dt.Columns.Add("AssetTypeID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("ItRate")
            dt.Columns.Add("OrignalCoast")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("DepYear")
            dt.Columns.Add("YTDDep")
            dt.Columns.Add("wrtnvalue")
            dt.Columns.Add("Rsdulvalue")

            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_YearID='" & iYearID & "'  order by AFAM_AssetType asc"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow

                    dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
                    dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")
                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select GL_Desc From Chart_Of_Accounts Where GL_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and gl_CompId=" & iCompID & "")
                    dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                        dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    End If
                    dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

                    'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")


                    dr("ItRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_AssetID=" & dt1.Rows(i)("AFAM_AssetType") & " And AM_CompID=" & iCompID & "")

                    dr("OrignalCoast") = dt1.Rows(i)("AFAM_PurchaseAmount")
                    dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iYearID & "")


                    Dim Fromdate As Date
                    Fromdate = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")

                    Dim diff As Integer = DateDiffComputation(sNameSpace, iCompID, Fromdate, ToDate)

                    dr("NoOfDays") = diff

                    dr("DepYear") = String.Format("{0:0.00}", Convert.ToDecimal(((dr("ItRate") * dr("OrignalCoast") / 100) * dr("NoOfDays") / 365)))
                    dr("YTDDep") = String.Format("{0:0.00}", Convert.ToDecimal(dr("DepYear")))
                    dr("wrtnvalue") = String.Format("{0:0.00}", Convert.ToDecimal(dr("OrignalCoast") - dr("YTDDep")))
                    dr("Rsdulvalue") = dr("OrignalCoast") * 5 / 100
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DateDiffComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dFrmDt As Date, ByVal dTodt As Date) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SELECT DATEDIFF(day, '" & objFAS.FormatDtForRDBMS(dFrmDt, "CT") & "','" & objFAS.FormatDtForRDBMS(dTodt, "CT") & "')"
            DateDiffComputation = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return DateDiffComputation
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDepreciationComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objDepComp As ClsDepreciationComputation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Asset_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_Asset_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_AssetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_AssetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDepComp.ADep_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_AssetAge", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.ADep_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_PurchaseAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_PurchaseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Depreciation_rate", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_Depreciation_rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_NoOfDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_NoOfDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Depreciationfor_theyear", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_Depreciationfor_theyear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_YTD", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_YTD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_WDV", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_WDV
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ResidualValue", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_ResidualValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.ADep_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.ADep_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDepComp.ADep_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objDepComp.ADep_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.ADep_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDepComp.ADep_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDepComp.ADep_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AsetDepreciation", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveIncomeTaxComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objItComp As ClsDepreciationComputation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Asset_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_Asset_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AssetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_AssetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objItComp.AIT_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AssetAge", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objItComp.AIT_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_PurchaseAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_PurchaseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IncomeTax_rate", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_IncomeTax_rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_NoOfDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_NoOfDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IncomeTaxfor_theyear", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_IncomeTaxfor_theyear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_YTD", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_YTD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_WDV", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_WDV
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ResidualValue", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_ResidualValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objItComp.AIT_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objItComp.AIT_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objItComp.AIT_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objItComp.AIT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objItComp.AIT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objItComp.AIT_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objItComp.AIT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetIncomeTaxRate", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkingmasterid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal objDepComp As ClsDepreciationComputation) As String
        Dim AssetID As New Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            sSql = "Select ADep_Asset_MasterID From Acc_AsetDepreciation Where ADep_CompID=" & iCompID & " and  ADep_AssetID='" & AssetID & "'"
            checkingmasterid = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return checkingmasterid
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkAssetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal objDepComp As ClsDepreciationComputation) As String
        Dim sSql As String = ""
        Dim AssetID As New Integer
        Dim retunID As String = ""
        Try

            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            sSql = "select ADep_AssetID from Acc_AsetDepreciation where ADep_AssetID=" & AssetID & " and ADep_CompID=" & iCompID & ""
            checkAssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return checkAssetID

            'sSql = "select ADep_AssetID from Acc_AsetDepreciation where ADep_AssetID=" & AssetID & " and ADep_CompID=" & iCompID & ""
            'retunID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            'sSql = "select AFAM_AssetCode from Acc_FixedAssetMaster where AFAM_AssetType=" & retunID & " and AFAM_CompID=" & iCompID & ""
            'checkAssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function FixedAssetSetting(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iddlMethod As Integer)
        Dim sSql As String = ""
        Dim iHeadID As Integer = 0
        Dim bcheck As Boolean

        Try
            bcheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Application_Settings where AS_CompID='" & iCompID & "' ")
            If bcheck = True Then
                sSql = "" : sSql = "update Application_Settings set AS_DepMethod=" & iddlMethod & " where AS_CompID='" & iCompID & "'"
            Else
            End If
            'sSql = "" : sSql = "insert into Application_Settings (AS_DepMethod,AS_CompID) values( " & iddlMethod & ",'" & iCompID & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFixedAsesetSetting(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "select AS_DepMethod from  Application_Settings where AS_CompID='" & iCompID & "'"
            LoadFixedAsesetSetting = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return LoadFixedAsesetSetting
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
