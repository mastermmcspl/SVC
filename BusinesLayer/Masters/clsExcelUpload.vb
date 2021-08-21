Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data

Public Class clsExcelUpload
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iACMID As Integer
    Private sACMType As String
    Private sACMName As String
    Private sACMCode As String

    Private iACMHead As Integer
    Private iACMGroup As Integer
    Private iACMSubGroup As Integer
    Private iACMGL As Integer
    Private iACMSubGL As Integer

    Private iACMCompID As Integer
    Private sACMStatus As String
    Private iACMCreatedBy As Integer
    Private iACMApprovedBy As Integer
    Private sACMIPAddrs As String
    Private sACMOperations As String

    Private iACADID As Integer
    Private iACADMasterID As Integer
    Private sACADAddr As String
    Private iACADCity As Integer
    Private iACADState As Integer
    Private iACADCountry As Integer
    Private iACADPincode As Integer
    Private sACADContactPerson As String
    Private sACADEmail As String
    Private sACADMobileNo As String
    Private sACADLandline As String
    Private sACADFax As String
    Private iACADCompID As Integer
    Private sACADStatus As String
    Private iACADCreatedBy As Integer


    Private iBM_ID As Integer
    Private iBM_IndType As Integer
    Private sBM_CustName As String
    Private sBM_Name As String
    Private sBM_Code As String
    Private iBM_Inventry As Integer
    Private sBM_ContactPerson As String

    Private sBM_EmailID As String
    Private sBM_MobileNo As String
    Private sBM_LandLineNo As String
    Private sBM_Fax As String
    Private sBM_Address As String
    Private sBM_Pincode As String
    Private iBM_City As Integer
    Private iBM_State As Integer

    Private sBM_Delflag As String
    Private iBM_CompID As Integer
    Private iBM_YearID As Integer
    Private sBM_Status As String
    Private sBM_Operation As String
    Private sBM_IPAddress As String
    Private iBM_CreatedBy As Integer
    Private dBM_CreatedOn As DateTime
    Private iBM_ApprovedBy As Integer
    Private dBM_ApprovedOn As DateTime
    Private iBM_DeletedBy As Integer
    Private dBM_DeletedOn As DateTime
    Private iBM_UpdatedBy As Integer
    Private dBM_UpdatedOn As DateTime

    Private iBM_Group As Integer
    Private iBM_SubGroup As Integer
    Private iBM_GL As Integer
    Private iBM_SubGL As Integer

    Private sBM_Address1 As String
    Private sBM_Address2 As String
    Private sBM_Address3 As String
    Private iBM_GenCategory As Integer
    Private sBM_GSTNRegNo As String
    Private iBM_CompanyType As Integer
    Private iBM_GSTNCategory As Integer

    Private iCSM_ID As Integer
    Private iCSM_IndType As Integer
    Private sCSM_CustName As String
    Private sCSM_Name As String
    Private sCSM_Code As String
    Private iCSM_Inventry As Integer
    Private sCSM_ContactPerson As String
    Private sCSM_ProductDescription
    Private sCSM_EmailID As String
    Private sCSM_MobileNo As String
    Private sCSM_LandLineNo As String
    Private sCSM_Fax As String

    Private sCSM_Address As String
    Private sCSM_Address1 As String
    Private sCSM_Address2 As String
    Private sCSM_Address3 As String

    Private sCSM_Pincode As String
    Private iCSM_City As Integer
    Private iCSM_State As Integer

    Private sCSM_Delflag As String
    Private iCSM_CompID As Integer
    Private sCSM_Status As String
    Private sCSM_Operation As String
    Private sCSM_IPAddress As String
    Private iCSM_CreatedBy As Integer
    Private dCSM_CreatedOn As DateTime
    Private iCSM_ApprovedBy As Integer
    Private dCSM_ApprovedOn As DateTime
    Private iCSM_DeletedBy As Integer
    Private dCSM_DeletedOn As DateTime
    Private iCSM_UpdatedBy As Integer
    Private dCSM_UpdatedOn As DateTime
    Private iCSM_Group As Integer
    Private iCSM_SubGroup As Integer
    Private iCSM_GL As Integer
    Private iCSM_SubGL As Integer


    Private Usr_ID As Integer
    Private Usr_Node As Integer
    Private Usr_Code As String
    Private Usr_FullName As String
    Private Usr_LoginName As String
    Private Usr_Password As String
    Private Usr_Email As String
    Private Usr_LevelGrp As Integer
    Private Usr_DutyStatus As String
    Private Usr_PhoneNo As String
    Private Usr_MobileNo As String
    Private Usr_OfficePhone As String
    Private Usr_OffPhExtn As String
    Private Usr_Designation As Integer
    Private Usr_CompanyID As Integer
    Private Usr_OrgID As Integer
    Private Usr_GrpOrUserLvlPerm As Integer
    Private Usr_NoOfUnsucsfAtteptts As Integer
    Private Usr_Ques As String
    Private Usr_Ans As String
    Private Usr_SentMail As Integer
    Private Usr_Partner As Integer
    Private Usr_NoOfLogin As Integer
    Private Usr_LastLoginDate As Date
    Private Usr_CreatedBy As Integer
    Private Usr_CreatedOn As Date
    Private Usr_UpdatedBy As Integer
    Private Usr_UpdatedOn As Date
    Private Usr_AppBy As Integer
    Private Usr_AppOn As Date
    Private Usr_DeletedBy As Integer
    Private Usr_DeletedOn As Date
    Private Usr_RecallBy As Integer
    Private Usr_RecallOn As Date
    Private Usr_Flag As String
    Private Usr_Status As String
    Private Usr_CompId As Integer
    Private Usr_Role As Integer
    Private Usr_MasterModule As Integer
    Private Usr_PurchaseModule As Integer
    Private Usr_SalesModule As Integer
    Private Usr_AccountsModule As Integer
    Private Usr_MasterRole As Integer
    Private Usr_PurchaseRole As Integer
    Private Usr_SalesRole As Integer
    Private Usr_AccountsRole As Integer
    Private Usr_IPAdress As String
    Private Usr_Address As String
    'Opening balance subledger
    Private SubOpn_Id As Integer
    Private SubCreatedOpn_Date As Date
    Private SubOpn_AccHead As Integer
    Private SubOpn_GLCode As String
    Private SubOpn_DebitAmt As Double
    Private SubOpn_CreditAmount As Double
    Private SubOpn_YearId As Integer
    Private SubOpn_Status As String
    Private SubOpn_CompId As Integer
    Private SubOpn_GlId As Integer
    Private SubOpn_IPAddress As String
    Private SubOpn_ReferenceNo As String
    Private SubPenidingAmount As Double
    Private SubDueOn As Date
    Private SubOpn_OverDueDays As Integer
    Private SubBillDate As Date
    Private SubOpn_SubGlId As Integer
    Private SubOpn_DabitOrCredit As Integer
    Private SubOpn_TransctionNo As String
    Private SubOpn_BilLID As Integer
    'Opening balance subledger
    Public Structure GST
        Dim AGS_ID As Integer
        Dim AGS_GSTM_ID As Integer
        Dim AGS_Schedule_Type As Integer
        Dim AGS_GSTRate As Double
        Dim AGS_SlnoOfSchedule As String
        Dim AGS_CHST As String
        Dim AGS_Chapter As String
        Dim AGS_Heading As String
        Dim AGS_SubHeading As String
        Dim AGS_Tarrif As String
        Dim AGS_GoodDescription As String
        Dim AGS_NotificationNo As String
        Dim AGS_NotificationDate As Date
        Dim AGS_FileNo As String
        Dim AGS_FileDate As Date
        Dim AGS_Createdby As Integer
        Dim AGS_CreatedOn As Date
        Dim AGS_Status As String
        Dim AGS_YearID As Integer
        Dim AGS_CompID As Integer
        Dim AGS_Operation As String
        Dim AGS_IPAddress As String
    End Structure

    Private ATD_ID As Integer
    Private ATD_TransactionDate As Date
    Private ATD_TrType As Integer
    Private ATD_BillId As Integer
    Private ATD_PaymentType As Integer
    Private ATD_Head As Integer
    Private ATD_DbOrCr As Integer
    Private ATD_GL As Integer
    Private ATD_SubGL As Integer
    Private ATD_Debit As Decimal
    Private ATD_Credit As Decimal
    Private ATD_CreatedOn As Date
    Private ATD_CreatedBy As Integer
    Private ATD_ApprovedBy As Integer
    Private ATD_ApprovedOn As Date
    Private ATD_Deletedby As Integer
    Private ATD_DeletedOn As Date
    Private ATD_Status As String
    Private ATD_YearID As Integer
    Private ATD_CompID As Integer
    Private ATD_Operation As String
    Private ATD_IPAddress As String

    Private ATD_UpdatedOn As Date
    Private ATD_UpdatedBy As Integer
    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private iCSM_CompanyType As Integer
    Private iCSM_GSTNCategory As Integer
    Private iCSM_GSTNRegNo As String

    Public Property CSM_GSTNCategory() As Integer
        Get
            Return (iCSM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iCSM_GSTNCategory = Value
        End Set
    End Property


    Public Property CSM_GSTNRegNo() As String
        Get
            Return (iCSM_GSTNRegNo)
        End Get
        Set(ByVal Value As String)
            iCSM_GSTNRegNo = Value
        End Set
    End Property
    Public Property CSM_CompanyType() As Integer
        Get
            Return (iCSM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iCSM_CompanyType = Value
        End Set
    End Property
    Public Property iATD_ZoneID() As Integer
        Get
            Return (ATD_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ATD_ZoneID = Value
        End Set
    End Property
    Public Property iATD_RegionID() As Integer
        Get
            Return (ATD_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ATD_RegionID = Value
        End Set
    End Property
    Public Property iATD_AreaID() As Integer
        Get
            Return (ATD_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ATD_AreaID = Value
        End Set
    End Property
    Public Property iATD_BranchID() As Integer
        Get
            Return (ATD_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ATD_BranchID = Value
        End Set
    End Property
    Public Property sATD_IPAddress() As String
        Get
            Return (ATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATD_IPAddress = Value
        End Set
    End Property
    Public Property sATD_Operation() As String
        Get
            Return (ATD_Operation)
        End Get
        Set(ByVal Value As String)
            ATD_Operation = Value
        End Set
    End Property
    Public Property iATD_YearID() As Integer
        Get
            Return (ATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            ATD_YearID = Value
        End Set
    End Property
    Public Property iATD_CompID() As Integer
        Get
            Return (ATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            ATD_CompID = Value
        End Set
    End Property
    Public Property sATD_Status() As String
        Get
            Return (ATD_Status)
        End Get
        Set(ByVal Value As String)
            ATD_Status = Value
        End Set
    End Property
    Public Property dATD_DeletedOn() As Date
        Get
            Return (ATD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_DeletedOn = Value
        End Set
    End Property
    Public Property iATD_Deletedby() As Integer
        Get
            Return (ATD_Deletedby)
        End Get
        Set(ByVal Value As Integer)
            ATD_Deletedby = Value
        End Set
    End Property
    Public Property dATD_ApprovedOn() As Date
        Get
            Return (ATD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_ApprovedOn = Value
        End Set
    End Property
    Public Property iATD_ApprovedBy() As Integer
        Get
            Return (ATD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_ApprovedBy = Value
        End Set
    End Property
    Public Property iATD_CreatedBy() As Integer
        Get
            Return (ATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_CreatedBy = Value
        End Set
    End Property
    Public Property dATD_CreatedOn() As Date
        Get
            Return (ATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_CreatedOn = Value
        End Set
    End Property
    Public Property iATD_UpdatedBy() As Integer
        Get
            Return (ATD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_UpdatedBy = Value
        End Set
    End Property
    Public Property dATD_UpdatedOn() As Date
        Get
            Return (ATD_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_UpdatedOn = Value
        End Set
    End Property
    Public Property dATD_Credit() As Decimal
        Get
            Return (ATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Credit = Value
        End Set
    End Property
    Public Property dATD_Debit() As Decimal
        Get
            Return (ATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Debit = Value
        End Set
    End Property
    Public Property iATD_SubGL() As Integer
        Get
            Return (ATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            ATD_SubGL = Value
        End Set
    End Property
    Public Property iATD_GL() As Integer
        Get
            Return (ATD_GL)
        End Get
        Set(ByVal Value As Integer)
            ATD_GL = Value
        End Set
    End Property
    Public Property iATD_Head() As Integer
        Get
            Return (ATD_Head)
        End Get
        Set(ByVal Value As Integer)
            ATD_Head = Value
        End Set
    End Property

    Public Property iATD_DbOrCr() As Integer
        Get
            Return (ATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            ATD_DbOrCr = Value
        End Set
    End Property
    Public Property iATD_PaymentType() As Integer
        Get
            Return (ATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            ATD_PaymentType = Value
        End Set
    End Property
    Public Property iATD_BillId() As Integer
        Get
            Return (ATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            ATD_BillId = Value
        End Set
    End Property
    Public Property iATD_TrType() As Integer
        Get
            Return (ATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            ATD_TrType = Value
        End Set
    End Property
    Public Property dATD_TransactionDate() As Date
        Get
            Return (ATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            ATD_TransactionDate = Value
        End Set
    End Property
    Public Property iATD_ID() As Integer
        Get
            Return (ATD_ID)
        End Get
        Set(ByVal Value As Integer)
            ATD_ID = Value
        End Set
    End Property


    Public Property BM_GSTNRegNo() As String
        Get
            Return (sBM_GSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sBM_GSTNRegNo = Value
        End Set
    End Property
    Public Property BM_CompanyType() As Integer
        Get
            Return (iBM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iBM_CompanyType = Value
        End Set
    End Property
    Public Property BM_GSTNCategory() As Integer
        Get
            Return (iBM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iBM_GSTNCategory = Value
        End Set
    End Property

    Public Property sSubOpn_IPAddress() As String
        Get
            Return (SubOpn_IPAddress)
        End Get
        Set(ByVal Value As String)
            SubOpn_IPAddress = Value
        End Set
    End Property
    Public Property iSubOpn_GlId() As Integer
        Get
            Return (SubOpn_GlId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_GlId = Value
        End Set
    End Property

    Public Property iSubOpn_SubGlId() As Integer
        Get
            Return (SubOpn_SubGlId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_SubGlId = Value
        End Set
    End Property

    Public Property iSubOpn_DabitOrCredit() As Integer
        Get
            Return (SubOpn_DabitOrCredit)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_DabitOrCredit = Value
        End Set
    End Property

    Public Property iSubOpn_CompId() As Integer
        Get
            Return (SubOpn_CompId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_CompId = Value
        End Set
    End Property

    Public Property iSubOpn_BilLID() As Integer
        Get
            Return (SubOpn_BilLID)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_BilLID = Value
        End Set
    End Property
    Public Property sSubOpn_Status() As String
        Get
            Return (SubOpn_Status)
        End Get
        Set(ByVal Value As String)
            SubOpn_Status = Value
        End Set
    End Property

    Public Property iSubOpn_YearId() As Integer
        Get
            Return (SubOpn_YearId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_YearId = Value
        End Set
    End Property
    Public Property dSubOpn_CreditAmount() As Double
        Get
            Return (SubOpn_CreditAmount)
        End Get
        Set(ByVal Value As Double)
            SubOpn_CreditAmount = Value
        End Set
    End Property
    Public Property dSubOpn_DebitAmt() As Double
        Get
            Return (SubOpn_DebitAmt)
        End Get
        Set(ByVal Value As Double)
            SubOpn_DebitAmt = Value
        End Set
    End Property
    Public Property sSubOpn_GLCode() As String
        Get
            Return (SubOpn_GLCode)
        End Get
        Set(ByVal Value As String)
            SubOpn_GLCode = Value
        End Set
    End Property

    Public Property SubOpn_sTransctionNo() As String
        Get
            Return (SubOpn_TransctionNo)
        End Get
        Set(ByVal Value As String)
            SubOpn_TransctionNo = Value
        End Set
    End Property

    Public Property iSubOpn_AccHead() As Integer
        Get
            Return (SubOpn_AccHead)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_AccHead = Value
        End Set
    End Property
    Public Property iSubOpn_OverDueDays() As Integer
        Get
            Return (SubOpn_OverDueDays)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_OverDueDays = Value
        End Set
    End Property
    Public Property dSubDueOn() As Date
        Get
            Return (SubDueOn)
        End Get
        Set(ByVal Value As Date)
            SubDueOn = Value
        End Set
    End Property
    Public Property dSubPenidingAmount() As Double
        Get
            Return (SubPenidingAmount)
        End Get
        Set(ByVal Value As Double)
            SubPenidingAmount = Value
        End Set
    End Property
    Public Property sSubOpn_ReferenceNo() As String
        Get
            Return (SubOpn_ReferenceNo)
        End Get
        Set(ByVal Value As String)
            SubOpn_ReferenceNo = Value
        End Set
    End Property

    Public Property dSubCreatedOpn_Date() As Date
        Get
            Return (SubCreatedOpn_Date)
        End Get
        Set(ByVal Value As Date)
            SubCreatedOpn_Date = Value
        End Set
    End Property
    Public Property iSubOpn_Id() As Integer
        Get
            Return (SubOpn_Id)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_Id = Value
        End Set
    End Property

    Public Property dSubBillDate() As Date
        Get
            Return (SubBillDate)
        End Get
        Set(ByVal Value As Date)
            SubBillDate = Value
        End Set
    End Property
    Public Property iACM_ID() As Integer
        Get
            Return (iACMID)
        End Get
        Set(ByVal Value As Integer)
            iACMID = Value
        End Set
    End Property
    Public Property sACM_Type() As String
        Get
            Return (sACMType)
        End Get
        Set(ByVal Value As String)
            sACMType = Value
        End Set
    End Property
    Public Property sACM_Name() As String
        Get
            Return (sACMName)
        End Get
        Set(ByVal Value As String)
            sACMName = Value
        End Set
    End Property
    Public Property sACM_Code() As String
        Get
            Return (sACMCode)
        End Get
        Set(ByVal Value As String)
            sACMCode = Value
        End Set
    End Property
    Public Property iACM_Head() As Integer
        Get
            Return (iACMHead)
        End Get
        Set(ByVal Value As Integer)
            iACMHead = Value
        End Set
    End Property
    Public Property iACM_Group() As Integer
        Get
            Return (iACMGroup)
        End Get
        Set(ByVal Value As Integer)
            iACMGroup = Value
        End Set
    End Property
    Public Property iACM_SubGroup() As Integer
        Get
            Return (iACMSubGroup)
        End Get
        Set(ByVal Value As Integer)
            iACMSubGroup = Value
        End Set
    End Property
    Public Property iACM_GL() As Integer
        Get
            Return (iACMGL)
        End Get
        Set(ByVal Value As Integer)
            iACMGL = Value
        End Set
    End Property
    Public Property iACM_SubGL() As Integer
        Get
            Return (iACMSubGL)
        End Get
        Set(ByVal Value As Integer)
            iACMSubGL = Value
        End Set
    End Property
    Public Property sACM_Status() As String
        Get
            Return (sACMStatus)
        End Get
        Set(ByVal Value As String)
            sACMStatus = Value
        End Set
    End Property
    Public Property iACM_CompID() As Integer
        Get
            Return (iACMCompID)
        End Get
        Set(ByVal Value As Integer)
            iACMCompID = Value
        End Set
    End Property
    Public Property iACM_CreatedBy() As Integer
        Get
            Return (iACMCreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMCreatedBy = Value
        End Set
    End Property
    Public Property iACM_ApprovedBy() As Integer
        Get
            Return (iACMApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMApprovedBy = Value
        End Set
    End Property
    Public Property sACM_IPAddrs() As String
        Get
            Return (sACMIPAddrs)
        End Get
        Set(ByVal Value As String)
            sACMIPAddrs = Value
        End Set
    End Property
    Public Property sACM_Operations() As String
        Get
            Return (sACMOperations)
        End Get
        Set(ByVal Value As String)
            sACMOperations = Value
        End Set
    End Property
    Public Property iACAD_ID() As Integer
        Get
            Return (iACADID)
        End Get
        Set(ByVal Value As Integer)
            iACADID = Value
        End Set
    End Property
    Public Property iACAD_MasterID() As Integer
        Get
            Return (iACADMasterID)
        End Get
        Set(ByVal Value As Integer)
            iACADMasterID = Value
        End Set
    End Property
    Public Property sACAD_Addr() As String
        Get
            Return (sACADAddr)
        End Get
        Set(ByVal Value As String)
            sACADAddr = Value
        End Set
    End Property
    Public Property iACAD_City() As Integer
        Get
            Return (iACADCity)
        End Get
        Set(ByVal Value As Integer)
            iACADCity = Value
        End Set
    End Property
    Public Property iACAD_State() As Integer
        Get
            Return (iACADState)
        End Get
        Set(ByVal Value As Integer)
            iACADState = Value
        End Set
    End Property
    Public Property iACAD_Country() As Integer
        Get
            Return (iACADCountry)
        End Get
        Set(ByVal Value As Integer)
            iACADCountry = Value
        End Set
    End Property
    Public Property iACAD_Pincode() As Integer
        Get
            Return (iACADPincode)
        End Get
        Set(ByVal Value As Integer)
            iACADPincode = Value
        End Set
    End Property
    Public Property sACAD_ContactPerson() As String
        Get
            Return (sACADContactPerson)
        End Get
        Set(ByVal Value As String)
            sACADContactPerson = Value
        End Set
    End Property
    Public Property sACAD_Email() As String
        Get
            Return (sACADEmail)
        End Get
        Set(ByVal Value As String)
            sACADEmail = Value
        End Set
    End Property
    Public Property sACAD_MobileNo() As String
        Get
            Return (sACADMobileNo)
        End Get
        Set(ByVal Value As String)
            sACADMobileNo = Value
        End Set
    End Property
    Public Property sACAD_Landline() As String
        Get
            Return (sACADLandline)
        End Get
        Set(ByVal Value As String)
            sACADLandline = Value
        End Set
    End Property
    Public Property sACAD_Fax() As String
        Get
            Return (sACADFax)
        End Get
        Set(ByVal Value As String)
            sACADFax = Value
        End Set
    End Property
    Public Property iACAD_CompID() As Integer
        Get
            Return (iACADCompID)
        End Get
        Set(ByVal Value As Integer)
            iACADCompID = Value
        End Set
    End Property
    Public Property sACAD_Status() As String
        Get
            Return (sACADStatus)
        End Get
        Set(ByVal Value As String)
            sACADStatus = Value
        End Set
    End Property
    Public Property iACAD_CreatedBy() As Integer
        Get
            Return (iACADCreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iACADCreatedBy = Value
        End Set
    End Property


    Public Property BM_GenCategory() As Integer
        Get
            Return (iBM_GenCategory)
        End Get
        Set(ByVal Value As Integer)
            iBM_GenCategory = Value
        End Set
    End Property
    Public Property BM_Address1() As String
        Get
            Return (sBM_Address1)
        End Get
        Set(ByVal Value As String)
            sBM_Address1 = Value
        End Set
    End Property
    Public Property BM_Address2() As String
        Get
            Return (sBM_Address2)
        End Get
        Set(ByVal Value As String)
            sBM_Address2 = Value
        End Set
    End Property
    Public Property BM_Address3() As String
        Get
            Return (sBM_Address3)
        End Get
        Set(ByVal Value As String)
            sBM_Address3 = Value
        End Set
    End Property
    Public Property BM_GL() As Integer
        Get
            Return (iBM_GL)
        End Get
        Set(ByVal Value As Integer)
            iBM_GL = Value
        End Set
    End Property
    Public Property BM_SubGL() As Integer
        Get
            Return (iBM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iBM_SubGL = Value
        End Set
    End Property
    Public Property BM_SubGroup() As Integer
        Get
            Return (iBM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iBM_SubGroup = Value
        End Set
    End Property
    Public Property BM_Group() As Integer
        Get
            Return (iBM_Group)
        End Get
        Set(ByVal Value As Integer)
            iBM_Group = Value
        End Set
    End Property
    Public Property BM_City() As Integer
        Get
            Return (iBM_City)
        End Get
        Set(ByVal Value As Integer)
            iBM_City = Value
        End Set
    End Property
    Public Property BM_Fax() As String
        Get
            Return (sBM_Fax)
        End Get
        Set(ByVal Value As String)
            sBM_Fax = Value
        End Set
    End Property
    Public Property BM_LandLineNo() As String
        Get
            Return (sBM_LandLineNo)
        End Get
        Set(ByVal Value As String)
            sBM_LandLineNo = Value
        End Set
    End Property
    Public Property BM_MobileNo() As String
        Get
            Return (sBM_MobileNo)
        End Get
        Set(ByVal Value As String)
            sBM_MobileNo = Value
        End Set
    End Property
    Public Property BM_EmailID() As String
        Get
            Return (sBM_EmailID)
        End Get
        Set(ByVal Value As String)
            sBM_EmailID = Value
        End Set
    End Property
    Public Property BM_ID() As Integer
        Get
            Return (iBM_ID)
        End Get
        Set(ByVal Value As Integer)
            iBM_ID = Value
        End Set
    End Property
    Public Property BM_IndType() As Integer
        Get
            Return (iBM_IndType)
        End Get
        Set(ByVal Value As Integer)
            iBM_IndType = Value
        End Set
    End Property
    Public Property BM_CustName() As String
        Get
            Return (sBM_CustName)
        End Get
        Set(ByVal Value As String)
            sBM_CustName = Value
        End Set
    End Property
    Public Property BM_Name() As String
        Get
            Return (sBM_Name)
        End Get
        Set(ByVal Value As String)
            sBM_Name = Value
        End Set
    End Property
    Public Property BM_Code() As String
        Get
            Return (sBM_Code)
        End Get
        Set(ByVal Value As String)
            sBM_Code = Value
        End Set
    End Property
    Public Property BM_Inventry() As Integer
        Get
            Return (iBM_Inventry)
        End Get
        Set(ByVal Value As Integer)
            iBM_Inventry = Value
        End Set
    End Property
    Public Property BM_ContactPerson() As String
        Get
            Return (sBM_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sBM_ContactPerson = Value
        End Set
    End Property
    Public Property BM_Address() As String
        Get
            Return (sBM_Address)
        End Get
        Set(ByVal Value As String)
            sBM_Address = Value
        End Set
    End Property
    Public Property BM_State() As Integer
        Get
            Return (iBM_State)
        End Get
        Set(ByVal Value As Integer)
            iBM_State = Value
        End Set
    End Property
    Public Property BM_Pincode() As String
        Get
            Return (sBM_Pincode)
        End Get
        Set(ByVal Value As String)
            sBM_Pincode = Value
        End Set
    End Property
    Public Property BM_Delflag() As String
        Get
            Return (sBM_Delflag)
        End Get
        Set(ByVal Value As String)
            sBM_Delflag = Value
        End Set
    End Property
    Public Property BM_CompID() As Integer
        Get
            Return (iBM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBM_CompID = Value
        End Set
    End Property
    Public Property BM_YearID() As Integer
        Get
            Return (iBM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iBM_YearID = Value
        End Set
    End Property
    Public Property BM_Status() As String
        Get
            Return (sBM_Status)
        End Get
        Set(ByVal Value As String)
            sBM_Status = Value
        End Set
    End Property
    Public Property BM_Operation() As String
        Get
            Return (sBM_Operation)
        End Get
        Set(ByVal Value As String)
            sBM_Operation = Value
        End Set
    End Property
    Public Property BM_IPAddress() As String
        Get
            Return (sBM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBM_IPAddress = Value
        End Set
    End Property
    Public Property BM_CreatedBy() As Integer
        Get
            Return (iBM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBM_CreatedBy = Value
        End Set
    End Property
    Public Property BM_CreatedOn() As DateTime
        Get
            Return (dBM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBM_CreatedOn = Value
        End Set
    End Property
    Public Property BM_ApprovedBy() As Integer
        Get
            Return (iBM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iBM_ApprovedBy = Value
        End Set
    End Property
    Public Property BM_ApprovedOn() As DateTime
        Get
            Return (dBM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBM_ApprovedOn = Value
        End Set
    End Property
    Public Property BM_DeletedBy() As Integer
        Get
            Return (iBM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iBM_DeletedBy = Value
        End Set
    End Property
    Public Property BM_DeletedOn() As DateTime
        Get
            Return (dBM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBM_DeletedOn = Value
        End Set
    End Property
    Public Property BM_UpdatedBy() As Integer
        Get
            Return (iBM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBM_UpdatedBy = Value
        End Set
    End Property
    Public Property BM_UpdatedOn() As DateTime
        Get
            Return (dBM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBM_UpdatedOn = Value
        End Set
    End Property


    Public Property CSM_SubGL() As Integer
        Get
            Return (iCSM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iCSM_SubGL = Value
        End Set
    End Property
    Public Property CSM_GL() As Integer
        Get
            Return (iCSM_GL)
        End Get
        Set(ByVal Value As Integer)
            iCSM_GL = Value
        End Set
    End Property
    Public Property CSM_SubGroup() As Integer
        Get
            Return (iCSM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iCSM_SubGroup = Value
        End Set
    End Property
    Public Property CSM_Group() As Integer
        Get
            Return (iCSM_Group)
        End Get
        Set(ByVal Value As Integer)
            iCSM_Group = Value
        End Set
    End Property
    Public Property CSM_ProductDescription() As String
        Get
            Return (sCSM_ProductDescription)
        End Get
        Set(ByVal Value As String)
            sCSM_ProductDescription = Value
        End Set
    End Property
    Public Property CSM_City() As Integer
        Get
            Return (iCSM_City)
        End Get
        Set(ByVal Value As Integer)
            iCSM_City = Value
        End Set
    End Property
    Public Property CSM_Fax() As String
        Get
            Return (sCSM_Fax)
        End Get
        Set(ByVal Value As String)
            sCSM_Fax = Value
        End Set
    End Property
    Public Property CSM_LandLineNo() As String
        Get
            Return (sCSM_LandLineNo)
        End Get
        Set(ByVal Value As String)
            sCSM_LandLineNo = Value
        End Set
    End Property
    Public Property CSM_MobileNo() As String
        Get
            Return (sCSM_MobileNo)
        End Get
        Set(ByVal Value As String)
            sCSM_MobileNo = Value
        End Set
    End Property
    Public Property CSM_EmailID() As String
        Get
            Return (sCSM_EmailID)
        End Get
        Set(ByVal Value As String)
            sCSM_EmailID = Value
        End Set
    End Property
    Public Property CSM_ID() As Integer
        Get
            Return (iCSM_ID)
        End Get
        Set(ByVal Value As Integer)
            iCSM_ID = Value
        End Set
    End Property
    Public Property CSM_IndType() As Integer
        Get
            Return (iCSM_IndType)
        End Get
        Set(ByVal Value As Integer)
            iCSM_IndType = Value
        End Set
    End Property
    Public Property CSM_CustName() As String
        Get
            Return (sCSM_CustName)
        End Get
        Set(ByVal Value As String)
            sCSM_CustName = Value
        End Set
    End Property
    Public Property CSM_Name() As String
        Get
            Return (sCSM_Name)
        End Get
        Set(ByVal Value As String)
            sCSM_Name = Value
        End Set
    End Property
    Public Property CSM_Code() As String
        Get
            Return (sCSM_Code)
        End Get
        Set(ByVal Value As String)
            sCSM_Code = Value
        End Set
    End Property

    Public Property CSM_Inventry() As Integer
        Get
            Return (iCSM_Inventry)
        End Get
        Set(ByVal Value As Integer)
            iCSM_Inventry = Value
        End Set
    End Property

    Public Property CSM_ContactPerson() As String
        Get
            Return (sCSM_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sCSM_ContactPerson = Value
        End Set
    End Property

    Public Property CSM_Address() As String
        Get
            Return (sCSM_Address)
        End Get
        Set(ByVal Value As String)
            sCSM_Address = Value
        End Set
    End Property
    Public Property CSM_State() As Integer
        Get
            Return (iCSM_State)
        End Get
        Set(ByVal Value As Integer)
            iCSM_State = Value
        End Set
    End Property
    Public Property CSM_Pincode() As String
        Get
            Return (sCSM_Pincode)
        End Get
        Set(ByVal Value As String)
            sCSM_Pincode = Value
        End Set
    End Property

    Public Property CSM_Delflag() As String
        Get
            Return (sCSM_Delflag)
        End Get
        Set(ByVal Value As String)
            sCSM_Delflag = Value
        End Set
    End Property
    Public Property CSM_CompID() As Integer
        Get
            Return (iCSM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCSM_CompID = Value
        End Set
    End Property
    Public Property CSM_Status() As String
        Get
            Return (sCSM_Status)
        End Get
        Set(ByVal Value As String)
            sCSM_Status = Value
        End Set
    End Property

    Public Property CSM_Operation() As String
        Get
            Return (sCSM_Operation)
        End Get
        Set(ByVal Value As String)
            sCSM_Operation = Value
        End Set
    End Property
    Public Property CSM_IPAddress() As String
        Get
            Return (sCSM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCSM_IPAddress = Value
        End Set
    End Property
    Public Property CSM_CreatedBy() As Integer
        Get
            Return (iCSM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_CreatedBy = Value
        End Set
    End Property
    Public Property CSM_CreatedOn() As DateTime
        Get
            Return (dCSM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_CreatedOn = Value
        End Set
    End Property

    Public Property CSM_ApprovedBy() As Integer
        Get
            Return (iCSM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_ApprovedBy = Value
        End Set
    End Property
    Public Property CSM_ApprovedOn() As DateTime
        Get
            Return (dCSM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_ApprovedOn = Value
        End Set
    End Property

    Public Property CSM_DeletedBy() As Integer
        Get
            Return (iCSM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_DeletedBy = Value
        End Set
    End Property

    Public Property CSM_DeletedOn() As DateTime
        Get
            Return (dCSM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_DeletedOn = Value
        End Set
    End Property

    Public Property CSM_UpdatedBy() As Integer
        Get
            Return (iCSM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_UpdatedBy = Value
        End Set
    End Property
    Public Property CSM_UpdatedOn() As DateTime
        Get
            Return (dCSM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_UpdatedOn = Value
        End Set
    End Property
    Public Property CSM_Address1() As String
        Get
            Return (sCSM_Address1)
        End Get
        Set(ByVal Value As String)
            sCSM_Address1 = Value
        End Set
    End Property
    Public Property CSM_Address2() As String
        Get
            Return (sCSM_Address2)
        End Get
        Set(ByVal Value As String)
            sCSM_Address2 = Value
        End Set
    End Property
    Public Property CSM_Address3() As String
        Get
            Return (sCSM_Address3)
        End Get
        Set(ByVal Value As String)
            sCSM_Address3 = Value
        End Set
    End Property


    Public Property iUsrPurchaseModule() As Integer
        Get
            Return (Usr_PurchaseModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseModule = Value
        End Set
    End Property
    Public Property iUsrPurchaseRole() As Integer
        Get
            Return (Usr_PurchaseRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseRole = Value
        End Set
    End Property
    Public Property iUsrSalesRole() As Integer
        Get
            Return (Usr_SalesRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesRole = Value
        End Set
    End Property
    Public Property iUsrSalesModule() As Integer
        Get
            Return (Usr_SalesModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesModule = Value
        End Set
    End Property
    Public Property iUsrAccountsRole() As Integer
        Get
            Return (Usr_AccountsRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsRole = Value
        End Set
    End Property

    Public Property iUsrAccountsModule() As Integer
        Get
            Return (Usr_AccountsModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsModule = Value
        End Set
    End Property

    Public Property iUsrMasterModule() As Integer
        Get
            Return (Usr_MasterModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterModule = Value
        End Set
    End Property
    Public Property iUsrMasterRole() As Integer
        Get
            Return (Usr_MasterRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterRole = Value
        End Set
    End Property
    Public Property iUsrRole() As Integer
        Get
            Return (Usr_Role)
        End Get
        Set(ByVal Value As Integer)
            Usr_Role = Value
        End Set
    End Property
    Public Property iUsrCompID() As Integer
        Get
            Return (Usr_CompId)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompId = Value
        End Set
    End Property
    Public Property sUsrStatus() As String
        Get
            Return (Usr_Status)
        End Get
        Set(ByVal Value As String)
            Usr_Status = Value
        End Set
    End Property
    Public Property sUsrFlag() As String
        Get
            Return (Usr_Flag)
        End Get
        Set(ByVal Value As String)
            Usr_Flag = Value
        End Set
    End Property
    Public Property dUsrRecallOn() As Date
        Get
            Return (Usr_RecallOn)
        End Get
        Set(ByVal Value As Date)
            Usr_RecallOn = Value
        End Set
    End Property
    Public Property iUsrRecallBy() As Integer
        Get
            Return (Usr_RecallBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_RecallBy = Value
        End Set
    End Property
    Public Property dUsrDeletedOn() As Date
        Get
            Return (Usr_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_DeletedOn = Value
        End Set
    End Property
    Public Property iUsrDeletedBy() As Integer
        Get
            Return (Usr_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_DeletedBy = Value
        End Set
    End Property
    Public Property dUsrAppOn() As Date
        Get
            Return (Usr_AppOn)
        End Get
        Set(ByVal Value As Date)
            Usr_AppOn = Value
        End Set
    End Property
    Public Property iUsrAppBy() As Integer
        Get
            Return (Usr_AppBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_AppBy = Value
        End Set
    End Property
    Public Property dUsrUpdatedOn() As Date
        Get
            Return (Usr_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_UpdatedOn = Value
        End Set
    End Property
    Public Property iUsrUpdatedBy() As Integer
        Get
            Return (Usr_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_UpdatedBy = Value
        End Set
    End Property
    Public Property dUsrCreatedOn() As Date
        Get
            Return (Usr_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_CreatedOn = Value
        End Set
    End Property
    Public Property iUsrCreatedBy() As Integer
        Get
            Return (Usr_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_CreatedBy = Value
        End Set
    End Property
    Public Property dUsrLastLoginDate() As Date
        Get
            Return (Usr_LastLoginDate)
        End Get
        Set(ByVal Value As Date)
            Usr_LastLoginDate = Value
        End Set
    End Property
    Public Property iUsrNoOfLogin() As Integer
        Get
            Return (Usr_NoOfLogin)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfLogin = Value
        End Set
    End Property
    Public Property iUsrPartner() As Integer
        Get
            Return (Usr_Partner)
        End Get
        Set(ByVal Value As Integer)
            Usr_Partner = Value
        End Set
    End Property
    Public Property iUsrSentMail() As Integer
        Get
            Return (Usr_SentMail)
        End Get
        Set(ByVal Value As Integer)
            Usr_SentMail = Value
        End Set
    End Property
    Public Property sUsrAns() As String
        Get
            Return (Usr_Ans)
        End Get
        Set(ByVal Value As String)
            Usr_Ans = Value
        End Set
    End Property
    Public Property sUsrQues() As String
        Get
            Return (Usr_Ques)
        End Get
        Set(ByVal Value As String)
            Usr_Ques = Value
        End Set
    End Property
    Public Property iUsrNoOfUnsucsfAtteptts() As Integer
        Get
            Return (Usr_NoOfUnsucsfAtteptts)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfUnsucsfAtteptts = Value
        End Set
    End Property
    Public Property iUsrGrpOrUserLvlPerm() As Integer
        Get
            Return (Usr_GrpOrUserLvlPerm)
        End Get
        Set(ByVal Value As Integer)
            Usr_GrpOrUserLvlPerm = Value
        End Set
    End Property
    Public Property iUsrOrgID() As Integer
        Get
            Return (Usr_OrgID)
        End Get
        Set(ByVal Value As Integer)
            Usr_OrgID = Value
        End Set
    End Property
    Public Property iUsrCompanyID() As Integer
        Get
            Return (Usr_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompanyID = Value
        End Set
    End Property
    Public Property iUsrDesignation() As Integer
        Get
            Return (Usr_Designation)
        End Get
        Set(ByVal Value As Integer)
            Usr_Designation = Value
        End Set
    End Property
    Public Property sUsrOffPhExtn() As String
        Get
            Return (Usr_OffPhExtn)
        End Get
        Set(ByVal Value As String)
            Usr_OffPhExtn = Value
        End Set
    End Property
    Public Property sUsrOfficePhone() As String
        Get
            Return (Usr_OfficePhone)
        End Get
        Set(ByVal Value As String)
            Usr_OfficePhone = Value
        End Set
    End Property
    Public Property sUsrPhoneNo() As String
        Get
            Return (Usr_PhoneNo)
        End Get
        Set(ByVal Value As String)
            Usr_PhoneNo = Value
        End Set
    End Property
    Public Property sUsrMobileNo() As String
        Get
            Return (Usr_MobileNo)
        End Get
        Set(ByVal Value As String)
            Usr_MobileNo = Value
        End Set
    End Property
    Public Property sUsrDutyStatus() As String
        Get
            Return (Usr_DutyStatus)
        End Get
        Set(ByVal Value As String)
            Usr_DutyStatus = Value
        End Set
    End Property
    Public Property iUsrLevelGrp() As Integer
        Get
            Return (Usr_LevelGrp)
        End Get
        Set(ByVal Value As Integer)
            Usr_LevelGrp = Value
        End Set
    End Property
    Public Property sUsrEmail() As String
        Get
            Return (Usr_Email)
        End Get
        Set(ByVal Value As String)
            Usr_Email = Value
        End Set
    End Property
    Public Property sUsrPassword() As String
        Get
            Return (Usr_Password)
        End Get
        Set(ByVal Value As String)
            Usr_Password = Value
        End Set
    End Property
    Public Property sUsrLoginName() As String
        Get
            Return (Usr_LoginName)
        End Get
        Set(ByVal Value As String)
            Usr_LoginName = Value
        End Set
    End Property
    Public Property sUsrFullName() As String
        Get
            Return (Usr_FullName)
        End Get
        Set(ByVal Value As String)
            Usr_FullName = Value
        End Set
    End Property
    Public Property sUsrCode() As String
        Get
            Return (Usr_Code)
        End Get
        Set(ByVal Value As String)
            Usr_Code = Value
        End Set
    End Property
    Public Property iUsrNode() As Integer
        Get
            Return (Usr_Node)
        End Get
        Set(ByVal Value As Integer)
            Usr_Node = Value
        End Set
    End Property
    Public Property iUserID() As Integer
        Get
            Return (Usr_ID)
        End Get
        Set(ByVal Value As Integer)
            Usr_ID = Value
        End Set
    End Property
    Public Property sUsrIPAdress() As String
        Get
            Return (Usr_IPAdress)
        End Get
        Set(ByVal Value As String)
            Usr_IPAdress = Value
        End Set
    End Property
    Public Property sUsrAddress() As String
        Get
            Return (Usr_Address)
        End Get
        Set(ByVal Value As String)
            Usr_Address = Value
        End Set
    End Property
    Public Function FindCityId(ByVal sNameSpace As String, ByVal iMas_MasterID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Mas_Id  from acc_General_Master"
            If iMas_MasterID <> "" Then
                sSql = sSql & " where Mas_desc like '" & iMas_MasterID & "%'"
            End If
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CreateRegion(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iUserID As Integer, ByVal sDescription As String, ByVal iType As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iRegion As Integer = 0, iMaxID As Integer = 0
        Dim objGenFun As New clsGeneralFunctions
        Try
            sSql = "Select *  from acc_General_Master where Mas_Desc = '" & objGen.SafeSQL(Trim(sDescription)) & "' and "
            sSql = sSql & "Mas_Master = " & iType & " and Mas_CompID = " & iCOmpID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                iRegion = dt.Rows(0)("Mas_Id")
            Else
                iMaxID = objGenFun.GetMaxID(sNameSpace, iCOmpID, "acc_General_Master", "Mas_id", "Mas_CompID")
                sSql = "" : sSql = "Insert into acc_General_Master(Mas_id,Mas_Desc,Mas_Delflag,Mas_Master,"
                sSql = sSql & "Mas_Remarks,Mas_CompID,Mas_Status,Mas_CrBy,"
                sSql = sSql & "Mas_CrOn,Mas_AppBy,Mas_AppOn,Mas_IPAddress,Mas_Operation)Values(" & iMaxID & ",'" & objGen.SafeSQL(Trim(sDescription)) & "','A'," & iType & ","
                sSql = sSql & "'" & objGen.SafeSQL(Trim(sDescription)) & "'," & iCOmpID & ",'A'," & iUserID & ","
                sSql = sSql & "GetDate()," & iUserID & ",GetDate(),'','C')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                iRegion = iMaxID
            End If
            Return iRegion
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCustomerMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objParty As clsExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Type", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACM_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Name", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Head", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Group", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SubGroup", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_GL", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SubGL", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CreatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CreatedOn", OleDb.OleDbType.Date, 100)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedOn", OleDb.OleDbType.Date, 10)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ApprovedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ApprovedOn", OleDb.OleDbType.Date, 20)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_DeletedBy", OleDb.OleDbType.Integer, 20)
            ObjParam(iParamCount).Value = objParty.iACM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_DeletedOn", OleDb.OleDbType.Date, 20)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IpAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACM_IPAddrs
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Operations", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveAddressDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objParty As clsExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACAD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACAD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Address", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objParty.sACAD_Addr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_City", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_State", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Country", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_Country
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Pincode", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ContactPerson", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objParty.sACAD_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACAD_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MobileNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Landline", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_Landline
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Fax", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACAD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CreatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CreatedOn", OleDb.OleDbType.Date, 100)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Address_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function SaveCustomerAddrsDetails(ByVal objCD As clsExcelUpload, ByVal sNameSpace As String) As Array
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try

    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_ID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MasterID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_MasterID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Address", OleDb.OleDbType.VarChar, 200)
    '        ObjParam(iParamCount).Value = objCD.sACAD_Addr
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_City", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_City
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_State ", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_State
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Country ", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_Country
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Pincode ", OleDb.OleDbType.Integer, 10)
    '        ObjParam(iParamCount).Value = objCD.iACAD_Pincode
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ContactPerson ", OleDb.OleDbType.VarChar, 20)
    '        ObjParam(iParamCount).Value = objCD.sACAD_ContactPerson
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Email ", OleDb.OleDbType.VarChar, 50)
    '        ObjParam(iParamCount).Value = objCD.sACAD_Email
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MobileNo", OleDb.OleDbType.VarChar, 12)
    '        ObjParam(iParamCount).Value = objCD.sACAD_MobileNo
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Landline  ", OleDb.OleDbType.VarChar, 20)
    '        ObjParam(iParamCount).Value = objCD.sACAD_Landline
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Fax", OleDb.OleDbType.VarChar, 20)
    '        ObjParam(iParamCount).Value = objCD.sACAD_Fax
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CompID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objCD.iACAD_CompID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Status", OleDb.OleDbType.VarChar, 4)
    '        ObjParam(iParamCount).Value = objCD.sACAD_Status
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAM_CreatedBy", OleDb.OleDbType.Integer, 5)
    '        ObjParam(iParamCount).Value = objCD.iACAD_CreatedBy
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Address_Details", 1, Arr, ObjParam)
    '        Return Arr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Try
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Description")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iCompID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) order by gl_glcode"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                While dr.Read
                    dRow = dt.NewRow()
                    dRow("SLNo") = i + 1

                    If IsDBNull(dr("gl_glcode")) = False Then
                        dRow("GLCode") = dr("gl_glcode")
                    End If

                    If IsDBNull(dr("gl_Desc")) = False Then
                        dRow("Description") = dr("gl_Desc")
                    End If

                    dt.Rows.Add(dRow)
                    i = i + 1
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubLedgerOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Try
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("Description")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("Date")
            dt.Columns.Add("Due on")
            dt.Columns.Add("OverDue By Days")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_compid=" & iCompID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=2 or gl_head=3) order by gl_glcode"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                While dr.Read
                    dRow = dt.NewRow()
                    dRow("SLNo") = i + 1

                    If IsDBNull(dr("gl_glcode")) = False Then
                        dRow("GLCode") = dr("gl_glcode")
                    End If

                    If IsDBNull(dr("gl_Desc")) = False Then
                        dRow("Description") = dr("gl_Desc")
                    End If

                    dt.Rows.Add(dRow)
                    i = i + 1
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveSubLedgerOpeningBalanceJE(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal objOP As clsExcelUpload)
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from acc_Transactions_Details")
            sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
            sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
            sSql = sSql & "ATD_GL,ATD_SubGL,ATD_DbOrCr,ATD_Debit,ATD_Credit,"
            sSql = sSql & "ATD_CreatedBy,ATD_CreatedOn,ATD_Status,"
            sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
            sSql = sSql & "Values(" & iMax & ",GetDate(),7,"
            sSql = sSql & "" & iSubOpn_BilLID & ",3," & objOP.iSubOpn_AccHead & ","
            sSql = sSql & "" & objOP.iSubOpn_GlId & "," & objOP.iSubOpn_SubGlId & "," & objOP.iSubOpn_DabitOrCredit & "," & objOP.dSubOpn_DebitAmt & "," & objOP.dSubOpn_CreditAmount & ","
            sSql = sSql & "" & iUserID & ",GetDate(),'A',"
            sSql = sSql & "" & objOP.iSubOpn_YearId & "," & iCompID & ",'C','" & objOP.sSubOpn_IPAddress & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Return 1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubLedgerOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal objOP As clsExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_SerialNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_TransctionNo ", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOP.SubOpn_sTransctionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSubOpn_AccHead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GLCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOP.sSubOpn_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DebitAmt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubOpn_DebitAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreditAmount", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubOpn_CreditAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_SubGlId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOP.sSubOpn_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ReferenceNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOP.sSubOpn_ReferenceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_BilDate", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = objOP.dSubBillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_PendingAmnt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubPenidingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DueOnDate", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = objOP.dSubDueOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_OverDueDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_OverDueDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_LedgerOpening_Balance", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePartyDetails(ByVal sNameSpace As String, ByVal objclsExcel As clsExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(39) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Code", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Inventry", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_Inventry
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_State", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Pincode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dBM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dBM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dBM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dBM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ContactPerson", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_City ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_LandLineNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sBM_LandLineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_MobileNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sBM_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sBM_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Fax", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Operation", OleDb.OleDbType.VarChar, 100)
            'ObjParam(iParamCount).Value = objclsExcel.sBM_Operation
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sBM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address1", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Address1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address2", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Address2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address3", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sBM_Address3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GenCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_GenCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GSTNRegNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsExcel.sBM_GSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iBM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSalesPartyMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveVATPANtable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sIPAddress As String, ByVal sCode As String, ByVal TINNo As String)
        Dim ssql As String
        Dim iBM_Id As Integer
        Dim sDescCode As String = ""
        Dim sSubGrp As String = ""
        Dim iBuyerExistingID, iBuyerPKID As Integer

        Try
            iBM_Id = objDBL.SQLExecuteScalarInt(sNameSpace, "select BM_Id from Sales_Buyers_Masters where BM_Code='" & sCode & "' and BM_CompId=" & iCompID & " ")

            iBuyerExistingID = objDBL.SQLExecuteScalarInt(sNameSpace, "select Buyer_PKId from Sales_Buyer_Accounting_Template where Buyer_ID=" & iBM_Id & " And Buyer_Desc='TIN' and Buyer_CompId=" & iCompID & " ")
            If iBuyerExistingID > 0 Then
                ssql = "" : ssql = "Update Sales_Buyer_Accounting_Template Set Buyer_Value='" & TINNo & "' Where Buyer_pkID=" & iBuyerExistingID & " And Buyer_ID=" & iBM_Id & " And Buyer_Desc='TIN' And Buyer_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            Else
                iBuyerPKID = objDBL.SQLExecuteScalar(sNameSpace, "Select  IsNull(MAX(Buyer_pkID),0)+1 from Sales_Buyer_Accounting_Template")
                ssql = "" : ssql = "Insert Into Sales_Buyer_Accounting_Template (Buyer_pkID,Buyer_ID,Buyer_Desc,Buyer_Value,Buyer_Status,Buyer_CompID)"
                ssql = ssql & "Values(" & iBuyerPKID & "," & iBM_Id & ",'TIN','" & TINNo & "','W'," & iCompID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCity As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 3"
            Else
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 4"
            End If

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
    Public Function SaveCumsterSupplierDetails(ByVal sNameSpace As String, ByVal objclsExcel As clsExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(39) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Code", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Inventry", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_Inventry
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ContactPerson", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_EmailID", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_MobileNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_LandLineNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_LandLineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Fax", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address1", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address2", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Address2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address3", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Address3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Pincode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_City ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_State", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.sCSM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dCSM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dCSM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dCSM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsExcel.dCSM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ProductDescription", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsExcel.CSM_ProductDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_GSTNRegNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_GSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iCSM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCustomerSupplierMasterUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatutoryNameValueID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String) As String
        Dim sSql As String
        Dim iID As Integer
        Try
            sSql = "Select Cmp_PKID From Company_Accounting_Template Where Cmp_ID=" & iCompID & " And Cmp_Desc='" & sStatutoryName & "'"
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveStatutoryNameValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String, ByVal sStatutoryValue As String, ByVal iID As Integer)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            'clsCustomerMaster.GetMaxIDCmpValue(sNameSpace, iCompID, "Company_Accounting_Template", "Cmp_PKID", "Cmp_ID")
            If iID = 0 Then
                iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Company_Accounting_Template", "Cmp_PKID", "Cmp_ID")
                sSql = "" : sSql = "Insert Into Company_Accounting_Template (Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_ID,Cmp_Status) values"
                sSql = sSql & "(" & iMaxID & ",'" & RemoveQuote(sStatutoryName) & "','" & RemoveQuote(sStatutoryValue) & "'," & iCompID & ",'W')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iID > 0 Then
                sSql = "" : sSql = "Update Company_Accounting_Template Set Cmp_Desc= '" & RemoveQuote(sStatutoryName) & "',Cmp_Value='" & RemoveQuote(sStatutoryValue) & "',Cmp_Status='U'"
                sSql = sSql & " Where Cmp_ID = " & iCompID & " And Cmp_PKID=  " & iID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmployeeDetails(ByVal sAC As String, ByVal objclsExcel As clsExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(33) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Node", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrNode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_FullName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsExcel.sUsrFullName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LoginName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsExcel.sUsrLoginName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Password", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrPassword
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Email", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsExcel.sUsrEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LevelGrp", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrLevelGrp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DutyStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsExcel.sUsrDutyStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PhoneNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrPhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MobileNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrMobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OfficePhone", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrOfficePhone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OffPhExtn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsExcel.sUsrOffPhExtn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Designation", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrDesignation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OrgnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrOrgID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_GrpOrUserLvlPerm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrGrpOrUserLvlPerm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Role", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrMasterModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrPurchaseModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrSalesModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrAccountsModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrMasterRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrPurchaseRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrSalesRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrAccountsRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsExcel.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsExcel.sUsrFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objclsExcel.sUsrStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_IPAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsExcel.Usr_IPAdress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CompId", OleDb.OleDbType.Integer, 50)
            ObjParam(iParamCount).Value = objclsExcel.iUsrCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Address", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsExcel.sUsrAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEmployeeMasterUserUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateJECode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""

        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 Acc_SLJE_ID From Acc_Ledger_JE_Master where Acc_SLJE_CompID = " & iCompID & " ")
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

    Public Function GetPOSubGlid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPOGlid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select gl_parent from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_AccHead from Chart_of_Accounts where GL_GLCode ='" & sCode & "' and gl_CompID = " & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function loadSubledgerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0
        Try
            dt.Columns.Add("sOpn_GLCode")
            dt.Columns.Add("dOpn_DebitAmt")
            dt.Columns.Add("dOpn_CreditAmount")
            dt.Columns.Add("dOpn_Date")
            dt.Columns.Add("iOpn_YearId")
            dt.Columns.Add("iOpn_CompId")
            dt.Columns.Add("iOpn_GlId")
            dt.Columns.Add("sOpn_IPAddress")
            dt.Columns.Add("iOpn_AccHead")

            sSql = "" : sSql = "select * from Acc_Ledger_JE_Master where Acc_SLJE_CompID=" & iCompID & " and Acc_SLJE_Year=" & iYearID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                While dr.Read
                    dRow = dt.NewRow()

                    If IsDBNull(dr("Acc_SLJE_GLCode")) = False Then
                        dRow("sOpn_GLCode") = dr("Acc_SLJE_GLCode")
                    End If

                    If IsDBNull(dr("Acc_SLJE_Debit")) = False Then
                        dRow("dOpn_DebitAmt") = dr("Acc_SLJE_Debit")
                    End If
                    If IsDBNull(dr("Acc_SLJE_Credit")) = False Then
                        dRow("dOpn_CreditAmount") = dr("Acc_SLJE_Credit")
                    End If

                    If IsDBNull(dr("Acc_SLJE_BillDate")) = False Then
                        dRow("dOpn_Date") = dr("Acc_SLJE_BillDate")
                    End If

                    If IsDBNull(dr("Acc_SLJE_Year")) = False Then
                        dRow("iOpn_YearId") = dr("Acc_SLJE_Year")
                    End If

                    If IsDBNull(dr("Acc_SLJE_CompID")) = False Then
                        dRow("iOpn_CompId") = dr("Acc_SLJE_CompID")
                    End If


                    If IsDBNull(dr("Acc_SLJE_GLID")) = False Then
                        dRow("iOpn_GlId") = dr("Acc_SLJE_GLID")
                    End If

                    If IsDBNull(dr("Acc_SLJE_IPAddress")) = False Then
                        dRow("sOpn_IPAddress") = dr("Acc_SLJE_IPAddress")
                    End If
                    If IsDBNull(dr("Acc_SLJE_AccHead")) = False Then
                        dRow("iOpn_AccHead") = dr("Acc_SLJE_AccHead")
                    End If

                    dt.Rows.Add(dRow)
                    i = i + 1
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function RemoveDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("sOpn_GLCode"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("sOpn_GLCode"), String.Empty)
                End If
            Next
            For Each DataRow As DataRow In duplicateList
                dt.Rows.Remove(DataRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTotalCreditAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As String) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try

            sSql = "" : sSql = "select sum(Acc_SLJE_Credit) from Acc_Ledger_JE_Master where Acc_SLJE_GLCode='" & GlCode & "' "
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalDabitAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As String) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try

            sSql = "" : sSql = "select sum(Acc_SLJE_Debit) from Acc_Ledger_JE_Master where Acc_SLJE_GLCode='" & GlCode & "' "
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckSupplierExistOrnot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iSubGL As Integer = 0
        Try
            sSql = "" : sSql = "Select * from CustomerSupplierMaster where CSM_Code ='" & sCode & "' and CSM_DelFlag='A' and CSM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                iSubGL = dt.Rows(0)("CSM_SubGL").ToString()
            Else
                iSubGL = 0
            End If
            Return iSubGL
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDescription As String, ByVal iGl As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update Chart_of_Accounts set gl_Desc ='" & objGen.SafeSQL(sDescription) & "',gl_Reason_Creation='" & objGen.SafeSQL(sDescription) & "' "
            sSql = sSql & "Where gl_id =" & iGl & " and gl_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckCustomersExistOrnot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iSubGL As Integer = 0
        Try
            sSql = "" : sSql = "Select * from sales_Buyers_Masters where BM_Code ='" & sCode & "' and BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                iSubGL = dt.Rows(0)("BM_SubGL").ToString()
            Else
                iSubGL = 0
            End If
            Return iSubGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGSTDetails(ByVal sNameSpace As String, ByVal objclsEx As clsExcelUpload.GST) As Object
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_GSTM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_GSTM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Schedule_Type", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_Schedule_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_SlnoOfSchedule", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objclsEx.AGS_SlnoOfSchedule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_CHST", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsEx.AGS_CHST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Chapter", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objclsEx.AGS_Chapter
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Heading", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objclsEx.AGS_Heading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_SubHeading", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objclsEx.AGS_SubHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Tarrif", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objclsEx.AGS_Tarrif
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_GoodDescription", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsEx.AGS_GoodDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_NotificationNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEx.AGS_NotificationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_NotificationDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEx.AGS_NotificationDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_FileNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsEx.AGS_FileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_FileDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEx.AGS_FileDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_Createdby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objclsEx.AGS_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Status", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsEx.AGS_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsEx.AGS_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsEx.AGS_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsEx.AGS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_GST_Schedule", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGSTMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objclsEx As clsExcelUpload.GST) As Integer
        Dim sSql As String = ""
        Dim GSTM_ID As Integer
        Dim bCheck As Boolean
        Dim iReturn As Integer
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_GST_Schedule_Master Where GSTM_NotificationNo='" & objclsEx.AGS_NotificationNo & "' And GSTM_CompID=" & iCompID & " ")
            If bCheck = True Then
                iReturn = objDBL.SQLExecuteScalarInt(sNameSpace, "Select GSTM_ID From Acc_GST_Schedule_Master Where GSTM_NotificationNo='" & objclsEx.AGS_NotificationNo & "' And GSTM_CompID=" & iCompID & " ")
            Else
                GSTM_ID = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(GSTM_ID)+1,1) from Acc_GST_Schedule_Master")
                sSql = "Insert Into Acc_GST_Schedule_Master(GSTM_ID,GSTM_NotificationNo,GSTM_NotificationDate,GSTM_FileNo,GSTM_FileDate,GSTM_Status,GSTM_CreatedBy,GSTM_CreatedOn,GSTM_CompID,GSTM_YearID,GSTM_Operation,GSTM_IPAddress)"
                sSql = sSql & "Values(" & GSTM_ID & ",'" & objclsEx.AGS_NotificationNo & "'," & objGen.FormatDtForRDBMS(objclsEx.AGS_NotificationDate, "I") & ",'" & objclsEx.AGS_FileNo & "'," & objGen.FormatDtForRDBMS(objclsEx.AGS_FileDate, "I") & ",'" & objclsEx.AGS_Status & "'," & objclsEx.AGS_Createdby & "," & objclsEx.AGS_CreatedOn & "," & objclsEx.AGS_CompID & "," & objclsEx.AGS_YearID & ",'" & objclsEx.AGS_Operation & "','" & objclsEx.AGS_IPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                iReturn = GSTM_ID
            End If
            Return iReturn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSubLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID,GL_Desc from chart_of_Accounts where gl_compid=" & iCompID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=3) order by GL_Desc"
            BindSubLedger = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindSubLedger
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpBalDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSubGLID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From ACC_Opening_Balance Where Opn_GLID=" & iSubGLID & " And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " And Opn_Status='A'"
            GetOpBalDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetOpBalDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveOpBal(ByVal sNameSpace As String, ByVal iCompID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Truncate Table ACC_Opening_Balance"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSubGlID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from chart_of_Accounts where GL_ID=" & iSubGlID & " And gl_compid=" & iCompID & " And gl_delflag='C' and gl_Status ='A' and (gl_head=3) order by GL_Desc"
            GetChartOfAccounts = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetChartOfAccounts
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objclsExcel As clsExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objclsExcel.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objclsExcel.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objclsExcel.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objclsExcel.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objclsExcel.iATD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iSubGlID As Integer, ByVal dAmount As Double, ByVal iDebit As Integer) As Integer
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try
            sSql = "" : sSql = "Select * From Acc_Opening_Balance Where  Opn_GLID=" & iSubGlID & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                'If iDebit = 1 Then
                '    sSql = "" : sSql = "Update Acc_Opening_Balance Set Opn_CreditAmount=" & dAmount & " Where Opn_GLID=" & iSubGlID & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
                '    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                'ElseIf iDebit = 2 Then
                '    sSql = "" : sSql = "Update Acc_Opening_Balance Set Opn_DebitAmt=" & dAmount & " Where Opn_GLID=" & iSubGlID & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
                '    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                'End If
                sSql = "" : sSql = "Update Acc_Opening_Balance Set Opn_BreakUpDone='Y',Opn_BreakUpBy=" & iUserID & ",Opn_BreakUpOn=GetDate() Where Opn_GLID=" & iSubGlID & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                sSql = "" : sSql = "Select Opn_ID From Acc_Opening_Balance Where  Opn_GLID=" & iSubGlID & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
                UpdateOpeningBalance = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return UpdateOpeningBalance
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBreakUp(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sBillNo As String, ByVal dBillDate As Date, ByVal dDebit As String, ByVal dCredit As String, ByVal iOpnID As Integer, ByVal iGLID As Integer)
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Op_BreakUp", "Opb_ID", "Opb_CompID")
            sSql = "Insert into Op_BreakUp(Opb_ID,Opb_OpnID,opb_GLID,Opb_BillNo,Opb_BillDate,Opb_Debit,Opb_Credit,Opb_CompID,Opb_YearID,Opb_Cry,Opb_CrOn) "
            sSql = sSql & "Values(" & iMaxID & "," & iOpnID & "," & iGLID & ",'" & sBillNo & "'," & objGen.FormatDtForRDBMS(dBillDate, "I") & ",'" & dDebit & "','" & dCredit & "'," & iCompID & "," & iYearID & "," & iUserID & ",GetDate())"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBreakUp(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iOpnID As Integer) As Integer
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try
            sSql = "Select * From Op_BreakUp Where Opb_GLID=" & iOpnID & " And Opb_CompID=" & iCompID & " And Opb_YearID=" & iYearID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                CheckBreakUp = 1
            Else
                CheckBreakUp = 0
            End If
            Return CheckBreakUp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStartDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBillDate As Date) As Integer
        Dim sSql As String = ""
        Dim dStartDate As Date : Dim dBillDate As Date
        Try
            dBillDate = objGen.FormatDtForRDBMS(sBillDate, "D")
            sSql = "Select CONVERT(VARCHAR(10),AS_StartDate,103) from Application_Settings Where AS_CompID=" & iCompID & ""
            dStartDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            If dBillDate > dStartDate Then
                GetStartDate = 1
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSDate(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select CONVERT(VARCHAR(10),AS_StartDate,103) from Application_Settings Where AS_CompID=" & iCompID & ""
            GetSDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetSDate
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
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccZone > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=2 And Org_CompID=" & iCompID & " "
            End If
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
            If iAccRgn > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=3 And Org_CompID=" & iCompID & " "
            End If
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
            If iAccarea > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=4 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function getBreakupStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSubGLID As Integer) As String
        Dim sSql As String = "" : Dim sStatus As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From ACC_Opening_Balance Where Opn_GLID=" & iSubGLID & " And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " And Opn_Status='A'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Opn_BreakUpDone")) = False Then
                    sStatus = dt.Rows(0)("Opn_BreakUpDone")
                Else
                    sStatus = ""
                End If
            End If
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINV_Desc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select INV_Parent from Inventory_Master where INV_Description ='" & iINV_Desc & "' and INV_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINV_Desc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select INV_ID from Inventory_Master where INV_Description ='" & iINV_Desc & "' and INV_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckCompanyTypeExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCompanyType & "' and Mas_Master = 2"
            Else
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCompanyType & "' and Mas_Master = 2"
            End If

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCompanyTypeExistOrNot = dr("Mas_Id")
            Else
                CheckCompanyTypeExistOrNot = 0
            End If
            dr.Close()
            Return CheckCompanyTypeExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckGSTINCategoryExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTINCategory As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from GSTCategory_Table where GC_GSTCategory ='" & sGSTINCategory & "'"
            Else
                sSql = "" : sSql = "Select * from GSTCategory_Table where GC_GSTCategory ='" & sGSTINCategory & "' and GC_CompanyTypeID=" & iMaster & ""
            End If

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckGSTINCategoryExistOrNot = dr("GC_Id")
            Else
                CheckGSTINCategoryExistOrNot = 0
            End If
            dr.Close()
            Return CheckGSTINCategoryExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
