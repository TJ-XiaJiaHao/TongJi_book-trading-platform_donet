
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.6.1055.0




// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly RandomCode
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- 下列自定义特性会自动添加，不要取消注释 -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module RandomCode.dll
// MVID: {9CA5E431-3260-49F7-9A69-DB58D4E1BF17}
.imagebase 0x10000000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x01060000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit DLL.RandomCode.RandomCode
       extends [mscorlib]System.Object
{
  .method public hidebysig instance string 
          getNumber(int32 Length) cil managed
  {
    // 代码大小       14 (0xe)
    .maxstack  3
    .locals init (string V_0)
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldarg.1
    IL_0003:  ldc.i4.0
    IL_0004:  call       instance string DLL.RandomCode.RandomCode::Number(int32,
                                                                           bool)
    IL_0009:  stloc.0
    IL_000a:  br.s       IL_000c

    IL_000c:  ldloc.0
    IL_000d:  ret
  } // end of method RandomCode::getNumber

  .method public hidebysig instance string 
          Number(int32 Length,
                 bool Sleep) cil managed
  {
    // 代码大小       77 (0x4d)
    .maxstack  3
    .locals init (string V_0,
             class [mscorlib]System.Random V_1,
             bool V_2,
             int32 V_3,
             int32 V_4,
             bool V_5,
             string V_6)
    IL_0000:  nop
    IL_0001:  ldarg.2
    IL_0002:  stloc.2
    IL_0003:  ldloc.2
    IL_0004:  brfalse.s  IL_000d

    IL_0006:  ldc.i4.3
    IL_0007:  call       void [mscorlib]System.Threading.Thread::Sleep(int32)
    IL_000c:  nop
    IL_000d:  ldstr      ""
    IL_0012:  stloc.0
    IL_0013:  newobj     instance void [mscorlib]System.Random::.ctor()
    IL_0018:  stloc.1
    IL_0019:  ldc.i4.0
    IL_001a:  stloc.3
    IL_001b:  br.s       IL_003b

    IL_001d:  nop
    IL_001e:  ldloc.0
    IL_001f:  ldloc.1
    IL_0020:  ldc.i4.s   10
    IL_0022:  callvirt   instance int32 [mscorlib]System.Random::Next(int32)
    IL_0027:  stloc.s    V_4
    IL_0029:  ldloca.s   V_4
    IL_002b:  call       instance string [mscorlib]System.Int32::ToString()
    IL_0030:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0035:  stloc.0
    IL_0036:  nop
    IL_0037:  ldloc.3
    IL_0038:  ldc.i4.1
    IL_0039:  add
    IL_003a:  stloc.3
    IL_003b:  ldloc.3
    IL_003c:  ldarg.1
    IL_003d:  clt
    IL_003f:  stloc.s    V_5
    IL_0041:  ldloc.s    V_5
    IL_0043:  brtrue.s   IL_001d

    IL_0045:  ldloc.0
    IL_0046:  stloc.s    V_6
    IL_0048:  br.s       IL_004a

    IL_004a:  ldloc.s    V_6
    IL_004c:  ret
  } // end of method RandomCode::Number

  .method public hidebysig instance string 
          getCharNumber(int32 Length) cil managed
  {
    // 代码大小       14 (0xe)
    .maxstack  3
    .locals init (string V_0)
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldarg.1
    IL_0003:  ldc.i4.0
    IL_0004:  call       instance string DLL.RandomCode.RandomCode::CharNumber(int32,
                                                                               bool)
    IL_0009:  stloc.0
    IL_000a:  br.s       IL_000c

    IL_000c:  ldloc.0
    IL_000d:  ret
  } // end of method RandomCode::getCharNumber

  .method public hidebysig instance string 
          CharNumber(int32 Length,
                     bool Sleep) cil managed
  {
    // 代码大小       128 (0x80)
    .maxstack  3
    .locals init (char[] V_0,
             string V_1,
             int32 V_2,
             class [mscorlib]System.Random V_3,
             bool V_4,
             valuetype [mscorlib]System.DateTime V_5,
             int32 V_6,
             int32 V_7,
             bool V_8,
             string V_9)
    IL_0000:  nop
    IL_0001:  ldarg.2
    IL_0002:  stloc.s    V_4
    IL_0004:  ldloc.s    V_4
    IL_0006:  brfalse.s  IL_000f

    IL_0008:  ldc.i4.3
    IL_0009:  call       void [mscorlib]System.Threading.Thread::Sleep(int32)
    IL_000e:  nop
    IL_000f:  ldc.i4.s   36
    IL_0011:  newarr     [mscorlib]System.Char
    IL_0016:  dup
    IL_0017:  ldtoken    field valuetype '<PrivateImplementationDetails>'/'__StaticArrayInitTypeSize=72' '<PrivateImplementationDetails>'::CB700205FF65F52AA739D18D7B8B0C06536C7C99
    IL_001c:  call       void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array,
                                                                                                        valuetype [mscorlib]System.RuntimeFieldHandle)
    IL_0021:  stloc.0
    IL_0022:  ldstr      ""
    IL_0027:  stloc.1
    IL_0028:  ldloc.0
    IL_0029:  ldlen
    IL_002a:  conv.i4
    IL_002b:  stloc.2
    IL_002c:  call       valuetype [mscorlib]System.DateTime [mscorlib]System.DateTime::get_Now()
    IL_0031:  stloc.s    V_5
    IL_0033:  ldloca.s   V_5
    IL_0035:  call       instance int64 [mscorlib]System.DateTime::get_Ticks()
    IL_003a:  conv.i4
    IL_003b:  not
    IL_003c:  newobj     instance void [mscorlib]System.Random::.ctor(int32)
    IL_0041:  stloc.3
    IL_0042:  ldc.i4.0
    IL_0043:  stloc.s    V_6
    IL_0045:  br.s       IL_006d

    IL_0047:  nop
    IL_0048:  ldloc.3
    IL_0049:  ldc.i4.0
    IL_004a:  ldloc.2
    IL_004b:  callvirt   instance int32 [mscorlib]System.Random::Next(int32,
                                                                      int32)
    IL_0050:  stloc.s    V_7
    IL_0052:  ldloc.1
    IL_0053:  ldloc.0
    IL_0054:  ldloc.s    V_7
    IL_0056:  ldelema    [mscorlib]System.Char
    IL_005b:  call       instance string [mscorlib]System.Char::ToString()
    IL_0060:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0065:  stloc.1
    IL_0066:  nop
    IL_0067:  ldloc.s    V_6
    IL_0069:  ldc.i4.1
    IL_006a:  add
    IL_006b:  stloc.s    V_6
    IL_006d:  ldloc.s    V_6
    IL_006f:  ldarg.1
    IL_0070:  clt
    IL_0072:  stloc.s    V_8
    IL_0074:  ldloc.s    V_8
    IL_0076:  brtrue.s   IL_0047

    IL_0078:  ldloc.1
    IL_0079:  stloc.s    V_9
    IL_007b:  br.s       IL_007d

    IL_007d:  ldloc.s    V_9
    IL_007f:  ret
  } // end of method RandomCode::CharNumber

  .method public hidebysig instance string 
          getChar(int32 Length) cil managed
  {
    // 代码大小       14 (0xe)
    .maxstack  3
    .locals init (string V_0)
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldarg.1
    IL_0003:  ldc.i4.0
    IL_0004:  call       instance string DLL.RandomCode.RandomCode::Char(int32,
                                                                         bool)
    IL_0009:  stloc.0
    IL_000a:  br.s       IL_000c

    IL_000c:  ldloc.0
    IL_000d:  ret
  } // end of method RandomCode::getChar

  .method public hidebysig instance string 
          Char(int32 Length,
               bool Sleep) cil managed
  {
    // 代码大小       128 (0x80)
    .maxstack  3
    .locals init (char[] V_0,
             string V_1,
             int32 V_2,
             class [mscorlib]System.Random V_3,
             bool V_4,
             valuetype [mscorlib]System.DateTime V_5,
             int32 V_6,
             int32 V_7,
             bool V_8,
             string V_9)
    IL_0000:  nop
    IL_0001:  ldarg.2
    IL_0002:  stloc.s    V_4
    IL_0004:  ldloc.s    V_4
    IL_0006:  brfalse.s  IL_000f

    IL_0008:  ldc.i4.3
    IL_0009:  call       void [mscorlib]System.Threading.Thread::Sleep(int32)
    IL_000e:  nop
    IL_000f:  ldc.i4.s   26
    IL_0011:  newarr     [mscorlib]System.Char
    IL_0016:  dup
    IL_0017:  ldtoken    field valuetype '<PrivateImplementationDetails>'/'__StaticArrayInitTypeSize=52' '<PrivateImplementationDetails>'::'420C015E6084500174F1912E91954C1A742F270F'
    IL_001c:  call       void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array,
                                                                                                        valuetype [mscorlib]System.RuntimeFieldHandle)
    IL_0021:  stloc.0
    IL_0022:  ldstr      ""
    IL_0027:  stloc.1
    IL_0028:  ldloc.0
    IL_0029:  ldlen
    IL_002a:  conv.i4
    IL_002b:  stloc.2
    IL_002c:  call       valuetype [mscorlib]System.DateTime [mscorlib]System.DateTime::get_Now()
    IL_0031:  stloc.s    V_5
    IL_0033:  ldloca.s   V_5
    IL_0035:  call       instance int64 [mscorlib]System.DateTime::get_Ticks()
    IL_003a:  conv.i4
    IL_003b:  not
    IL_003c:  newobj     instance void [mscorlib]System.Random::.ctor(int32)
    IL_0041:  stloc.3
    IL_0042:  ldc.i4.0
    IL_0043:  stloc.s    V_6
    IL_0045:  br.s       IL_006d

    IL_0047:  nop
    IL_0048:  ldloc.3
    IL_0049:  ldc.i4.0
    IL_004a:  ldloc.2
    IL_004b:  callvirt   instance int32 [mscorlib]System.Random::Next(int32,
                                                                      int32)
    IL_0050:  stloc.s    V_7
    IL_0052:  ldloc.1
    IL_0053:  ldloc.0
    IL_0054:  ldloc.s    V_7
    IL_0056:  ldelema    [mscorlib]System.Char
    IL_005b:  call       instance string [mscorlib]System.Char::ToString()
    IL_0060:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0065:  stloc.1
    IL_0066:  nop
    IL_0067:  ldloc.s    V_6
    IL_0069:  ldc.i4.1
    IL_006a:  add
    IL_006b:  stloc.s    V_6
    IL_006d:  ldloc.s    V_6
    IL_006f:  ldarg.1
    IL_0070:  clt
    IL_0072:  stloc.s    V_8
    IL_0074:  ldloc.s    V_8
    IL_0076:  brtrue.s   IL_0047

    IL_0078:  ldloc.1
    IL_0079:  stloc.s    V_9
    IL_007b:  br.s       IL_007d

    IL_007d:  ldloc.s    V_9
    IL_007f:  ret
  } // end of method RandomCode::Char

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // 代码大小       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method RandomCode::.ctor

} // end of class DLL.RandomCode.RandomCode

