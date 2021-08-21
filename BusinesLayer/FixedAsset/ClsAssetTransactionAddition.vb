﻿Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Security.Cryptography
Public Class ClsAssetTransactionAddition
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private AFAA_ID As Integer
    Private AFAA_AssetTrType As Integer
    Private AFAA_CurrencyType As Integer
    Private AFAA_CurrencyAmnt As Double
    Private AFAA_Zone As Integer
    Private AFAA_Region As Integer
    Private AFAA_Area As Integer
    Private AFAA_Branch As Integer
    Private AFAA_ActualLocn As String
    Private AFAA_SupplierName As Integer
    Private AFAA_SupplierCode As Integer
    Private AFAA_TrType As Integer
    Private AFAA_AssetType As String
    Private AFAA_AssetNo As String
    Private AFAA_AssetRefNo As String
    Private AFAA_Description As String
    Private AFAA_ItemCode As String
    Private AFAA_ItemDescription As String
    Private AFAA_Quantity As Integer
    Private AFAA_CommissionDate As Date
    Private AFAA_PurchaseDate As Date
    Private AFAA_AssetAge As Double
    Private AFAA_AssetAmount As Double
    Private AFAA_AssetDelID As Integer
    Private AFAA_AssetDelDate As Date
    Private AFAA_AssetDeletionDate As Date
    Private AFAA_Assetvalue As Double
    Private AFAA_AssetDesc As String
    Private AFAA_CreatedBy As Integer
    Private AFAA_CreatedOn As Date
    Private AFAA_UpdatedBy As Integer
    Private AFAA_UpdatedOn As Date
    Private AFAA_ApprovedBy As Integer
    Private AFAA_ApprovedOn As Date
    Private AFAA_Deletedby As Integer
    Private AFAA_DeletedOn As Date
    Private AFAA_Status As String
    Private AFAA_Delflag As String
    Private AFAA_YearID As Integer
    Private AFAA_CompID As Integer
    Private AFAA_Operation As String
    Private AFAA_IPAddress As String
    Private AFAA_AddnType As String
    Private AFAA_DelnType As String
    Private AFAA_Depreciation As Double
    Private AFAA_AddtnDate As Date

    Private iPGE_BASENAME As Integer
    Private iPGE_CABINET As Integer
    Private iPGE_FOLDER As Integer
    Private iPGE_DOCUMENT_TYPE As Integer
    Private sPGE_TITLE As String
    Private dPGE_DATE As DateTime
    Private iPge_DETAILS_ID As Integer
    Private iPge_CreatedBy As Integer
    Private sPGE_OBJECT As String
    Private iPGE_PAGENO As Integer
    Private sPGE_EXT As String
    Private sPGE_KeyWORD As String
    Private sPGE_OCRText As String
    Private iPGE_SIZE As Integer
    Private iPGE_CURRENT_VER As Integer
    Private sPGE_STATUS As String
    Private iPGE_SubCabinet As Integer
    Private iPge_UpdatedBy As Integer
    Private iPGE_QC_UsrGrpId As Integer
    Private sPGE_FTPStatus As String
    Private iPGE_batch_name As Integer
    Private spge_OrignalFileName As String
    Private iPGE_BatchID As Integer
    Private iPGE_OCRDelFlag As Integer
    Private iPge_CompID As Integer
    Private spge_Delflag As String

    Private FXATD_ID As Integer
    Private FXATD_TransactionDate As Date
    Private FXATD_TrType As Integer
    Private FXATD_BillId As Integer
    Private FXATD_PaymentType As Integer
    Private FXATD_DbOrCr As Integer
    Private FXATD_Head As Integer
    Private FXATD_GL As Integer
    Private FXATD_SubGL As Integer
    Private FXATD_Debit As Decimal
    Private FXATD_Credit As Decimal
    Private FXATD_CreatedOn As Date
    Private FXATD_CreatedBy As Integer
    Private FXATD_ApprovedBy As Integer
    Private FXATD_ApprovedOn As Date
    Private FXATD_Deletedby As Integer
    Private FXATD_DeletedOn As Date
    Private FXATD_Status As String
    Private FXATD_YearID As Integer
    Private FXATD_Operation As String
    Private FXATD_CompID As Integer
    Private FXATD_IPAddress As String

    Public Property dAFAA_AddtnDate() As Date
        Get
            Return (AFAA_AddtnDate)
        End Get
        Set(ByVal Value As Date)
            AFAA_AddtnDate = Value
        End Set
    End Property
    Public Property dAFAA_Depreciation() As Double
        Get
            Return (AFAA_Depreciation)
        End Get
        Set(ByVal Value As Double)
            AFAA_Depreciation = Value
        End Set
    End Property
    Public Property sAFAA_DelnType() As String
        Get
            Return (AFAA_DelnType)
        End Get
        Set(ByVal Value As String)
            AFAA_DelnType = Value
        End Set
    End Property
    Public Property sAFAA_AddnType() As String
        Get
            Return (AFAA_AddnType)
        End Get
        Set(ByVal Value As String)
            AFAA_AddnType = Value
        End Set
    End Property
    Public Property sFXATD_IPAddress() As String
        Get
            Return (FXATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            FXATD_IPAddress = Value
        End Set
    End Property
    Public Property sFXATD_Operation() As String
        Get
            Return (FXATD_Operation)
        End Get
        Set(ByVal Value As String)
            FXATD_Operation = Value
        End Set
    End Property
    Public Property iFXATD_YearID() As Integer
        Get
            Return (FXATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_YearID = Value
        End Set
    End Property
    Public Property sFXATD_Status() As String
        Get
            Return (FXATD_Status)
        End Get
        Set(ByVal Value As String)
            FXATD_Status = Value
        End Set
    End Property
    Public Property dFXATD_DeletedOn() As Date
        Get
            Return (FXATD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_DeletedOn = Value
        End Set
    End Property
    Public Property iFXATD_Deletedby() As Integer
        Get
            Return (FXATD_Deletedby)
        End Get
        Set(ByVal Value As Integer)
            FXATD_Deletedby = Value
        End Set
    End Property
    Public Property dFXATD_ApprovedOn() As Date
        Get
            Return (FXATD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_ApprovedOn = Value
        End Set
    End Property
    Public Property iFXATD_ApprovedBy() As Integer
        Get
            Return (FXATD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            FXATD_ApprovedBy = Value
        End Set
    End Property
    Public Property dFXATD_CreatedOn() As Date
        Get
            Return (FXATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            FXATD_CreatedOn = Value
        End Set
    End Property
    Public Property iFXATD_CreatedBy() As Integer
        Get
            Return (FXATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            FXATD_CreatedBy = Value
        End Set
    End Property
    Public Property dFXATD_Credit() As Decimal
        Get
            Return (FXATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            FXATD_Credit = Value
        End Set
    End Property
    Public Property dFXATD_Debit() As Decimal
        Get
            Return (FXATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            FXATD_Debit = Value
        End Set
    End Property
    Public Property iFXATD_SubGL() As Integer
        Get
            Return (FXATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            FXATD_SubGL = Value
        End Set
    End Property
    Public Property iFXATD_GL() As Integer
        Get
            Return (FXATD_GL)
        End Get
        Set(ByVal Value As Integer)
            FXATD_GL = Value
        End Set
    End Property
    Public Property iFXATD_Head() As Integer
        Get
            Return (FXATD_Head)
        End Get
        Set(ByVal Value As Integer)
            FXATD_Head = Value
        End Set
    End Property
    Public Property iFXATD_CompID() As Integer
        Get
            Return (FXATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_CompID = Value
        End Set
    End Property
    Public Property iFXATD_DbOrCr() As Integer
        Get
            Return (FXATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            FXATD_DbOrCr = Value
        End Set
    End Property
    Public Property iFXATD_PaymentType() As Integer
        Get
            Return (FXATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            FXATD_PaymentType = Value
        End Set
    End Property
    Public Property iFXATD_BillId() As Integer
        Get
            Return (FXATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            FXATD_BillId = Value
        End Set
    End Property
    Public Property iFXATD_TrType() As Integer
        Get
            Return (FXATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            FXATD_TrType = Value
        End Set
    End Property
    Public Property dFXATD_TransactionDate() As Date
        Get
            Return (FXATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            FXATD_TransactionDate = Value
        End Set
    End Property
    Public Property iFXATD_ID() As Integer
        Get
            Return (FXATD_ID)
        End Get
        Set(ByVal Value As Integer)
            FXATD_ID = Value
        End Set
    End Property



    Public Property iPGECABINET() As Integer
        Get
            Return (iPGE_CABINET)
        End Get
        Set(ByVal Value As Integer)
            iPGE_CABINET = Value
        End Set
    End Property

    Public Property iPGEBASENAME() As Integer
        Get
            Return (iPGE_BASENAME)
        End Get
        Set(ByVal Value As Integer)
            iPGE_BASENAME = Value
        End Set
    End Property
    Public Property iPGEFOLDER() As Integer
        Get
            Return (iPGE_FOLDER)
        End Get
        Set(ByVal Value As Integer)
            iPGE_FOLDER = Value
        End Set
    End Property

    Public Property iPGEDOCUMENTTYPE() As Integer
        Get
            Return (iPGE_DOCUMENT_TYPE)
        End Get
        Set(ByVal Value As Integer)
            iPGE_DOCUMENT_TYPE = Value
        End Set
    End Property
    Public Property sPGETITLE() As String
        Get
            Return (sPGE_TITLE)
        End Get
        Set(ByVal Value As String)
            sPGE_TITLE = Value
        End Set
    End Property
    Public Property spgeDelflag() As String
        Get
            Return (spge_Delflag)
        End Get
        Set(ByVal Value As String)
            spge_Delflag = Value
        End Set
    End Property
    Public Property dPGEDATE() As Date
        Get
            Return (dPGE_DATE)
        End Get
        Set(ByVal Value As Date)
            dPGE_DATE = Value
        End Set
    End Property
    Public Property iPgeDETAILSID() As Integer
        Get
            Return (iPge_DETAILS_ID)
        End Get
        Set(ByVal Value As Integer)
            iPge_DETAILS_ID = Value
        End Set
    End Property
    Public Property sPGEOBJECT() As String
        Get
            Return (sPGE_OBJECT)
        End Get
        Set(ByVal Value As String)
            sPGE_OBJECT = Value
        End Set
    End Property
    Public Property iPgeCreatedBy() As Integer
        Get
            Return (iPge_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPge_CreatedBy = Value
        End Set
    End Property
    Public Property iPGEPAGENO() As Integer
        Get
            Return (iPGE_PAGENO)
        End Get
        Set(ByVal Value As Integer)
            iPGE_PAGENO = Value
        End Set
    End Property
    Public Property sPGEEXT() As String
        Get
            Return (sPGE_EXT)
        End Get
        Set(ByVal Value As String)
            sPGE_EXT = Value
        End Set
    End Property
    Public Property sPGEKeyWORD() As String
        Get
            Return (sPGE_KeyWORD)
        End Get
        Set(ByVal Value As String)
            sPGE_KeyWORD = Value
        End Set
    End Property
    Public Property sPGEOCRText() As String
        Get
            Return (sPGE_OCRText)
        End Get
        Set(ByVal Value As String)
            sPGE_OCRText = Value
        End Set
    End Property
    Public Property iPGESIZE() As Integer
        Get
            Return (iPGE_SIZE)
        End Get
        Set(ByVal Value As Integer)
            iPGE_SIZE = Value
        End Set
    End Property
    Public Property iPGECURRENT_VER() As Integer
        Get
            Return (iPGE_CURRENT_VER)
        End Get
        Set(ByVal Value As Integer)
            iPGE_CURRENT_VER = Value
        End Set
    End Property
    Public Property sPGESTATUS() As String
        Get
            Return (sPGE_STATUS)
        End Get
        Set(ByVal Value As String)
            sPGE_STATUS = Value
        End Set
    End Property
    Public Property iPGESubCabinet() As Integer
        Get
            Return (iPGE_SubCabinet)
        End Get
        Set(ByVal Value As Integer)
            iPGE_SubCabinet = Value
        End Set
    End Property
    Public Property iPgeUpdatedBy() As Integer
        Get
            Return (iPge_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPge_UpdatedBy = Value
        End Set
    End Property
    Public Property iPGEQCUsrGrpId() As Integer
        Get
            Return (iPGE_QC_UsrGrpId)
        End Get
        Set(ByVal Value As Integer)
            iPGE_QC_UsrGrpId = Value
        End Set
    End Property
    Public Property sPGEFTPStatus() As String
        Get
            Return (sPGE_FTPStatus)
        End Get
        Set(ByVal Value As String)
            sPGE_FTPStatus = Value
        End Set
    End Property
    Public Property iPGEbatchname() As Integer
        Get
            Return (iPGE_batch_name)
        End Get
        Set(ByVal Value As Integer)
            iPGE_batch_name = Value
        End Set
    End Property
    Public Property spgeOrignalFileName() As String
        Get
            Return (spge_OrignalFileName)
        End Get
        Set(ByVal Value As String)
            spge_OrignalFileName = Value
        End Set
    End Property
    Public Property iPGEBatchID() As Integer
        Get
            Return (iPGE_BatchID)
        End Get
        Set(ByVal Value As Integer)
            iPGE_BatchID = Value
        End Set
    End Property
    Public Property iPGEOCRDelFlag() As Integer
        Get
            Return (iPGE_OCRDelFlag)
        End Get
        Set(ByVal Value As Integer)
            iPGE_OCRDelFlag = Value
        End Set
    End Property
    Public Property iPgeCompID() As Integer
        Get
            Return (iPge_CompID)
        End Get
        Set(ByVal Value As Integer)
            iPge_CompID = Value
        End Set
    End Property


    Public Property iAFAA_ID() As Integer
        Get
            Return AFAA_ID
        End Get
        Set(ByVal value As Integer)
            AFAA_ID = value
        End Set
    End Property
    Public Property iAFAA_AssetTrType() As Integer
        Get
            Return AFAA_AssetTrType
        End Get
        Set(value As Integer)
            AFAA_AssetTrType = value
        End Set
    End Property
    Public Property dAFAA_CurrencyAmnt() As Double
        Get
            Return AFAA_CurrencyAmnt
        End Get
        Set(value As Double)
            AFAA_CurrencyAmnt = value
        End Set
    End Property
    Public Property iAFAA_Zone() As Integer
        Get
            Return AFAA_Zone
        End Get
        Set(value As Integer)
            AFAA_Zone = value
        End Set
    End Property
    Public Property iAFAA_Region() As Integer
        Get
            Return AFAA_Region
        End Get
        Set(value As Integer)
            AFAA_Region = value
        End Set
    End Property
    Public Property iAFAA_Area() As Integer
        Get
            Return AFAA_Area
        End Get
        Set(value As Integer)
            AFAA_Area = value
        End Set
    End Property
    Public Property iAFAA_Branch() As Integer
        Get
            Return AFAA_Branch
        End Get
        Set(value As Integer)
            AFAA_Branch = value
        End Set
    End Property
    Public Property sAFAA_ActualLocn() As String
        Get
            Return AFAA_ActualLocn
        End Get
        Set(value As String)
            AFAA_ActualLocn = value
        End Set
    End Property
    Public Property iAFAA_SupplierName() As Integer
        Get
            Return AFAA_SupplierName
        End Get
        Set(value As Integer)
            AFAA_SupplierName = value
        End Set
    End Property
    Public Property iAFAA_SupplierCode() As Integer
        Get
            Return AFAA_SupplierCode
        End Get
        Set(value As Integer)
            AFAA_SupplierCode = value
        End Set
    End Property
    Public Property iAFAA_CurrencyType() As Integer
        Get
            Return AFAA_CurrencyType
        End Get
        Set(value As Integer)
            AFAA_CurrencyType = value
        End Set
    End Property
    Public Property iAFAA_TrType() As Integer
        Get
            Return AFAA_TrType
        End Get
        Set(ByVal value As Integer)
            AFAA_TrType = value
        End Set
    End Property
    Public Property sAFAA_AssetType() As String
        Get
            Return AFAA_AssetType
        End Get
        Set(ByVal value As String)
            AFAA_AssetType = value
        End Set
    End Property
    Public Property sAFAA_AssetNo() As String
        Get
            Return AFAA_AssetNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetNo = value
        End Set
    End Property
    Public Property sAFAA_AssetRefNo() As String
        Get
            Return AFAA_AssetRefNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetRefNo = value
        End Set
    End Property
    Public Property sAFAA_Description() As String
        Get
            Return AFAA_Description
        End Get
        Set(ByVal value As String)
            AFAA_Description = value
        End Set
    End Property
    Public Property sAFAA_ItemCode() As String
        Get
            Return AFAA_ItemCode
        End Get
        Set(ByVal value As String)
            AFAA_ItemCode = value
        End Set
    End Property
    Public Property sAFAA_ItemDescription() As String
        Get
            Return AFAA_ItemDescription
        End Get
        Set(ByVal value As String)
            AFAA_ItemDescription = value
        End Set
    End Property
    Public Property iAFAA_Quantity() As Integer
        Get
            Return AFAA_Quantity
        End Get
        Set(ByVal value As Integer)
            AFAA_Quantity = value
        End Set
    End Property
    Public Property dAFAA_CommissionDate() As Date
        Get
            Return AFAA_CommissionDate
        End Get
        Set(ByVal value As Date)
            AFAA_CommissionDate = value
        End Set
    End Property
    Public Property dAFAA_PurchaseDate() As Date
        Get
            Return AFAA_PurchaseDate
        End Get
        Set(ByVal value As Date)
            AFAA_PurchaseDate = value
        End Set
    End Property
    Public Property dAFAA_AssetAge() As Double
        Get
            Return AFAA_AssetAge
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAge = value
        End Set
    End Property
    Public Property dAFAA_AssetAmount() As Double
        Get
            Return AFAA_AssetAmount
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAmount = value
        End Set
    End Property
    Public Property iAFAA_AssetDelID() As Integer
        Get
            Return AFAA_AssetDelID
        End Get
        Set(ByVal value As Integer)
            AFAA_AssetDelID = value
        End Set
    End Property
    Public Property dAFAA_AssetDelDate() As Date
        Get
            Return AFAA_AssetDelDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDelDate = value
        End Set
    End Property
    Public Property dAFAA_AssetDeletionDate() As Date
        Get
            Return AFAA_AssetDeletionDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDeletionDate = value
        End Set
    End Property

    Public Property dAFAA_Assetvalue() As Double
        Get
            Return AFAA_Assetvalue
        End Get
        Set(ByVal value As Double)
            AFAA_Assetvalue = value
        End Set
    End Property

    Public Property sAFAA_AssetDesc() As String
        Get
            Return AFAA_AssetDesc
        End Get
        Set(ByVal value As String)
            AFAA_AssetDesc = value
        End Set
    End Property
    Public Property iAFAA_CreatedBy() As Integer
        Get
            Return AFAA_CreatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_CreatedBy = value
        End Set
    End Property
    Public Property dAFAA_CreatedOn() As Date
        Get
            Return AFAA_CreatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_CreatedOn = value
        End Set
    End Property
    Public Property iAFAA_UpdatedBy() As Integer
        Get
            Return AFAA_UpdatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_UpdatedBy = value
        End Set
    End Property
    Public Property dAFAA_UpdatedOn() As Date
        Get
            Return AFAA_UpdatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_UpdatedOn = value
        End Set
    End Property
    Public Property iAFAA_ApprovedBy() As Integer
        Get
            Return AFAA_ApprovedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_ApprovedBy = value
        End Set
    End Property
    Public Property dAFAA_ApprovedOn() As Date
        Get
            Return AFAA_ApprovedOn
        End Get
        Set(ByVal value As Date)
            AFAA_ApprovedOn = value
        End Set
    End Property
    Public Property dAFAA_Deletedby() As Integer
        Get
            Return AFAA_Deletedby
        End Get
        Set(ByVal value As Integer)
            AFAA_Deletedby = value
        End Set
    End Property
    Public Property dAFAA_DeletedOn() As Date
        Get
            Return AFAA_DeletedOn
        End Get
        Set(ByVal value As Date)
            AFAA_DeletedOn = value
        End Set
    End Property
    Public Property sAFAA_Status() As String
        Get
            Return AFAA_Status
        End Get
        Set(ByVal value As String)
            AFAA_Status = value
        End Set
    End Property
    Public Property sAFAA_Delflag() As String
        Get
            Return AFAA_Delflag
        End Get
        Set(ByVal value As String)
            AFAA_Delflag = value
        End Set
    End Property
    Public Property iAFAA_YearID() As Integer
        Get
            Return AFAA_YearID
        End Get
        Set(ByVal value As Integer)
            AFAA_YearID = value
        End Set
    End Property
    Public Property iAFAA_CompID() As Integer
        Get
            Return AFAA_CompID
        End Get
        Set(ByVal value As Integer)
            AFAA_CompID = value
        End Set
    End Property
    Public Property sAFAA_Operation() As String
        Get
            Return AFAA_Operation
        End Get
        Set(ByVal value As String)
            AFAA_Operation = value
        End Set
    End Property
    Public Property sAFAA_IPAddress() As String
        Get
            Return AFAA_IPAddress
        End Get
        Set(ByVal value As String)
            AFAA_IPAddress = value
        End Set
    End Property
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try

            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AFAM_ID)+1,1) from Acc_FixedAssetMaster")
            sPrefix = "FAT001" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select AFAA_ID,AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " order by AFAA_ID asc"
            sSql = "Select AFAA_ID,AFAA_AssetNo from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & " order by AFAA_ID asc"
            'sSql = "Select AFAA_ID,AFAA_AssetNo from Acc_FixedAssetAdditionDel where AFAA_Status<>'D' order by AFAA_ID asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExistingItemCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal assettype As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select AFAM_ID,AFAM_Itemcode from Acc_FixedAssetMaster where AFAM_AssetType=" & assettype & " and AFAM_Status='A' and AFAM_YearID='" & iYearId & "'"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CUR_ID,CUR_CODE + '-' + CUR_CountryName as CUR_CountryName from Currency_Master where CUR_Status='A' order by CUR_CountryName asc"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFxdAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GL_Desc,GL_ID From Chart_Of_Accounts Where GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetchartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CompID=" & iCompID & " and gl_DelFlag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPaymentsMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL
            dr("DebitORCredit") = iDbOrCr

            If iGLID > 0 Then
                Dim dtDGL As New DataTable
                Dim DVGLCODE As New DataView(dtCOA)
                DVGLCODE.RowFilter = "Gl_id=" & iGLID
                dtDGL = DVGLCODE.ToTable

                dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
                dr("GLDescription") = dtDGL.Rows(0)("gl_desc")

            Else
                dr("GLCode") = "" : dr("GLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("GLID") = "0"
            End If


            If iSubGL > 0 Then
                Dim dtDSUBGL As New DataTable
                Dim DVSUBGLCODE As New DataView(dtCOA)
                DVSUBGLCODE.RowFilter = "Gl_id=" & iSubGL
                dtDSUBGL = DVSUBGLCODE.ToTable

                dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
                dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            Else
                dr("SubGL") = "" : dr("SubGLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("SubGLID") = "0"
            End If


            Dim iCount As Integer = 0
            iCount = dtPayment.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iGLID)
                End If

                dr("Debit") = dAmount
                dr("Credit") = 0.00
                dr("DebitOrCredit") = 1
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
                dr("DebitOrCredit") = 2
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If
            'dtPayment.Merge(dt)
            Return dtPayment
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sColumn As String, ByVal iGlID As Integer) As Double
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dDebitOrCredit As Double = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from acc_Opening_Balance where Opn_GLID =" & iGlID & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dDebitOrCredit = dt.Rows(0)(sColumn).ToString()
            End If
            Return dDebitOrCredit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildTable() As DataTable
        Dim dt As New DataTable
        Dim dc As New DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebitORCredit", GetType(Integer))
            dt.Columns.Add(dc)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select gl_glcode from Chart_Of_Accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            LoadAssetNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer, ByVal iYearId As Integer) As Integer
        Dim sSql As String = ""
        Dim Grp As Integer
        Try
            sSql = "select IsNull(count(*),0)+1 from acc_fixedAssetAdditionDel where AFAA_AssetType='" & iglID & "' and AFAA_CompID=" & iCompID & " and AFAA_YearID='" & iYearId & "'"
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))
            Return Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetTypeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As String) As String
        Dim sSql As String = ""
        Try
            sSql = "select gl_glcode from chart_of_accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            GetAssetTypeNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetAddition(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstTrn As ClsAssetTransactionAddition) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(43) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetTrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetTrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CurrencyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyAmnt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CurrencyAmnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Zone", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Zone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Region", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Region
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Area", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Area
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Branch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ActualLocn", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_ActualLocn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_SupplierName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_SupplierCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetType", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetRefNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_ItemCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemDescription", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_ItemDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_PurchaseDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_PurchaseDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAge", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetDelID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetDelDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDeletionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetDeletionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Assetvalue", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Assetvalue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AssetDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AddnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AddnType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_DelnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_DelnType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Depreciation", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_Depreciation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AddtnDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsstTrn.AFAA_AddtnDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssetAdditionDel", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer) As String
        Dim sSql As String = "", sCOde As String = ""
        Dim sSuplrCode As String
        Try
            sSql = "Select CSM_Code from customerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_CompID = " & iCompID & ""
            sSuplrCode = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sSuplrCode
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
    Public Function LoadAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("Ext")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & " ATCH_ID = " & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("Ext") = dt.Rows(i)("ATCH_EXT")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objGen.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objGen.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetExtension(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer) As String
        Dim sSql As String, sExtn As String = ""
        Try
            sSql = "Select atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
            sExtn = objDBL.SQLGetDescription(sAC, sSql)
            Return sExtn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function showDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAFAA_ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Acc_FixedAssetAdditionDel Where AFAA_ID=" & iAFAA_ID & " And AFAA_CompID=" & iCompID & " And AFAA_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSuppliersLedgerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from CustomerSupplierMaster where CSM_ID =" & iParty & " and CSM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChartOfAccountHead(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iCompID & ""
            iAccHead = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function showDetailsAttachment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim drow As DataRow
        Dim sql As String = ""
        Try
            dt.Columns.Add("FilePath1")
            dt.Columns.Add("FileName1")
            dt.Columns.Add("Extension1")
            dt.Columns.Add("CreatedOn1")
            sql = "Select * from EDT_PAGE where PGE_FOLDER=" & iPaymentID & " and PGE_CompID=" & iCompID & ""
            dt1 = objDBL.SQLExecuteDataTable(sNameSpace, sql)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    drow = dt.NewRow

                    If IsDBNull(dt1.Rows(i)("pge_OrignalFileName").ToString()) = False Then
                        drow("FileName1") = dt1.Rows(i)("pge_OrignalFileName").ToString()
                    End If
                    If IsDBNull(dt1.Rows(i)("Pge_CreatedOn").ToString()) = False Then
                        drow("CreatedOn1") = objGen.FormatDtForRDBMS(dt1.Rows(i)("Pge_CreatedOn").ToString(), "D")
                    End If
                    If IsDBNull(dt1.Rows(i)("PGE_EXT").ToString()) = False Then
                        drow("Extension1") = dt1.Rows(i)("PGE_EXT").ToString()
                    End If
                    drow("FilePath1") = ""

                    dt.Rows.Add(drow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function LoadSavedTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try

            dc = New DataColumn("ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebitOrCredit", GetType(String))
            dt.Columns.Add(dc)

            sSql = "" : sSql = "select A.FXATD_ID,A.FXATD_Head,A.FXATD_PaymentType,A.FXATD_Gl,A.FXATD_SubGL,A.FXATD_Debit,A.FXATD_DbOrCr,"
            sSql = sSql & "A.FXATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc,"
            sSql = sSql & "D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit "
            sSql = sSql & "from Acc_FXDTransactions_Details A join chart_of_Accounts B on "
            sSql = sSql & "A.FXATD_BillId = " & iPaymentID & " and A.FXATD_TRType = 10 and A.FXATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            sSql = sSql & "A.FXATD_SubGL = C.gl_ID and A.FXATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            sSql = sSql & "D.Opn_GLID = A.FXATD_Gl and D.Opn_YearID = " & iYearID & " order by a.FXAtd_id "
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = ds.Tables(0).Rows(i)("FXATD_ID")

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("FXATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("FXATD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("FXATD_SubGL").ToString()
                    End If


                    If IsDBNull(ds.Tables(0).Rows(i)("GLCode").ToString()) = False Then
                        dr("GLCode") = ds.Tables(0).Rows(i)("GLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                        dr("GLDescription") = ds.Tables(0).Rows(i)("GLDescription").ToString()

                        If IsDBNull(ds.Tables(0).Rows(i)("GLDebit").ToString()) = False Then
                            If ds.Tables(0).Rows(i)("GLDebit").ToString() <> "0.00" Then
                                dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLDebit").ToString()
                            End If
                        End If

                        If IsDBNull(ds.Tables(0).Rows(i)("GLCredit").ToString()) = False Then
                            If ds.Tables(0).Rows(i)("GLCredit").ToString() <> "0.00" Then
                                dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLCredit").ToString()
                            End If
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Debit").ToString()) = False Then
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("FXATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("FXATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("FXATD_DbOrCr").ToString()) = False Then
                        dr("DebitOrCredit") = ds.Tables(0).Rows(i)("FXATD_DbOrCr")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsstId As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select AFAA_Delflag from  Acc_FixedAssetAdditionDel"
            sSql = sSql & " Where AFAA_ID=" & iAsstId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iyearId & " "
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub StatusCheck(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsstId As Integer, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_FixedAssetAdditionDel Set AFAA_Delflag='" & Sdelflag & "',AFAA_ApprovedBy=" & iApedby & ",AFAA_ApprovedOn=getdate(),AFAA_Status='" & sStatus & "'"
            sSql = sSql & " Where AFAA_ID=" & iAsstId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iyearId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal sCustomerName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sCustomerName & "' And CBN_Parent=-1 "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sCustomerName & "' And CBN_Parent=-1 "
                GetCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGenFun.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_NODE,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERGROUP,CBN_USERID,CBN_ParGrp,CBN_PERMISSION,cbn_DelStatus,CBN_SCCount,CBN_FolCount,cbn_Operation) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sCustomerName & "'," & -1 & ",'" & sCustomerName & "',0," & iUserID & ",0,0,'A',0,0,'X')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubCabinetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sTrTypeName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' And CBN_Parent=" & iCabinetID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sTrTypeName & "' And CBN_Parent=" & iCabinetID & " "
                GetSubCabinetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGenFun.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
                sSql = "" : sSql = "Insert Into EDT_CABINET(CBN_NODE,CBN_NAME,CBN_PARENT,CBN_Note,CBN_USERGROUP,CBN_USERID,CBN_ParGrp,CBN_PERMISSION,cbn_DelStatus,CBN_SCCount,CBN_FolCount,cbn_Operation) "
                sSql = sSql & "Values(" & iMaxID & ",'" & sTrTypeName & "'," & iCabinetID & ",'" & sTrTypeName & "',0," & iUserID & ",0,0,'A',0,0,'X')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetSubCabinetID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolderID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sFolderName As String) As Integer
        Dim bCheck As Boolean
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "select * from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "select FOL_FOLID from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
                GetFolderID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                iMaxID = objGenFun.GetEdictMaxID(sNameSpace, iCompID, "edt_folder", "FOL_FOLID")
                sSql = "" : sSql = "Insert Into edt_folder(FOL_FOLID,FOL_CABINET,FOL_NAME,FOL_NOTES,FOL_CRBY,FOL_STATUS,FOL_PAGECOUNT,fol_operation,fol_operationBy) "
                sSql = sSql & "Values(" & iMaxID & "," & iCabinetID & ",'" & sFolderName & "','" & sFolderName & "'," & iUserID & ",'A',0,'I'," & iUserID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetFolderID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As String
        Dim sSql As String = ""
        Dim sDesc As String
        Try
            sSql = "select AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_ID=" & iID & "  And AFAA_CompID=" & iCompID & ""
            sDesc = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sDesc
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePage(ByVal sAC As String, ByVal iACID As Integer, ByVal objAsstTrn As ClsAssetTransactionAddition) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CABINET", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_CABINET
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_FOLDER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DOCUMENT_TYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_DOCUMENT_TYPE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_TITLE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_TITLE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DATE", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAsstTrn.dPGE_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_DETAILS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPgeDETAILSID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPge_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OBJECT", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_OBJECT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_PAGENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_PAGENO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_EXT", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_EXT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_KeyWORD", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_KeyWORD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRText", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_OCRText
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SIZE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_SIZE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CURRENT_VER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_CURRENT_VER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.sPGESTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_SubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_QC_UsrGrpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_QC_UsrGrpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FTPStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.sPGE_FTPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_batch_name", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_batch_name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_OrignalFileName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAsstTrn.spge_OrignalFileName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_BatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRDelFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPGE_OCRDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iPge_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.spge_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGE", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ISFileinDB(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String
        Dim str As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = 'FilesInDB'"
            str = objDBL.SQLExecuteScalar(sAC, sSql)
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImagePath(ByVal sAC As String) As String
        Dim sSql As String
        Dim str As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = 'ImgPath'"
            str = objDBL.SQLExecuteScalar(sAC, sSql)
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateImageSettings(ByVal sAC As String, ByVal iACID As Integer, ByVal iBaseId As Long, ByVal iPageID As Long)
        Dim ssql As String
        Try
            ssql = "Select * from edt_image_settings where img_Form = 'S' and img_IMGID = " & iPageID & ""
            If objDBL.DBCheckForRecord(sAC, ssql) = True Then
                objDBL.SQLExecuteNonQuery(sAC, "Update EDT_IMAGE_Settings Set img_Form = 'I' , img_IMGID = " & iBaseId & "  where img_Form = 'S' and img_IMGID = " & iPageID & "")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetTransactionDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objAsstTrn As ClsAssetTransactionAddition) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objAsstTrn.dFXATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_TrType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstTrn.dFXATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objAsstTrn.dFXATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objAsstTrn.sFXATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_YearID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsstTrn.iFXATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_Operation ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsstTrn.sFXATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FXATD_IPAddress ", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objAsstTrn.sFXATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_FXDTransactions_Details", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDeletionStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetId As Integer, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "update Acc_FixedAssetAdditionDel set AFAA_Status='" & sStatus & "', AFAA_Delflag='Y' where AFAA_ID=" & iAssetId & " and AFAA_CompID=" & iCompID & ""
            objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdatePhysicalverificationdtls(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal sVrfiedby As String, ByVal sVrfiedOn As String, ByVal sApprovedby As String, ByVal sApprovedOn As String, ByVal sVrfiedremakes As String, ByVal sApprovedremarks As String, ByVal iExTrnId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "update Acc_FixedAssetAdditionDel set AFAA_PhyVerifiedby='" & sVrfiedby & "',AFAA_PhyVerifiedOn='" & objGen.FormatDtForRDBMS(sVrfiedOn, "CT") & "',AFAA_PhyApprovedby='" & sApprovedby & "',AFAA_PhyApprovedOn='" & objGen.FormatDtForRDBMS(sApprovedOn, "CT") & "',AFAA_PhyVerifiedRemarks='" & sVrfiedremakes & "',AFAA_PhyApprovedRemarks='" & sApprovedremarks & "' where AFAA_ID=" & iExTrnId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iYearId & ""
            UpdatePhysicalverificationdtls = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImageSettings(ByVal sAC As String, ByVal sCode As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from sad_config_settings where sad_Config_Key ='" & sCode & "'"
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

    Public Function GetItemDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal itemcode As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from acc_fixedAssetmaster where afam_itemcode='" & itemcode & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateFixedMasterLoan(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal sLoanTwhom As String, ByVal dLoanAmt As Double, ByVal sLnAggrmntNo As String, ByVal dLnDate As Date, ByVal iCrncyType As Integer, ByVal dLnExchDate As Date, ByVal sAfamICOde As String)
        Dim sSql As String = ""
        Try
            sSql = "update acc_fixedAssetmaster set AFAM_LToWhom='" & sLoanTwhom & "',AFAM_LAmount='" & dLoanAmt & "',AFAM_LAggriNo='" & sLnAggrmntNo & "',AFAM_LDate='" & objGen.FormatDtForRDBMS(dLnDate, "CT") & "',AFAM_LCurrencyType='" & iCrncyType & "',AFAM_LExchDate='" & objGen.FormatDtForRDBMS(dLnExchDate, "CT") & "' where AFAM_ItemCode='" & sAfamICOde & "' and AFAM_CompID=" & iCompID & " and AFAM_YearID=" & iYearId & ""
            UpdateFixedMasterLoan = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPrevTransDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sItemCode As String, ByVal iAstType As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("AssetNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")


            sSql = "select FXATD_BillId,FXATD_GL,FXATD_SubGL, FXATD_Debit, FXATD_Credit from Acc_FXDTransactions_Details  where FXATD_TrType=10 and FXATD_BillId in (select AFAA_ID from Acc_FixedAssetAdditionDel where AFAA_ItemCode='" & sItemCode & "' and AFAA_AssetType= '" & iAstType & "')"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("FXATD_BillId")) = False Then
                    dRow("AssetNo") = objDBL.SQLExecuteScalar(sAC, "select AFAA_AssetRefNo from Acc_FixedAssetAdditionDel where AFAA_ID = " & dtDetails.Rows(i)("FXATD_BillId") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("FXATD_GL")) = False Then
                    dRow("GLCode") = objDBL.SQLExecuteScalar(sAC, "select gl_glcode from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("FXATD_GL") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("FXATD_GL")) = False Then
                    dRow("GLDescription") = objDBL.SQLExecuteScalar(sAC, "select GL_Desc from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("FXATD_GL") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("FXATD_SubGL")) = False Then
                    dRow("SubGL") = objDBL.SQLExecuteScalar(sAC, "select gl_glcode from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("FXATD_SubGL") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("FXATD_SubGL")) = False Then
                    dRow("SubGLDescription") = objDBL.SQLExecuteScalar(sAC, "select GL_Desc from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("FXATD_SubGL") & "")
                End If
                dRow("OpeningBalance") = 0.00
                If IsDBNull(dtDetails.Rows(i)("FXATD_Debit")) = False Then
                    dRow("Debit") = dtDetails.Rows(i)("FXATD_Debit")
                End If

                If IsDBNull(dtDetails.Rows(i)("FXATD_Credit")) = False Then
                    dRow("Credit") = dtDetails.Rows(i)("FXATD_Credit")
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDelQty(ByVal sAC As String, ByVal iACID As Integer, ByVal itemType As Integer, ByVal itemcode As String) As Double
        Dim sSql As String
        Dim dt As New DataTable
        Dim iDelqty As Integer = 0
        Try
            sSql = "Select AFAD_Quantity  from Acc_FixedAssetDeletion where AFAD_ItemCode='" & itemcode & "'and AFAD_AssetType='" & itemType & "' and  AFAD_Status='D' order by AFAD_ID desc"
            iDelqty = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iDelqty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFilesCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Count(PGE_BASENAME) from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "') And pge_CompID=" & iCompID & " "
            BindAttachFilesCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return BindAttachFilesCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "') And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
