#"""
#This script is used to run convert the raw data to train and test data
#It is designed to be idempotent [stateless transformation]
#Usage:
#    python3 ./scripts/etl.py
#"""
#from pathlib import Path

#import click
#import pandas as pd
#from sklearn.model_selection import train_test_split
#from sklearn.preprocessing import MinMaxScaler

#from utility import parse_config


#@click.command()
#@click.argument("config_file", type=str, default="scripts/config.yml")
#def etl(config_file):
#    """
#    ETL function that load raw data and convert to train and test set
#    Args:
#        config_file [str]: path to config file
#    Returns:
#        None
#    """

#    ##################
#    # Load config from config file
#    ##################
#    config = parse_config(config_file)

#    raw_data_file = config["etl"]["raw_data_file"]
#    processed_path = Path(config["etl"]["processed_path"])
#    test_size = config["etl"]["test_size"]

#    ##################
#    # Data transformation
#    ##################)
#    flat = pd.read_csv(raw_data_file,sep=';', decimal=',',header=None)
#    flat = flat.dropna()
#    flat = flat.replace('pierwotny', 0)
#    flat = flat.replace('wtórny', 1)
#    X = flat.drop([2, 0, 3,4,16,17], axis = 1)
#    Y = flat[2]

#    scaler = MinMaxScaler()
#    X_scaled = scaler.fit_transform(X)
#    Y = Y.values.reshape(-1,1)
#    Y_scaled = scaler.fit_transform(Y)

#    scaler_path.parent.mkdir(parents=True, exist_ok=True)
#    with open(scaler_path, "wb") as f:
#        dump(scaler, f)
#    logger.info(f"Persisted scaler to {scaler_path}")

#    ##################
#    # train test split & Export
#    ##################
#    # train test split

#    X_train, X_test, y_train, y_test = train_test_split(X_scaled, y_scaled, test_size = test_size)

#    train = X_train.merge(y_train)
#    test = X_test.merge(y_test)

#    # export data
#    train.to_csv(processed_path / "train.csv", index=False)
#    test.to_csv(processed_path / "test.csv", index=False)


#if __name__ == "__main__":
#    etl()
