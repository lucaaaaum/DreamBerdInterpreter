# DreamBerdInterpreter
## Introduction
### What the hell is a DreamBerd and why should I interpret one?
DreamBerd is, put it simple, the *perfect* language. It was developed by [TodePond](https://github.com/TodePond) and is currently maintained by them. If you want to check out the original DreamBerd language definition, be sure to go to [the project's repo](https://github.com/TodePond/DreamBerd).

The language is meant to be a joke, but I wnated to test my knowledeges with .NET and see if I could create an interpreter to it. I also needed to really, *really* get into Regex because it is quite the language to interpret.
### CLI
DreamBerdInterpreter is a CLI tool that can interpret DreamBerd, both as some expressions in a shell and as a full on script.
#### Shell
The shell tool is exactly what it sounds like. Think of a python shell or the JavaScript built-in console of your favorite browser. It lets you run any command the language offers, and even has some custom commands and a help menu.
#### Script Interpreter
The script interpreter, on the other hand, is meant to be 

### Building the Project
- todo (it's getting late where I leave man, I need to rest)

### Examples
You can run some fun stuff, currently:

#### Variables and Constants
This will create a variable variable (reassignable and editable) and will print it's value on the console:
```dreamberd
var var myVariableVariable = anyValueYouWish!
print(myVariableVariable)!
```

#### Printing
DreamBerd allows you to print *anything* you want. I mean, almost anything.
```dreamberd
print("this is a string with quotation marks!")!   // output: this is a string without quotation marks!
print(this is a string without quotation marks!)!  // output: this is a string without quotation marks!
var var myVariable = "this is a string inside a variable with quotation marks!"!
print(myVariable)!                                 // output: this is a string with quotation marks!
var var myOtherVariable = this is a string inside a variable without quotation marks!!
print(myOtherVariable)!                            // output: this is a string without quotation marks
```

#### Comments
You can comment lines out and you can also comment things beside the actual code (I don't know how to express myself, sry!)
```dreamberd
print("antedêguemon")! // this part is not evaluated
// this part is also not evaluated
```
