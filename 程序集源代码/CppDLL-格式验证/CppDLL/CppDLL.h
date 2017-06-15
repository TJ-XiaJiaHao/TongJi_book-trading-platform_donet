#include<iostream>
#include<string>
#include<regex>
using namespace std;
#ifdef CPPDLL_EXPORTS
#define CPPDLL_API __declspec(dllexport)
#else
#define CPPDLL_API __declspec(dllimport)
#endif


CPPDLL_API class CVerify
{
public:
	CVerify(){}
	~CVerify(){}
public:
	bool IsEmail(char* emailCharArry){
		cout << "IsEmail " << emailCharArry << endl;
		return true;
	}
	bool IsHandSet(char* phone){
		cout << "IsHandSet " << phone << endl;
		return true;

	}
	bool IsChar(char ch);

};
extern "C" CPPDLL_API bool IsEmail(char* emailCharArry);
extern "C" CPPDLL_API bool IsHandSet(char* phone);
CPPDLL_API bool IsChar(char ch);
