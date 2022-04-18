import time
from pathlib import Path
from typing import Optional, List

import numpy as np
from fastapi import FastAPI, Query
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel

from api_utility import Predictor

##################
# Define input data model
##################
class Flat(BaseModel):
    area: float 
    price: Optional[float] = 0
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

origins = [
    "http://localhost",
    "http://localhost:4200",
    "http://localhost:5000",
    "https://localhost:5000",
    "http://localhost:5001",
    "https://localhost:5001"  
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/")
def read_root():
    """
    root, welcome message
    """

    return {
        "message": "this is root, prediction endpoint should be `predict`!, update endpoint should be `update_model`"
    }


@app.post("/predict/")
def predict_item(sample: Flat):
    return {
        "prediction": float(predictor.predict(sample))
    }


@app.post("/update_model/")
def update_model(flats: Flats):
    res = predictor.update(flats.samples)
    return {
        "MseTrain": res["mse_train"],
        "MseTest": res["mse_test"],
        "RmseTrain": res["rmse_train"],
        "RmseTest": res["rmse_test"],
        "MaeTrain": res["mae_train"],
        "MaeTest": res["mae_test"],
    }
