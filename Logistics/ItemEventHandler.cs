using Mafi;
using Mafi.Collections;
using Mafi.Collections.ImmutableCollections;
using Mafi.Collections.ReadonlyCollections;
using Mafi.Core;
using Mafi.Core.Economy;
using Mafi.Core.Products;
using System.Collections.Generic;
using UnityEngine;


namespace COIExtended.ItemEventHandler
{
 
    public class COIE_ProductsManager : IProductsManager
    {
        public IAssetTransactionManager AssetManager { get; private set; }

        public IProductsManager m_ProductsManager;

        public ImmutableArray<ProductStats> ProductStats { get; private set; }

        public ProductsSlimIdManager SlimIdManager { get; private set; }

        public COIE_ProductsManager(IAssetTransactionManager assetManager, IProductsManager productsManager, ImmutableArray<ProductStats> productStats, ProductsSlimIdManager slimIdManager)
        {
            AssetManager = assetManager;
            m_ProductsManager = productsManager;
            ProductStats = productStats;
            SlimIdManager = slimIdManager;

            Debug.Log("Created Cargo ship progress");

            Debug.Log("Created Cargo ship progress");

            Debug.Log("Created Cargo ship progress");
            Debug.Log("Created Cargo ship progress");
            Debug.Log("Created Cargo ship progress");
            Debug.Log("Created Cargo ship progress");
            Debug.Log("Created Cargo ship progress");
        }

        public void ProductCreated(ProductProto product, Quantity quantity, CreateReason reason)
        {
            if (product.Id == NewIDs.Virtual.CargoShipProgress)
            {
                Debug.Log("Created Cargo ship progress");
            }
        }
        
        public void ReportProductsTransformation(IIndexable<ProductQuantity> inputs, IIndexable<ProductQuantity> outputs, DestroyReason destroyReason, CreateReason createReason, bool disableSourceProductsConversionLoss = false)
        {
            // Implement the logic here.
        }

        public void DestroyProductReturnRemovedSourceProducts(ProductProto product, Quantity quantity, DestroyReason reason, Lyst<KeyValuePair<ProductProto, PartialQuantityLarge>> result)
        {
            // Implement the logic here.
        }

        public void ProductCreated(ProductProto product, Quantity quantity, IIndexable<ProductQuantity> sources, CreateReason reason)
        {
            // Implement the logic here.
        }

        public void ProductDestroyed(ProductProto product, Quantity quantity, DestroyReason reason)
        {
            // Implement the logic here.
        }

        public void ProductDestroyed(ProductSlimId slimId, Quantity quantity, DestroyReason reason)
        {
            // Implement the logic here.
        }

        public bool CanBeCleared(ProductProto product)
        {
            // Implement the logic here.
            return true; // or false based on your logic.
        }

        public void ClearProduct(ProductProto product, Quantity quantity)
        {
            // Implement the logic here.
        }

        public void ClearProductNoChecks(ProductProto product, Quantity quantity)
        {

        }

        public void ReportStorageCapacityChange(ProductProto product, Quantity quantity)
        {
            // Implement the logic here.
        }

        public ProductStats GetStatsFor(ProductProto product)
        {
            // Implement the logic here.
            return default(ProductStats); // Return the appropriate value.
        }

        public void IncreaseRecyclingRatio(Percent percent)
        {
            // Implement the logic here.
        }
    }

   
}
