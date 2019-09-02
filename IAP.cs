using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;


	
	public class IAP : MonoBehaviour, IStoreListener
	{
		private static IStoreController m_StoreController;          // The Unity Purchasing system.
		private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.


		public static string Product_2G =    "more_gold";   
		public static string Product_1G = "basic";
		public static string Product_3G =  "more_golds"; 

	int totalMoney;

	public Text totalMoneyT1;
	public Text totalMoneyT2;
	public Text totalMoneyT3;


		void Start()
		{
			// If we haven't set up the Unity Purchasing reference
			if (m_StoreController == null)
			{
				// Begin to configure our connection to Purchasing
				InitializePurchasing();
			}
		}

		public void InitializePurchasing() 
		{
			
			if (IsInitialized())
			{
			
				return;
			}

			
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			
		builder.AddProduct(Product_1G, ProductType.Consumable);
		builder.AddProduct(Product_2G, ProductType.Consumable);
		builder.AddProduct(Product_3G, ProductType.Consumable);

			
			
			UnityPurchasing.Initialize(this, builder);
		}


		private bool IsInitialized()
		{
			// Only say we are initialized if both the Purchasing references are set.
			return m_StoreController != null && m_StoreExtensionProvider != null;
		}


			public void Buy_5000_G()
			{
				BuyProductID(Product_1G);
			}
			public void Buy_12000_G()
			{
				BuyProductID(Product_2G);
			}
			public void Buy_35000_G()
			{
				BuyProductID(Product_3G);
			}


		void BuyProductID(string productId)
		{
			// If Purchasing has been initialized ...
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing 
				// system's products collection.
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
					// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
					// asynchronously.
					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
				// retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}


		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			// Purchasing has succeeded initializing. Collect our Purchasing references.
			Debug.Log("OnInitialized: PASS");

			// Overall Purchasing system, configured with products for this application.
			m_StoreController = controller;
			// Store specific subsystem, for accessing device-specific store features.
			m_StoreExtensionProvider = extensions;
		}


		public void OnInitializeFailed(InitializationFailureReason error)
		{
			// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
			Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}


		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
		{
			// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, Product_1G, StringComparison.Ordinal))
			{
			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 5000;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);
				Debug.Log("You Bought 5000G");
			//	ScoreManager.score += 100;
		}else if (String.Equals(args.purchasedProduct.definition.id, Product_2G, StringComparison.Ordinal))
		{
			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 12000;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);
			Debug.Log("You Bought 12000");
			//	ScoreManager.score += 100;
		}else if (String.Equals(args.purchasedProduct.definition.id, Product_3G, StringComparison.Ordinal))
		{
			totalMoney = PlayerPrefs.GetInt ("Money", 500);
			totalMoney = totalMoney + 35000;
			totalMoneyT1.text = totalMoneyT2.text = totalMoneyT3.text = totalMoney.ToString();

			PlayerPrefs.SetInt ("Money", totalMoney);
			Debug.Log("You Bought 35000G");
			//	ScoreManager.score += 100;
		}


			
			// Or ... an unknown product has been purchased by this user. Fill in additional products here....
			else 
			{
				Debug.Log("Unkown buyed : got unlimited C");
			}

			// Return a flag indicating whether this product has completely been received, or if the application needs 
			// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
			// saving purchased products to the cloud, and when that save is delayed. 
			return PurchaseProcessingResult.Complete;
		}


		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
			// this reason with the user to guide their troubleshooting actions.
			Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
		}
	}
