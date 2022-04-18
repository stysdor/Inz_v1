# -*- coding: utf-8 -*-

from pathlib import Path
from pickle import load
import tensorflow.keras 
from tensorflow.keras.models import model_from_json
from sklearn.model_selection import train_test_split
from tensorflow.keras import metrics
import pandas as pd
import numpy as np


class Predictor:

    def __init__(self):
        """
        Initialize the model
        """
        self.model = load_model()
        self.scaler = load_scaler()
        self.kmeans = load_kmeans()
        
    def predict(self, sample):
        data = transform_item(self, sample)
        data = np.delete(data, 1, axis = 1)
        prediction_scaled = self.model.predict([data])
        data = np.insert(data, 1, prediction_scaled)
        data_inverse_transformed = self.scaler.inverse_transform([data])

        return data_inverse_transformed[:,1]

    def update(self, samples):
        data = transform_data(self, samples)
        return train_model(self.model, self.scaler, data)

    def save_model(self, model):
        save_model(model)
        
            

def load_model():
 
    # load json and create model
    json_file = open('../model/model.json', 'r')
    loaded_model_json = json_file.read()
    json_file.close()
    loaded_model = model_from_json(loaded_model_json)
    # load weights into new model
    loaded_model.load_weights("../model/model.h5")
    
    return loaded_model

def load_scaler():
     with open("../model/scaler.pkl", "rb") as f:
        scaler = load(f)
     return scaler

def load_kmeans():
    with open("../model/kmeans.pkl", "rb") as f:
        kmeans = load(f)
    return kmeans

def transform_data(predictor: Predictor, samples):
    transformed_data = []
    for item in samples:
        transformed_item = transform_item(predictor, item)
        transformed_data.append(transformed_item[0])
    return transformed_data

def transform_item(predictor: Predictor, sample):
    X = get_list_attribute(sample)
    X.append(predictor.kmeans.predict([X[2:4]])[0])
    X.pop(3)
    X.pop(2)
    transformed_item = []
    transformed_item = predictor.scaler.transform([X])

    return transformed_item

def get_list_attribute(sample):
    features = dict(sample)
    attributes_list = []
    for key in features.keys():
        if key == 'market':
            value = 0 if features[key]=='pierwotny' else 1
            attributes_list.append(value)
        else:
            features[key] = attributes_list.append(features[key])
    return attributes_list

def train_model(model, scaler, data):
    data = np.asarray(data)
    X =  np.delete(data, 1, axis=1)
    y = data[:,1]
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size = 0.15)
    model.compile(
        optimizer = 'adam', 
        loss = 'mean_squared_error',
        metrics=[
        metrics.MeanSquaredError(),
        metrics.RootMeanSquaredError(),
        metrics.MeanAbsoluteError()
    ])
    epochs_hist = model.fit(X_train, y_train, epochs = 50, batch_size = 15, verbose = 1, validation_split = 0.2 )

    X_testing = np.array(X_test)
    y_predict = model.predict(X_testing)
    mse_training = epochs_hist.history['val_loss'][49]
    rmse_training = epochs_hist.history['val_root_mean_squared_error'][49]
    mae_training = epochs_hist.history['val_mean_absolute_error'][49]
    evaluation_test = model.evaluate(X_test, y_test)
    save_model(model)
    return {
        "mse_test": evaluation_test[1],
        "rmse_test": evaluation_test[2],
        "mae_test": evaluation_test[3],
        "mse_train": mse_training,
        "rmse_train": rmse_training,
        "mae_train": mae_training
        }

def save_model(model):
        # serialize model to JSON
    model_json = model.to_json()
    with open("../model/model.json", "w") as json_file:
        json_file.write(model_json)
    # serialize weights to HDF5
    model.save_weights("../model/model.h5")