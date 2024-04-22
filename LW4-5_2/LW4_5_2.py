import numpy as np
import os
import pandas as pd
import keras
from PIL import Image
import matplotlib.pyplot as plt

while True:
    model_loaded = keras.models.load_model('lstm_model')
    
    image = Image.open('D:/circus/45/digit28.bmp')  

    pix = image.load() 

    arr_bytes=[]
    for i in range(28):
        arr_bytes.append([0]*28)

    for i in range(28):
        for j in range(28):
            arr_bytes[j][i]=pix[i,j][0]/255
        
    x = np.expand_dims(arr_bytes, axis=0)
    res = model_loaded.predict(x)
    print( res )
    print( np.argmax(res) )
    input()








