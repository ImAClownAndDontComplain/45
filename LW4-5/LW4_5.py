import keras
from keras.datasets import mnist

#Importing the data
(X_train, y_train),(X_test, y_test) = mnist.load_data() 

#Normalizing the data
X_train = X_train.astype('float32') / 255.0
X_test = X_test.astype('float32') / 255.0

#Initializing model
model = keras.models.Sequential()

#Adding the model layers
model.add(keras.layers.LSTM(128, input_shape=(X_train.shape[1:]), return_sequences=True))
model.add(keras.layers.Dropout(0.2))
model.add(keras.layers.LSTM(128))
model.add(keras.layers.Dense(64, activation='relu'))
model.add(keras.layers.Dropout(0.2))
model.add(keras.layers.Dense(10, activation='softmax'))
model.summary()

#Compiling the model
model.compile( loss='sparse_categorical_crossentropy', optimizer = keras.optimizers.Adam(lr=0.001, decay=1e-6), metrics=['accuracy'] )

#Fitting data to the model
history = model.fit(X_train, y_train, epochs=3, validation_data=(X_test, y_test))

#save_model(model)

model.save('lstm_model')
model_loaded = keras.models.load_model('lstm_model')