.class private auto ansi sealed '<PrivateImplementationDetails>'
       extends [mscorlib]System.Object
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .class explicit ansi sealed nested private '__StaticArrayInitTypeSize=52'
         extends [mscorlib]System.ValueType
  {
    .pack 1
    .size 52
  } // end of class '__StaticArrayInitTypeSize=52'

  .class explicit ansi sealed nested private '__StaticArrayInitTypeSize=72'
         extends [mscorlib]System.ValueType
  {
    .pack 1
    .size 72
  } // end of class '__StaticArrayInitTypeSize=72'

  .field static assembly initonly valuetype '<PrivateImplementationDetails>'/'__StaticArrayInitTypeSize=52' '420C015E6084500174F1912E91954C1A742F270F' at I_0000287C
  .field static assembly initonly valuetype '<PrivateImplementationDetails>'/'__StaticArrayInitTypeSize=72' CB700205FF65F52AA739D18D7B8B0C06536C7C99 at I_000028B4
} // end of class '<PrivateImplementationDetails>'


// =============================================================

.data cil I_0000287C = bytearray (
                 41 00 42 00 43 00 44 00 45 00 46 00 47 00 48 00  // A.B.C.D.E.F.G.H.
                 49 00 4A 00 4B 00 4C 00 4D 00 4E 00 4F 00 50 00  // I.J.K.L.M.N.O.P.
                 51 00 52 00 53 00 54 00 55 00 56 00 57 00 58 00  // Q.R.S.T.U.V.W.X.
                 59 00 5A 00)                                     // Y.Z.
.data cil I_000028B4 = bytearray (
                 30 00 31 00 32 00 33 00 34 00 35 00 36 00 37 00  // 0.1.2.3.4.5.6.7.
                 38 00 39 00 41 00 42 00 43 00 44 00 45 00 46 00  // 8.9.A.B.C.D.E.F.
                 47 00 48 00 49 00 4A 00 4B 00 4C 00 4D 00 4E 00  // G.H.I.J.K.L.M.N.
                 4F 00 50 00 51 00 52 00 53 00 54 00 55 00 56 00  // O.P.Q.R.S.T.U.V.
                 57 00 58 00 59 00 5A 00)                         // W.X.Y.Z.
// *********** 反汇编完成 ***********************
// 警告: 创建了 Win32 资源文件 RandomCode.res
