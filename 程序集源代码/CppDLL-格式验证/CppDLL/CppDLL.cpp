// CppDLL.cpp : Defines the exported functions for the DLL application.
//
#include "stdafx.h"
#include "CppDLL.h"
CPPDLL_API bool IsChar(char ch){
	if ((ch >= 97) && (ch <= 122)) //26��Сд��ĸ
		return true;
	if ((ch >= 65) && (ch <= 90)) //26����д��ĸ
		return true;
	if ((ch >= 48) && (ch <= 57)) //0~9
		return true;
	if (ch == 95 || ch == 45 || ch == 46 || ch == 64) //_-.@
		return true;
	return false;
}
CPPDLL_API bool IsEmail(char* emailCharArry){

	string email = emailCharArry;
	//�������С��5����Ȼ�޷���������
	if (email.length() < 5){
		return false;
	}

	//����ĸҪ�Ϸ�
	char ch = email[0]; 
	if (((ch >= 97) && (ch <= 122)) || ((ch >= 65) && (ch <= 90)) || ((ch >= 48) && (ch <= 57)))
	{
		int atCount = 0;
		int atPos = 0;
		int dotCount = 0;
		for (int i = 1; i < email.length(); i++) //0�Ѿ��жϹ��ˣ���1��ʼ
		{
			ch = email[i];
			if (IsChar(ch))
			{
				if (ch == 64) //"@"
				{
					atCount++;
					atPos = i;
				}
				else if ((atCount > 0) && (ch == 46))//@���ź��"."��
					dotCount++;
			}
			else{
				return false;
			}
		}
		//6. ��β�������ַ���@�����ߡ�.��
		if (ch == 46){
			return false;
		}
		//2. �������һ������ֻ��һ�����š�@��
		//3. @������������һ�������������š�.��
		if ((atCount != 1) || (dotCount < 1) || (dotCount>3)){
			return false;
		}
		//5. ��������֡�@.������.@
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