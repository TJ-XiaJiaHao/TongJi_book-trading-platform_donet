// CppDLL.cpp : Defines the exported functions for the DLL application.
//
#include "stdafx.h"
#include "CppDLL.h"
CPPDLL_API bool IsChar(char ch){
	if ((ch >= 97) && (ch <= 122)) //26个小写字母
		return true;
	if ((ch >= 65) && (ch <= 90)) //26个大写字母
		return true;
	if ((ch >= 48) && (ch <= 57)) //0~9
		return true;
	if (ch == 95 || ch == 45 || ch == 46 || ch == 64) //_-.@
		return true;
	return false;
}
CPPDLL_API bool IsEmail(char* emailCharArry){

	string email = emailCharArry;
	//如果长度小于5，显然无法构成邮箱
	if (email.length() < 5){
		return false;
	}

	//首字母要合法
	char ch = email[0]; 
	if (((ch >= 97) && (ch <= 122)) || ((ch >= 65) && (ch <= 90)) || ((ch >= 48) && (ch <= 57)))
	{
		int atCount = 0;
		int atPos = 0;
		int dotCount = 0;
		for (int i = 1; i < email.length(); i++) //0已经判断过了，从1开始
		{
			ch = email[i];
			if (IsChar(ch))
			{
				if (ch == 64) //"@"
				{
					atCount++;
					atPos = i;
				}
				else if ((atCount > 0) && (ch == 46))//@符号后的"."号
					dotCount++;
			}
			else{
				return false;
			}
		}
		//6. 结尾不得是字符“@”或者“.”
		if (ch == 46){
			return false;
		}
		//2. 必须包含一个并且只有一个符号“@”
		//3. @后必须包含至少一个至多三个符号“.”
		if ((atCount != 1) || (dotCount < 1) || (dotCount>3)){
			return false;
		}
		//5. 不允许出现“@.”或者.@
		int x, y;
		x = email.find("@.");
		y = email.find(".@");
		if (x > 0 || y > 0)
		{
			return false;
		}
		return true;
	}
	return false;
}
CPPDLL_API bool IsHandSet(char* phoneArr){
	string phone = phoneArr;
	bool temp = false;
	regex e("^1(3\\d|47|5([0-3]|[5-9])|8(0|2|[5-9]))\\d{8}$");
	if (regex_match(phone, e)) return true;
	else return false;
}