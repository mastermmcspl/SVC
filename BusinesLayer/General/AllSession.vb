Public Structure AllSession
    Private iAccessCodeID As Integer
    Private sAccessCode As String
    Private sEncryptPassword As String
    Private iYearID As Integer
    Private sYearName As String
    Private iUserID As Integer
    Private sUserFullName As String
    Private sUserLoginName As String
    Private sUserFullNameCode As String
    Private sLastLoginDate As String
    Private sIPAddress As String
    Private iFileSize As Integer
    Private sTimeOut As String
    Private sTimeOutWarning As String
    Private iScreenWidth As Integer
    Private iScreenHeight As Integer
    Private sMenu As String
    Private sSubMenu As String
    Private sForm As String
    Private iNoOfUnSucsfAtteptts As Integer
    Private iMinPasswordCharacter As Integer
    Private iMaxPasswordCharacter As Integer
    Private iBCMAssignmentID As Integer
    Private iBCMYearID As Integer
    Private sBCMYearName As String
    Private iLCAssignmentID As Integer
    Private dStartDate As String
    Private dEndDate As String
    Private iDefaultBranch As Integer

    Private iStatusid As String
    Private iPkoid As String

    Public dtDocoImageViewID As DataTable

    Public Property AccessCodeID() As Integer
        Get
            Return (iAccessCodeID)
        End Get
        Set(ByVal Value As Integer)
            iAccessCodeID = Value
        End Set
    End Property
    Public Property AccessCode() As String
        Get
            Return (sAccessCode)
        End Get
        Set(ByVal Value As String)
            sAccessCode = Value
        End Set
    End Property
    Public Property EncryptPassword() As String
        Get
            Return (sEncryptPassword)
        End Get
        Set(ByVal Value As String)
            sEncryptPassword = Value
        End Set
    End Property
    Public Property YearID() As Integer
        Get
            Return (iYearID)
        End Get
        Set(ByVal Value As Integer)
            iYearID = Value
        End Set
    End Property
    Public Property YearName() As String
        Get
            Return (sYearName)
        End Get
        Set(ByVal Value As String)
            sYearName = Value
        End Set
    End Property
    Public Property UserID() As Integer
        Get
            Return (iUserID)
        End Get
        Set(ByVal Value As Integer)
            iUserID = Value
        End Set
    End Property
    Public Property UserFullName() As String
        Get
            Return (sUserFullName)
        End Get
        Set(ByVal Value As String)
            sUserFullName = Value
        End Set
    End Property
    Public Property UserLoginName() As String
        Get
            Return (sUserLoginName)
        End Get
        Set(ByVal Value As String)
            sUserLoginName = Value
        End Set
    End Property
    Public Property UserFullNameCode() As String
        Get
            Return (sUserFullNameCode)
        End Get
        Set(ByVal Value As String)
            sUserFullNameCode = Value
        End Set
    End Property
    Public Property LastLoginDate() As String
        Get
            Return (sLastLoginDate)
        End Get
        Set(ByVal Value As String)
            sLastLoginDate = Value
        End Set
    End Property
    Public Property IPAddress() As String
        Get
            Return (sIPAddress)
        End Get
        Set(ByVal Value As String)
            sIPAddress = Value
        End Set
    End Property
    Public Property FileSize() As Integer
        Get
            Return (iFileSize)
        End Get
        Set(ByVal Value As Integer)
            iFileSize = Value
        End Set
    End Property
    Public Property TimeOut() As String
        Get
            Return (sTimeOut)
        End Get
        Set(ByVal Value As String)
            sTimeOut = Value
        End Set
    End Property
    Public Property TimeOutWarning() As String
        Get
            Return (sTimeOutWarning)
        End Get
        Set(ByVal Value As String)
            sTimeOutWarning = Value
        End Set
    End Property
    Public Property ScreenWidth() As Integer
        Get
            Return (iScreenWidth)
        End Get
        Set(ByVal Value As Integer)
            iScreenWidth = Value
        End Set
    End Property
    Public Property ScreenHeight() As Integer
        Get
            Return (iScreenHeight)
        End Get
        Set(ByVal Value As Integer)
            iScreenHeight = Value
        End Set
    End Property
    Public Property Menu() As String
        Get
            Return (sMenu)
        End Get
        Set(ByVal Value As String)
            sMenu = Value
        End Set
    End Property
    Public Property SubMenu() As String
        Get
            Return (sSubMenu)
        End Get
        Set(ByVal Value As String)
            sSubMenu = Value
        End Set
    End Property
    Public Property Form() As String
        Get
            Return (sForm)
        End Get
        Set(ByVal Value As String)
            sForm = Value
        End Set
    End Property
    Public Property NoOfUnSucsfAtteptts() As Integer
        Get
            Return (iNoOfUnSucsfAtteptts)
        End Get
        Set(ByVal Value As Integer)
            iNoOfUnSucsfAtteptts = Value
        End Set
    End Property
    Public Property MaxPasswordCharacter() As Integer
        Get
            Return (iMaxPasswordCharacter)
        End Get
        Set(ByVal Value As Integer)
            iMaxPasswordCharacter = Value
        End Set
    End Property
    Public Property MinPasswordCharacter() As Integer
        Get
            Return (iMinPasswordCharacter)
        End Get
        Set(ByVal Value As Integer)
            iMinPasswordCharacter = Value
        End Set
    End Property
    Public Property BCMAssignmentID() As Integer
        Get
            Return (iBCMAssignmentID)
        End Get
        Set(ByVal Value As Integer)
            iBCMAssignmentID = Value
        End Set
    End Property
    Public Property BCMYearID() As Integer
        Get
            Return (iBCMYearID)
        End Get
        Set(ByVal Value As Integer)
            iBCMYearID = Value
        End Set
    End Property
    Public Property BCMYearName() As String
        Get
            Return (sBCMYearName)
        End Get
        Set(ByVal Value As String)
            sBCMYearName = Value
        End Set
    End Property
    Public Property LCSchedule() As Integer
        Get
            Return (iLCAssignmentID)
        End Get
        Set(ByVal Value As Integer)
            iLCAssignmentID = Value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return (dStartDate)
        End Get
        Set(ByVal Value As String)
            dStartDate = Value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return (dEndDate)
        End Get
        Set(ByVal Value As String)
            dEndDate = Value
        End Set
    End Property
    Public Property DefaultBranch() As Integer
        Get
            Return (iDefaultBranch)
        End Get
        Set(ByVal Value As Integer)
            iDefaultBranch = Value
        End Set
    End Property
    Public Property Statusid() As String
        Get
            Return (iStatusid)
        End Get
        Set(ByVal Value As String)
            iStatusid = Value
        End Set
    End Property
    Public Property pkoid() As String
        Get
            Return (iPkoid)
        End Get
        Set(ByVal Value As String)
            iPkoid = Value
        End Set
    End Property
End Structure

