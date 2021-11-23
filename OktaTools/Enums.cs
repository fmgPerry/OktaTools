namespace OktaTools
{
    public enum Envs
    {
        DevInt,
        TestA,
        TestB,

    }
    public enum AuthLevels
    {
        None = 20,
        View = 40,
        Act = 60,
        Full = 100
    }
    public enum OktaGroup
    {
        test,
        prod
    }
    public enum Secretkey
    {
        test,
        pp,
        p
    }
    public enum OktaUsersList
    {
        None,
        FormImportOktaUsers,
        FormDeleteOktaUserByID,
        FormDeleteOktaUsers,
        FormDeleteOktaUsersAllUnlinked,
        FormUpdateOktaUserLogin,
        FormResyncOktaUsers
    }
    public enum CsvFilename
    {
        AllOktaProfiles,
        CanBeDeletedOktaProfiles,
        DeletedOktaProfiles,
        ImportedOktaProfiles,
        ResyncedOktaProfiles,
        NotProcessedOktaProfiles
    }

    public enum ImportMethod
    {
        Code,
        Boomi        
    }
    public enum GetUserBy
    {
        contactId,
        publicId,
        login,
        oktaId
    }
}
