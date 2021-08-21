Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Public Class clsIndexing
    Private objDBL As New DBHelper
    Private Shared iUsrType As Integer
    Dim sPermlvl As String
    Dim dsMain As New DataSet
    Dim iParGrp As Integer = 0
    Dim sCabPerm As String
    Dim Permdt As DataTable
    Private Shared sMem As String = String.Empty
    Dim dtPerm As New DataTable
    Dim iUsrParGrp As Integer = 0
    Private Shared ObjConnection As OleDb.OleDbConnection
    Dim objGnrl As New clsGeneralFunctions

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

    Public Structure BatchScan
        Public BT_ID As Integer
        Public BT_CustomerID As Integer
        Public BT_BatchNo As String
        Public BT_TrType As Integer
        Public BT_NoOfTransaction As Integer
        Public BT_DebitTotal As Double
        Public BT_CreditTotal As Double
        Public BT_Delflag As String
        Public BT_Status As String
        Public BT_CompID As Integer
        Public BT_YearID As Integer
        Public BT_CrBy As Integer
        Public BT_CrOn As DateTime
        Public BT_Operation As String
        Public BT_IPAddress As String
    End Structure

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

    Public Function LoadCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal iUsrID As Integer) As DataTable
        Dim sSql As String
        Dim dtcab As New DataTable
        Try
            sSql = "Select CBN_NODE,CBN_NAME from edt_cabinet where CBN_DelStatus='A' and CBN_Parent=-1 And CBN_PERMISSION <> 1  order by CBN_Name"
            dtcab = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtcab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabinet(ByVal sAC As String, ByVal iACID As Integer, ByVal CabinetId As Integer) As DataTable
        Dim sSql As String
        Dim dtSubcabinet As New DataTable
        Try
            sSql = "Select CBN_NODE,CBN_NAME from edt_cabinet where CBN_DelStatus='A' And CBN_PERMISSION <> 1  and CBN_Parent= " & CabinetId & " order by CBN_Name"
            dtSubcabinet = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtSubcabinet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolder(ByVal sAC As String, ByVal iACID As Integer, ByVal SubCabId As Integer) As DataTable
        Dim sSql As String
        Dim dtFolder As New DataTable
        Try
            sSql = "Select Fol_FolID,FOL_Name from edt_folder where FOL_Status='A' and FOL_Cabinet= " & SubCabId & " order by FOL_Name"
            dtFolder = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtFolder
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDocumentType(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtDocumentType As New DataTable
        Try
            sSql = "Select DOT_DOCTYPEID,DOT_DOCNAME from edt_document_type where DOT_DelFlag='A' order by DOT_DOCNAME"
            dtDocumentType = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDocumentType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescriptors(ByVal sAC As String, ByVal iACID As Integer, ByVal iDocTypeID As Integer) As DataTable
        Dim sSql As String
        Dim dtDescriptors As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("DescriptorID")
            dt.Columns.Add("Descriptor")

            sSql = "select a.des_id,a.Desc_name from EDT_DESCRIPTIOS a,EDT_DOCTYPE_LINK b"
            sSql = sSql & "  where a.des_id=b.edd_dptrid and b.edd_doctypeid= " & iDocTypeID & "  order by a.Desc_name"
            dtDescriptors = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDescriptors.Rows.Count > 0 Then
                For i = 0 To dtDescriptors.Rows.Count - 1
                    dr = dt.NewRow
                    dr("DescriptorID") = dtDescriptors.Rows(i)("des_id")
                    dr("Descriptor") = dtDescriptors.Rows(i)("Desc_name")
                    dt.Rows.Add(dr)
                Next
                Return dt
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadKeyWords() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("Key")
            For i = 0 To 3
                dr = dt.NewRow
                dr("Key") = ""
                dt.Rows.Add(dr)
            Next
            Return dt
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
    Public Function SavePage(ByVal sAC As String, ByVal iACID As Integer, ByVal objIndex As clsIndexing) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CABINET", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_CABINET
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FOLDER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_FOLDER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DOCUMENT_TYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_DOCUMENT_TYPE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_TITLE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.sPGE_TITLE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_DATE", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objIndex.dPGE_DATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_DETAILS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPgeDETAILSID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPge_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OBJECT", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objIndex.sPGE_OBJECT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_PAGENO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_PAGENO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_EXT", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objIndex.sPGE_EXT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_KeyWORD", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objIndex.sPGE_KeyWORD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRText", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.sPGE_OCRText
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SIZE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_SIZE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_CURRENT_VER", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_CURRENT_VER
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.sPGESTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_SubCabinet
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_QC_UsrGrpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_QC_UsrGrpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_FTPStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.sPGE_FTPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_batch_name", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_batch_name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_OrignalFileName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objIndex.spge_OrignalFileName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_BatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_BatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGE_OCRDelFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPGE_OCRDelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Pge_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objIndex.iPge_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@pge_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objIndex.spge_Delflag
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
    Public Function SavePageDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iPagedetailsID As Integer, iDocumenttypeID As Integer, iDescriptorID As Integer, ByVal sKeyWords As String, ByVal sDescValues As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(7) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_BASEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iPagedetailsID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_DOCTYPE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDocumenttypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_DESCID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDescriptorID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_KEYWORD ", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = sKeyWords
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_VALUE", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sDescValues
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EPD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_PAGE_DETAILS", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBatchScanDetails(ByVal sAC As String, ByVal objBatch As clsIndexing.BatchScan)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CustomerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CustomerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_BatchNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBatch.BT_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_NoOfTransaction", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_NoOfTransaction
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_DebitTotal", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objBatch.BT_DebitTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CreditTotal", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CreditTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Delflag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objBatch.BT_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objBatch.BT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBatch.BT_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_CrOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objBatch.BT_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objBatch.BT_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objBatch.BT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spBatchScan_Table", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "" : Dim sStr As String = ""
        Dim sMaximumID As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 BT_ID From BatchScan_Table Order By BT_ID Desc")
            sStr = "BS-" & sMaximumID + 1
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomers(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select BM_ID,BM_Code + ' - ' + BM_Name as Name  from sales_Buyers_Masters where BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            sSql = "Select MDA_ID,MDA_CompanyName from MMCSPL_DB_Access"
            dt = objDBL.SQLExecuteDataTable("MMCSPL", sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBatchNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer, ByVal iTrType As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select BT_ID,BT_BatchNO From BatchScan_Table Where BT_CustomerID=" & iCustomerID & " And BT_TrType=" & iTrType & " And BT_CompID=" & iCompID & " And BT_YearID=" & iYearID & " "
            BindBatchNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDOCTYPEID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where DOT_DOCNAME='Voucher' And DOT_CompID=" & iCompID & " "
            GetDOCTYPEID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDOCTYPEID
        Catch ex As Exception
            Throw
        End Try
    End Function
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
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
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
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "EDT_CABINET", "CBN_NODE")
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
                iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "edt_folder", "FOL_FOLID")
                sSql = "" : sSql = "Insert Into edt_folder(FOL_FOLID,FOL_CABINET,FOL_NAME,FOL_NOTES,FOL_CRBY,FOL_STATUS,FOL_PAGECOUNT,fol_operation,fol_operationBy) "
                sSql = sSql & "Values(" & iMaxID & "," & iCabinetID & ",'" & sFolderName & "','" & sFolderName & "'," & iUserID & ",'A',0,'I'," & iUserID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                GetFolderID = iMaxID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetDocumentTypeID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iCabinetID As Integer, ByVal sFolderName As String) As Integer
    '    Dim bCheck As Boolean
    '    Dim sSql As String = ""
    '    Dim iMaxID As Integer
    '    Try
    '        sSql = "" : sSql = "select * from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
    '        bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
    '        If bCheck = True Then
    '            sSql = "" : sSql = "select FOL_FOLID from edt_folder where FOL_NAME='" & sFolderName & "' And FOL_CABINET=" & iCabinetID & " "
    '            GetDocumentTypeID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Else
    '            iMaxID = objGnrl.GetEdictMaxID(sNameSpace, iCompID, "edt_folder", "FOL_FOLID")
    '            sSql = "" : sSql = "Insert Into edt_folder(FOL_FOLID,FOL_CABINET,FOL_NAME,FOL_NOTES,FOL_CRBY,FOL_STATUS,FOL_PAGECOUNT,fol_operation,fol_operationBy) "
    '            sSql = sSql & "Values(" & iMaxID & "," & iCabinetID & ",'" & sFolderName & "','" & sFolderName & "'," & iUserID & ",'W',0,'I'," & iUserID & ")"
    '            GetDocumentTypeID = iMaxID
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
End Class
