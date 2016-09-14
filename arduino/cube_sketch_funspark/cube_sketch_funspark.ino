#include <Wire.h>
#include <Adafruit_NeoPixel.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>
#include <avr/power.h>

#define BNO055_NEOPIXEL_PIN          6

 
/* This driver reads raw data from the BNO055

   Connections
   ===========
   Connect SCL to analog 5
   Connect SDA to analog 4
   Connect VDD to 3.3V DC
   Connect GROUND to common ground
 
*/

/* Set the delay between fresh samples */
#define BNO055_SAMPLERATE_DELAY_MS (200)
 
Adafruit_BNO055 bno = Adafruit_BNO055();
 
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

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(1, BNO055_NEOPIXEL_PIN, NEO_GRB + NEO_KHZ800);
 
/**************************************************************************/
/*
    Arduino setup function (automatically called at startup)
*/
/**************************************************************************/
void setup(void) 
{
      Serial1.begin(9600);
    
    inputString.reserve(200);
     
    String outputMessage="";
    outputMessage.concat("By David Norden aka kiwidave - Spotify Cube - v1.2");
    outputMessage.concat("\r\n");
 
    Serial1.println(outputMessage);


  pixels.begin();
  pixels.show();
  
    
  /* Initialise the sensor */
  if(!bno.begin())
  {
    /* There was a problem detecting the BNO055 ... check your connections */
    String outputMessage="";
    outputMessage.concat("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    outputMessage.concat("\r\n");
    outputMessage.concat("fall back to dummy mode");
 
    Serial1.println(outputMessage);
    No_Gyro=true;
 
  }
   
   
  if(!No_Gyro)
  {  
    pixels.setPixelColor(0, pixels.Color(255,2550,255)); // Moderately bright red color.
    
    pixels.show();

     /* Display the current temperature */
    int8_t temp = bno.getTemp();

    String outputMessage="";
    outputMessage.concat("Current Temperature: ");
    outputMessage.concat("\r\n");
    outputMessage.concat(temp);
    outputMessage.concat(" C");
    outputMessage.concat("\r\n");
    Serial1.println(outputMessage);
    
    bno.setExtCrystalUse(true);
  }
  
 }

void sendOutFeatureList()
{
  Serial1.println("Cube Features are :");
  
  Serial1.println("BNO055");
   
}

/**************************************************************************/
/*
    Arduino loop function, called once 'setup' is complete (your own code
    should go here)
*/
/**************************************************************************/
void loop(void) 
{
  
  if (stringComplete) {
    //Serial.println(inputString);
    if(inputString=="CO=true\n")
    {
      Write_All_Fields=false;
    }
    else if(inputString=="CO=false\n")
    {
      Write_All_Fields=true;
    }

    // clear the string:
    inputString = "";
    stringComplete = false;
  }
  
  
    WriteBlankLine=false;
    outputbuffer="";
    counter++;

 
  
   // Possible vector values can be:
    // - VECTOR_ACCELEROMETER - m/s^2
    // - VECTOR_MAGNETOMETER  - uT
    // - VECTOR_GYROSCOPE     - rad/s
    // - VECTOR_EULER         - degrees
    // - VECTOR_LINEARACCEL   - m/s^2
    // - VECTOR_GRAVITY       - m/s^2
  
    imu::Vector<3> euler = bno.getVector(Adafruit_BNO055::VECTOR_EULER);
 
    String VECTOR_EULER;
    VECTOR_EULER.concat("|X ");
    VECTOR_EULER.concat(euler.x());
    VECTOR_EULER.concat("|Y ");
    VECTOR_EULER.concat(euler.y());
    VECTOR_EULER.concat("|Z ");
    VECTOR_EULER.concat(euler.z());
    
    
    if(last_VECTOR_EULER_x ==0)
    {
       write_euler("|X ",euler.x());
       last_VECTOR_EULER_x=euler.x();
    }
    else if(Write_All_Fields)
    {
       write_euler("|X ",euler.x());
     }
    else if(euler.x()!=last_VECTOR_EULER_x)
    {
       write_euler("|X ",euler.x());
       last_VECTOR_EULER_x=euler.x();
    }
    
    // euler as string output
    if(last_VECTOR_EULER=="")
    {
       write_euler(VECTOR_EULER);
    }
    else if(Write_All_Fields)
    {
       write_euler(VECTOR_EULER);
    }
    else if(VECTOR_EULER!=last_VECTOR_EULER)
    {
       write_euler(VECTOR_EULER);
    }

  //    delay(BNO055_SAMPLERATE_DELAY_MS);
  

  //
  //  delay(BNO055_SAMPLERATE_DELAY_MS);
    
 
  //
  //  delay(BNO055_SAMPLERATE_DELAY_MS);
    
    imu::Vector<3> accel = bno.getVector(Adafruit_BNO055::VECTOR_LINEARACCEL);
      
    if(last_VECTOR_LINEARACCEL_x ==0)
    {
       write_accel("|accelX ",accel.x());
       last_VECTOR_LINEARACCEL_x=accel.x();
    }
    else if(Write_All_Fields)
    {
       write_accel("|accelX ",accel.x());
     }
    else if(accel.x()!=last_VECTOR_LINEARACCEL_x)
    {
       write_accel("|accelX ",accel.x());
       last_VECTOR_LINEARACCEL_x=accel.x();
    }
    
    if(last_VECTOR_LINEARACCEL_y ==0)
    {
       write_accel("|accelY ",accel.y());
       last_VECTOR_LINEARACCEL_y=accel.y();
    }
    else if(Write_All_Fields)
    {
       write_accel("|accelY ",accel.y());
    }
    else if(accel.y()!=last_VECTOR_LINEARACCEL_y)
    {
       write_accel("|accelY ",accel.y());
       last_VECTOR_LINEARACCEL_y=accel.y();
    }
    
    if(last_VECTOR_LINEARACCEL_z ==0)
    {
       write_accel("|accelZ ",accel.z());
       last_VECTOR_LINEARACCEL_z=accel.z();
    }
    else if(Write_All_Fields)
    {
       write_accel("|accelZ ",accel.z());
       WriteBlankLine=true;
    }
    else if(accel.z()!=last_VECTOR_LINEARACCEL_z)
    {
       write_accel("|accelZ ",accel.z());
       last_VECTOR_LINEARACCEL_z=accel.z();
    }
     
    
   
   if(WriteBlankLine && outputbuffer!="")
   {
  
      Serial1.print("Counter ");
      Serial1.print(counter);

      Serial1.print(outputbuffer);

      outputbuffer="";
 
   } 
    // Quaternion data
    imu::Quaternion quat = bno.getQuat();
    Serial1.print("|qW ");
    Serial1.print(quat.w(), 4);
    Serial1.print("|qX ");
    Serial1.print(quat.y(), 4);
    Serial1.print("|qY ");
    Serial1.print(quat.x(), 4);
    Serial1.print("|qZ ");
    Serial1.print(quat.z(), 4); 
    
    imu::Vector<3> mag = bno.getVector(Adafruit_BNO055::VECTOR_MAGNETOMETER);
    /* Display the floating point data */
    Serial1.print("|magX ");
    Serial1.print(mag.x());
    Serial1.print("|magY ");
    Serial1.print(mag.y());
    Serial1.print("|magZ ");
    Serial1.print(mag.z());
    
    Serial1.println("");
    delay(BNO055_SAMPLERATE_DELAY_MS);
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
  while (Serial1.available()) {
    // get the new byte:
    char inChar = (char)Serial1.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}

