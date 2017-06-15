// dllmain.h : Declaration of module class.

class CMSGBUSModule : public ATL::CAtlDllModuleT< CMSGBUSModule >
{
public :
	DECLARE_LIBID(LIBID_MSGBUSLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MSGBUS, "{55373E0C-FF93-4669-93B9-BB67DF603CA4}")
};

extern class CMSGBUSModule _AtlModule;
