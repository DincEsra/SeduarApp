from sqlalchemy import create_engine, Column, Integer, String, Float, Boolean
from sqlalchemy.orm import declarative_base, sessionmaker

# SQLite veritabanı dosyasının adı ve konumu
SQLALCHEMY_DATABASE_URL = "sqlite:///./seduar.db"

engine = create_engine(SQLALCHEMY_DATABASE_URL, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base = declarative_base()

# Ürünler Tablomuz (MAUI'deki Product.cs modeliyle aynı yapıda)
class Product(Base):
    __tablename__ = "products"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, index=True)
    description = Column(String)
    price = Column(Float)
    image_url = Column(String)
    category = Column(String)
    is_new_arrival = Column(Boolean, default=False)