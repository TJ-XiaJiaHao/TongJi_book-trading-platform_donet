# NET
#### **1、课程项目要求**
  1. 系统至少有五个程序集，需要系统进行模块划分
  2. 需要其中一个是共享程序集
  3. 采用c++/CLI实现一个程序集，给c#使用
  4. 使用c++实现一部分功能，输出为一个Win32DLL，尽量采用多种方式调用其中的函数（互操作）
  5. 实现一个简单的COM组件，然后进行使用
  6. 要求使用多线程技术和线程池技术
  7. 利用ASP.NET、ADO.NET完成基本的处理
  8. 最好利用.NET其他技术进行时间
#### **2、项目概述**
  - 二手书交易平台，用户可自由发布商品或购买商品
  - 商品评论只有购买之后才能评论
  - 用户注册采用邮箱验证的形式
  - 由于支付系统无法完成，所以所有的支付都默认为成功
#### **3、课程要求完成说明**
  1. 主体框架：ASP.NET，前端模板为cshtml，除了基本的controllers和models，我们还在两层中间加了一层业务逻辑层  
  2. 数据库访问：EF5.0 + Linq  
  3. 程序集：位于lib文件夹中   
  
|ID|功能|文件名|命名空间|编写语言|程序集类型|备注|
|---|---|---|----|---|---|---|
|1|邮箱验证|EmailService.dll|DLL.EmailService|c#|私有程序集|要在web.config配置|
|2|验证码生成|RandomCode.dll|DLL.RandomCode|c#|共享程序集|-|
|3|加密|EntryptAndDetrypt.dll|DLL.EntryptAndDetrypt|c#|私有程序集|-|
|4|上传图片|UploadFile.dll|DLL.UploadFile|c#|私有程序集|-|
|5|格式验证|CppDLL.dll|-|c++|私有程序集|-|
  
#### **4、共享程序集RandomCode说明**
1. 生成命令    

![Alt text](https://github.com/justPlay197/NET/blob/master/images/%E6%B7%BB%E5%8A%A0%E5%85%B1%E4%BA%AB%E7%A8%8B%E5%BA%8F%E9%9B%86.png?raw=true)  

2. c#中调用情况：可以看出这是一个共享程序集，且拥有公钥
![Alt text](https://github.com/justPlay197/NET/blob/master/images/%E5%85%B1%E4%BA%AB%E7%A8%8B%E5%BA%8F%E9%9B%86RandomCode.png?raw=true)

#### **5、c++动态链接库遇到的问题**
c++动态链接库所放位置：C:\\Program Files (x86)\\IIS Express —— 运行时的位置  

|ID|1|
|---|---|
|错误类型|运行的时候报错，错误为Debug Assertion Fail|
|问题原因|查阅资料后为指针非法使用问题，但我并没有用指针，最后锁定为字符串传入问题|
|解决办法|c++参数类型改为char*，c#传入为byte[]|

|ID|2|
|---|---|
|错误类型|运行崩溃显示堆栈不对称|
|问题原因|堆栈清理者设定出错|
|解决办法|在dllimport的时候指定堆栈清理方为Cdecl|

|ID|3|
|---|---|
|错误类型|运行正常，但返回的结果永远是true，明显应该返回false的情况也返回true|
|问题原因|非托管代码数据传输问题|
|解决办法|在dllimport和函数声明中间加一句[return:MarshalAs(UnmanagedType.I1)]|
