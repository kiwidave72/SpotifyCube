#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>
#include <avr/power.h>
 
/* This driver reads raw data from the BNO055

   Connections
   ===========
   Connect SCL to analog 5
   Connect SDA to analog 4
   Connect VDD to 3.3V DC
   Connect GROUND to common ground
 
*/

int RXLED = 17;  // The RX LED has a defined Arduino pin


 
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
 
bool No_Gyro=false;
bool Write_All_Fields=true;

String lastMessage="";

String last_VECTOR_LINEARACCEL="";
double last_VECTOR_LINEARACCEL_x=0;
double last_VECTOR_LINEARACCEL_y=0;
double last_VECTOR_LINEARACCEL_z=0;



String last_VECTOR_EULER="";
double last_VECTOR_EULER_x=0;


int counter=0;
String outputbuffer="";
bool WriteBlankLine = false;

 
/**************************************************************************/
/*
    Arduino setup function (automatically called at startup)
*/
/**************************************************************************/
void setup(void) 
{
      Serial.begin(9600);
    
    inputString.reserve(200);
     
    String outputMessage="";
    outputMessage.concat("By David Norden aka kiwidave - Spotify Cube - v1.2");
    outputMessage.concat("\r\n");
 
    Serial.println(outputMessage);
    pinMode(RXLED, OUTPUT);  // Set RX LED as an output
 }

void sendOutFeatureList()
{
  Serial.println("Cube Features are :");
  
  Serial.println("BNO055");
   
}

/**************************************************************************/
/*
    Arduino loop function, called once 'setup' is complete (your own code
    should go here)
*/
/**************************************************************************/
void loop(void) 
{
    Serial.println("hello");
   digitalWrite(RXLED, LOW);   // set the LED on
   TXLED0; //TX LED is not tied to a normally controlled pin
   delay(1000);              // wait for a second
   digitalWrite(RXLED, HIGH);    // set the LED off
   TXLED1;
   delay(1000);              // wait for a second
    
 }

void write_accel(String field, double data)
{
  outputbuffer.concat(field);
  outputbuffer.concat(data);
}

void write_euler(String field, double data)
{
  outputbuffer.concat(field);
  outputbuffer.concat(data);
  
}  
void write_euler(String data)
{
    outputbuffer.concat(data);
    last_VECTOR_EULER=data;
    WriteBlankLine=true;
}
void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}

