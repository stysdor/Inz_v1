import time
from pathlib import Path
from typing import Optional, List

import numpy as np
from fastapi import FastAPI, Query
from pydantic import BaseModel

from api_utility import Predictor

##################
# Define input data model
##################
class Sample(BaseModel):
    area: float 
    n_latitude: float 
    e_longitude: float 
    roomNumber: int
    floor: int 
    floorInBuilding: int
    isBalcony: bool 
    isGarden: bool
    isTarrace: bool
    isLoggia: bool
    constructionYear: int
    isCellar: bool
    isGarage: bool
    isParkingSpace: bool
    market: str
    isLift: bool

class Flat(BaseModel):
    area: float 
    n_latitude: float 
    e_longitude: float 
    roomNumber: int
    floor: int 
    floorInBuilding: int
    isBalcony: bool 
    isGarden: bool
    isTarrace: bool
    isLoggia: bool
    constructionYear: int
    isCellar: bool
    isGarage: bool
    isParkingSpace: bool
    market: str
    isLift: bool
    price: float

class Flats(BaseModel):
    samples: List[Flat]

##################
# Import trained model
##################
predictor = Predictor()

##################
# Prediction API
##################
app = FastAPI()

@app.get("/")
def read_root():
    """
    root, welcome message
    """

    return {
        "message": "this is root, prediction endpoint should be `predict`!, update endpoint should be `update_model`"
    }


@app.post("/predict/")
def predict_item(sample: Sample):
    return {
        "prediction": float(predictor.predict(sample))
    }


@app.post("/update_model/")
def update_model(flats: Flats):
    res = predictor.update(flats.samples)
    return {
        "mse_training": res["mse_training"],
        "mse_test": res["mse_test"],
        "rmse_training": res["mse_training"],
        "rmse_test": res["mse_test"],
        "mae_training": res["mse_training"],
        "mae_test": res["mse_test"],
        "utc_ts": int(time.time()),
    }
