// MSGBUS.cpp : Implementation of DLL Exports.

//
// Note: COM+ 1.0 Information:
//      Please remember to run Microsoft Transaction Explorer to install the component(s).
//      Registration is not done by default. 

#include "stdafx.h"
#include "resource.h"
#include "MSGBUS_i.h"
#include "dllmain.h"
#include "compreg.h"
#include "xdlldata.h"


using namespace ATL;

// Used to determine whether the DLL can be unloaded by OLE.
STDAPI DllCanUnloadNow(void)
{
	#ifdef _MERGE_PROXYSTUB
	HRESULT hr = PrxDllCanUnloadNow();
	if (hr != S_OK)
		return hr;
#endif
			return _AtlModule.DllCanUnloadNow();
	}

// Returns a class factory to create an object of the requested type.
STDAPI DllGetClassObject(_In_ REFCLSID rclsid, _In_ REFIID riid, _Outptr_ LPVOID* ppv)
{
	#ifdef _MERGE_PROXYSTUB
	HRESULT hr = PrxDllGetClassObject(rclsid, riid, ppv);
	if (hr != CLASS_E_CLASSNOTAVAILABLE)
		return hr;
#endif
		return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}

// DllRegisterServer - Adds entries to the system registry.
STDAPI DllRegisterServer(void)
{
	// registers object, typelib and all interfaces in typelib
	HRESULT hr = _AtlModule.DllRegisterServer();
	#ifdef _MERGE_PROXYSTUB
	if (FAILED(hr))
		return hr;
	hr = PrxDllRegisterServer();
#endif
		return hr;
}

// DllUnregisterServer - Removes entries from the system registry.
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
	#ifdef _MERGE_PROXYSTUB
	if (FAILED(hr))
		return hr;
	hr = PrxDllRegisterServer();
	if (FAILED(hr))
		return hr;
	hr = PrxDllUnregisterServer();
#endif
		return hr;
}

// DllInstall - Adds/Removes entries to the system registry per user per machine.
STDAPI DllInstall(BOOL bInstall, _In_opt_  LPCWSTR pszCmdLine)
{
	HRESULT hr = E_FAIL;
	static const wchar_t szUserSwitch[] = L"user";

	if (pszCmdLine != NULL)
	{
		if (_wcsnicmp(pszCmdLine, szUserSwitch, _countof(szUserSwitch)) == 0)
		{
			ATL::AtlSetPerUserRegistration(true);
		}
	}

	if (bInstall)
	{	
		hr = DllRegisterServer();
		if (FAILED(hr))
		{
			DllUnregisterServer();
		}
	}
	else
	{
		hr = DllUnregisterServer();
	}

	return hr;
}


// MSGBUS.cpp : Implementation of CMSGBUS

#include "stdafx.h"
#include "MSGBUS.h"


// CMSGBUS



STDMETHODIMP CMSGBUS::getMsg(LONG id, BSTR* msg)
{
	// TODO: Add your implementation code here
	BSTR str;
	if (id > 10000)getError(id, msg);
	else getInfo(id, msg);
	return S_OK;
}


STDMETHODIMP CMSGBUS::getError(LONG id, BSTR* msg)
{
	// TODO: Add your implementation code here
	BSTR str;
	switch (id){
	case 10001:
		str = ::SysAllocString(L"邮箱格式不正确！");
		break;
	case 10002:
		str = ::SysAllocString(L"账号或密码错误！");
		break;
	case 10003:
		str = ::SysAllocString(L"手机号格式不正确！");
		break;
	case 10004:
		str = ::SysAllocString(L"邮箱格式不正确！");
		break;
	case 10005:
		str = ::SysAllocString(L"账号已经存在！");
		break;
	case 10006:
		str = ::SysAllocString(L"输入数据错误！");
		break;
	default:
		str = ::SysAllocString(L"go die");
		break;
	}
	*msg = str;
	return S_OK;
}


STDMETHODIMP CMSGBUS::getInfo(LONG id, BSTR* msg)
{
	// TODO: Add your implementation code here
	// TODO: Add your implementation code here
	BSTR str;
	switch (id){
	case 00001:
		str = ::SysAllocString(L"添加书本成功！");
		break;
	case 00002:
		str = ::SysAllocString(L"添加购物车成功！");
		break;
	case 00003:
		str = ::SysAllocString(L"支付成功！");
		break;
	default:
		str = ::SysAllocString(L"go die");
		break;
	}
	*msg = str;
	return S_OK;
}
