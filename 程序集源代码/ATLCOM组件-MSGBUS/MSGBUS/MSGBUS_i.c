

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Jun 23 07:15:40 2017
 */
/* Compiler settings for MSGBUS.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, IID_IComponentRegistrar,0xa817e7a2,0x43fa,0x11d0,0x9e,0x44,0x00,0xaa,0x00,0xb6,0x77,0x0a);


MIDL_DEFINE_GUID(IID, IID_IMSGBUS,0xA05F6F3A,0xB773,0x4851,0x9A,0xC5,0x24,0x28,0x10,0x03,0xE0,0xCE);


MIDL_DEFINE_GUID(IID, LIBID_MSGBUSLib,0xF348B5B9,0x2C9F,0x49DE,0xAF,0x93,0xBE,0xCE,0x4A,0x6E,0xE4,0x48);


MIDL_DEFINE_GUID(CLSID, CLSID_CompReg,0x77142EDE,0x6E71,0x4193,0xA4,0xEE,0xFF,0x58,0x37,0x62,0xEF,0x8F);


MIDL_DEFINE_GUID(CLSID, CLSID_MSGBUS,0x14195B27,0xE7FE,0x44FB,0x86,0x00,0xAA,0x3B,0x14,0x7F,0xCD,0xFF);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



