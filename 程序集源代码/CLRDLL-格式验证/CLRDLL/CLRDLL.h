// CLRDLL.h

#pragma once
#include<string>
#include<iostream>
#include<regex>
using namespace std;
using namespace System;

namespace CLRDLL {

	public ref class Verify
	{
		// TODO: Add your methods for this class here.
	public:
		bool IsEmail(char* email);
		bool IsHandset(char* phone);
		void test(){
			cout << "Hello CLRDLL" << endl;
		}
	private:
		bool IsChar(char ch);
	};
}
