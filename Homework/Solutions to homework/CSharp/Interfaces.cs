using System;


interface Animal{
	void SaySomething();
}

class Dog: Animal
{
   public void SaySomething(){
		Console.Write("Bao...");
	}
}

class Cat: Animal
{
	 public void SaySomething(){
		Console.Write("Miao...");
	}
}

class Cow: Animal
{
	public void SaySomething(){
		Console.Write("Muuu..");
	}
}


