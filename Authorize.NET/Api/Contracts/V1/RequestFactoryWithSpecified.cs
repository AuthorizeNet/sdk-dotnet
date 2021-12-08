using System;
namespace AuthorizeNet.Api.Contracts.V1
{
#pragma warning disable 169

	public static class RequestFactoryWithSpecified
	{
		public static void DecryptPaymentDataRequest(decryptPaymentDataRequest argument)
		{
			if (null != argument)
			{
				OpaqueDataType(argument.opaqueData);
			}
		}
		public static void OpaqueDataType(opaqueDataType argument)
		{
			if (null != argument)
			{
				if (argument.expirationTimeStamp != null) { argument.expirationTimeStampSpecified = true; }
			}
		}

		public static void ProcessorType(processorType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void CustomerPaymentProfileListItemType(customerPaymentProfileListItemType argument)
		{
			if (null != argument)
			{
				if (argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified = true; }
				CustomerAddressType(argument.billTo);
				PaymentMaskedType(argument.payment);
			}
		}

		public static void CustomerPaymentProfileSorting(CustomerPaymentProfileSorting argument)
		{
			if (null != argument)
			{
				//(argument.orderDescending);
			}
		}

		public static void ARBSubscriptionMaskedType(ARBSubscriptionMaskedType argument)
		{
			if (null != argument)
			{
				PaymentScheduleType(argument.paymentSchedule);
				if (0 <= argument.amount) { argument.amountSpecified = true; }
				if (0 <= argument.trialAmount) { argument.trialAmountSpecified = true; }
				if (0 <= argument.status) { argument.statusSpecified = true; }
				SubscriptionCustomerProfileType(argument.profile);
				OrderType(argument.order);
			}
		}

		public static void SubscriptionCustomerProfileType(subscriptionCustomerProfileType argument)
		{
			if (null != argument)
			{
				CustomerProfileExType(argument);
				CustomerPaymentProfileMaskedType(argument.paymentProfile);
				CustomerAddressExType(argument.shippingProfile);
			}
		}

		public static void PaymentSimpleType(paymentSimpleType argument)
		{
			if (null != argument)
			{
				if (argument.Item is bankAccountType) { BankAccountType(argument.Item as bankAccountType); }
				if (argument.Item is creditCardSimpleType) { CreditCardSimpleType(argument.Item as creditCardSimpleType); }
			}
		}
		public static void BankAccountType(bankAccountType argument)
		{
			if (null != argument)
			{
				if (0 <= (int)argument.accountType) { argument.accountTypeSpecified = true; }
				if (0 <= (int)argument.echeckType) { argument.echeckTypeSpecified = true; }
			}
		}
		public static void CreditCardSimpleType(creditCardSimpleType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreditCardType(creditCardType argument)
		{
			if (null != argument)
			{
				CreditCardSimpleType(argument);
				if (argument.isPaymentToken) { argument.isPaymentTokenSpecified = true; }
			}
		}
		public static void CustomerProfileSummaryType(customerProfileSummaryType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void SubscriptionDetail(SubscriptionDetail argument)
		{
			if (null != argument)
			{
			}
		}
		public static void Paging(Paging argument)
		{
			if (null != argument)
			{
			}
		}

		public static void TransactionListSorting(TransactionListSorting argument)
		{
			if (null != argument)
			{
				//(argument.orderDescending);
			}
		}
		public static void HeldTransactionRequestType(heldTransactionRequestType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void ARBGetSubscriptionListSorting(ARBGetSubscriptionListSorting argument)
		{
			if (null != argument)
			{

			}
		}
		public static void PermissionType(permissionType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void MerchantContactType(merchantContactType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void MobileDeviceType(mobileDeviceType argument)
		{
			if (null != argument)
			{
				if (0 <= (int)argument.deviceActivation) { argument.deviceActivationSpecified = true; }
			}
		}
		public static void TransactionSummaryType(transactionSummaryType argument)
		{
			if (null != argument)
			{
				SubscriptionPaymentType(argument.subscription);
				if (argument.hasReturnedItems) { argument.hasReturnedItemsSpecified = true; }
				FraudInformationType(argument.fraudInformation);
				CustomerProfileIdType(argument.profile);
			}
		}
		public static void SubscriptionPaymentType(subscriptionPaymentType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreateProfileResponse(createProfileResponse argument)
		{
			if (null != argument)
			{
				MessagesType(argument.messages);
			}
		}
		public static void MessagesType(messagesType argument)
		{
			if (null != argument)
			{
				if (null != argument.message) { foreach (var value in argument.message) { MessagesTypeMessage(value); } }
			}
		}
		public static void MessagesTypeMessage(messagesTypeMessage argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ArrayOfSetting(ArrayOfSetting argument)
		{
			if (null != argument)
			{
				if (null != argument.setting) { foreach (var value in argument.setting) { SettingType(value); } }
			}
		}
		public static void SettingType(settingType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void SubMerchantType(subMerchantType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void EmailSettingsType(emailSettingsType argument)
		{
			if (null != argument)
			{
				ArrayOfSetting(argument);
			}
		}

		public static void FraudInformationType(fraudInformationType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void TransRetailInfoType(transRetailInfoType argument)
		{
			if (null != argument)
			{
				//marketType should not be assigned here
				//argument.marketType = "2";
			}
		}
		public static void CcAuthenticationType(ccAuthenticationType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void PaymentProfile(paymentProfile argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerProfilePaymentType(customerProfilePaymentType argument)
		{
			if (null != argument)
			{
				if (argument.createProfile) { argument.createProfileSpecified = true; }
				PaymentProfile(argument.paymentProfile);
			}
		}
		public static void TransactionRequestType(transactionRequestType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.amount) { argument.amountSpecified = true; }
				PaymentType(argument.payment);
				CustomerProfilePaymentType(argument.profile);
				SolutionType(argument.solution);
				OrderType(argument.order);
				if (null != argument.lineItems) { foreach (var value in argument.lineItems) { LineItemType(value); } }
				ExtendedAmountType(argument.tax);
				ExtendedAmountType(argument.duty);
				ExtendedAmountType(argument.shipping);
				if (argument.taxExempt) { argument.taxExemptSpecified = true; }
				CustomerDataType(argument.customer);
				CustomerAddressType(argument.billTo);
				NameAndAddressType(argument.shipTo);
				CcAuthenticationType(argument.cardholderAuthentication);
				TransRetailInfoType(argument.retail);
				if (null != argument.transactionSettings) { foreach (var value in argument.transactionSettings) { SettingType(value); } }
				if (null != argument.userFields) { foreach (var value in argument.userFields) { UserField(value); } }
				ExtendedAmountType(argument.surcharge);
				SubMerchantType(argument.subMerchant);
				ExtendedAmountType(argument.tip);
				ProcessingOptions(argument.processingOptions);
				SubsequentAuthInformation(argument.subsequentAuthInformation);
				OtherTaxType(argument.otherTax);
				NameAndAddressType(argument.shipFrom);
				AuthorizationIndicatorType(argument.authorizationIndicatorType);
			}
		}
		public static void PaymentType(paymentType argument)
		{
			if (null != argument)
			{
				if (argument.Item is bankAccountType) { BankAccountType(argument.Item as bankAccountType); }
				if (argument.Item is creditCardType) { CreditCardType(argument.Item as creditCardType); }
				if (argument.Item is encryptedTrackDataType)
				{
					EncryptedTrackDataType(argument.Item as encryptedTrackDataType);
				}

				if (argument.Item is paymentEmvType) { PaymentEmvType(argument.Item as paymentEmvType); }
				if (argument.Item is opaqueDataType) { OpaqueDataType(argument.Item as opaqueDataType); }
				if (argument.Item is payPalType) { PayPalType(argument.Item as payPalType); }
				if (argument.Item is creditCardTrackType) { CreditCardTrackType(argument.Item as creditCardTrackType); }
			}
		}
		public static void EncryptedTrackDataType(encryptedTrackDataType argument)
		{
			if (null != argument)
			{
				KeyBlock(argument.FormOfPayment);
			}
		}

		public static void PaymentEmvType(paymentEmvType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void KeyBlock(KeyBlock argument)
		{
			if (null != argument)
			{
				KeyValue(argument.Value);
			}
		}
		public static void KeyValue(KeyValue argument)
		{
			if (null != argument)
			{
				KeyManagementScheme(argument.Scheme);
			}
		}
		public static void KeyManagementScheme(KeyManagementScheme argument)
		{
			if (null != argument)
			{
				KeyManagementSchemeDUKPT(argument.DUKPT);
			}
		}
		public static void KeyManagementSchemeDUKPT(KeyManagementSchemeDUKPT argument)
		{
			if (null != argument)
			{
				KeyManagementSchemeDUKPTMode(argument.Mode);
				KeyManagementSchemeDUKPTDeviceInfo(argument.DeviceInfo);
				KeyManagementSchemeDUKPTEncryptedData(argument.EncryptedData);
			}
		}
		public static void KeyManagementSchemeDUKPTMode(KeyManagementSchemeDUKPTMode argument)
		{
			if (null != argument)
			{
			}
		}
		public static void KeyManagementSchemeDUKPTDeviceInfo(KeyManagementSchemeDUKPTDeviceInfo argument)
		{
			if (null != argument)
			{
			}
		}
		public static void KeyManagementSchemeDUKPTEncryptedData(KeyManagementSchemeDUKPTEncryptedData argument)
		{
			if (null != argument)
			{
			}
		}
		public static void PayPalType(payPalType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreditCardTrackType(creditCardTrackType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void SolutionType(solutionType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void OrderType(orderType argument)
		{
			if (null != argument)
			{
				if (argument.discountAmount >= 0) { argument.discountAmountSpecified = true; }
				if (argument.taxIsAfterDiscount) { argument.taxIsAfterDiscountSpecified = true; }
				if (null != argument.purchaseOrderDateUTC) { argument.purchaseOrderDateUTCSpecified = true; }
			}
		}
		public static void OrderExType(orderExType argument)
		{
			if (null != argument)
			{
				OrderType(argument);
			}
		}
		public static void LineItemType(lineItemType argument)
		{
			if (null != argument)
			{
				if (argument.taxable)
				{
					argument.taxableSpecified = true;
					if (argument.taxRate >= 0) { argument.taxRateSpecified = true; }
					if (argument.taxAmount >= 0) { argument.taxAmountSpecified = true; }
					if (argument.nationalTax >= 0) { argument.nationalTaxSpecified = true; }
					if (argument.localTax >= 0) { argument.localTaxSpecified = true; }
					if (argument.vatRate >= 0) { argument.vatRateSpecified = true; }
					if (argument.alternateTaxRate >= 0) { argument.alternateTaxRateSpecified = true; }
					if (argument.alternateTaxAmount >= 0) { argument.alternateTaxAmountSpecified = true; }
					if (argument.totalAmount >= 0) { argument.totalAmountSpecified = true; }
					if (argument.discountRate >= 0) { argument.discountRateSpecified = true; }
					if (argument.discountAmount >= 0) { argument.discountAmountSpecified = true; }
					if (argument.taxIncludedInTotal) { argument.taxIncludedInTotalSpecified = true; }
					if (argument.taxIsAfterDiscount) { argument.taxIsAfterDiscountSpecified = true; }
				}
			}
		}
		public static void ExtendedAmountType(extendedAmountType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerDataType(customerDataType argument)
		{
			if (null != argument)
			{
				if (0 <= (int)argument.type) { argument.typeSpecified = true; }
				DriversLicenseType(argument.driversLicense);
			}
		}
		public static void DriversLicenseType(driversLicenseType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerAddressType(customerAddressType argument)
		{
			if (null != argument)
			{
				NameAndAddressType(argument);
			}
		}
		public static void NameAndAddressType(nameAndAddressType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerAddressExType(customerAddressExType argument)
		{
			if (null != argument)
			{
				CustomerAddressType(argument);
			}
		}
		public static void UserField(userField argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ReturnedItemType(returnedItemType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void BatchStatisticType(batchStatisticType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.returnedItemAmount) { argument.returnedItemAmountSpecified = true; }
				if (0 <= argument.returnedItemCount) { argument.returnedItemCountSpecified = true; }
				if (0 <= argument.chargebackAmount) { argument.chargebackAmountSpecified = true; }
				if (0 <= argument.chargebackCount) { argument.chargebackCountSpecified = true; }
				if (0 <= argument.correctionNoticeCount) { argument.correctionNoticeCountSpecified = true; }
				if (0 <= argument.chargeChargeBackAmount) { argument.chargeChargeBackAmountSpecified = true; }
				if (0 <= argument.chargeChargeBackCount) { argument.chargeChargeBackCountSpecified = true; }
				if (0 <= argument.refundChargeBackAmount) { argument.refundChargeBackAmountSpecified = true; }
				if (0 <= argument.refundChargeBackCount) { argument.refundChargeBackCountSpecified = true; }
				if (0 <= argument.chargeReturnedItemsAmount) { argument.chargeReturnedItemsAmountSpecified = true; }
				if (0 <= argument.chargeReturnedItemsCount) { argument.chargeReturnedItemsCountSpecified = true; }
				if (0 <= argument.refundReturnedItemsAmount) { argument.refundReturnedItemsAmountSpecified = true; }
				if (0 <= argument.refundReturnedItemsCount) { argument.refundReturnedItemsCountSpecified = true; }
			}
		}
		public static void BatchDetailsType(batchDetailsType argument)
		{
			if (null != argument)
			{
				if (DateTime.MinValue != argument.settlementTimeUTC) { argument.settlementTimeUTCSpecified = true; }
				if (DateTime.MinValue != argument.settlementTimeLocal) { argument.settlementTimeLocalSpecified = true; }
				if (null != argument.statistics) { foreach (var value in argument.statistics) { BatchStatisticType(value); } }
			}
		}
		public static void FDSFilterType(FDSFilterType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void TransactionDetailsTypeTag(transactionDetailsTypeTag argument)
		{
			if (null != argument)
			{
			}
		}

		public static void OtherTaxType(otherTaxType argument)
		{
			if (null != argument)
			{
				if (argument.nationalTaxAmount >= 0) { argument.nationalTaxAmountSpecified = true; }
				if (argument.localTaxAmount >= 0) { argument.localTaxAmountSpecified = true; }
				if (argument.alternateTaxAmount >= 0) { argument.alternateTaxAmountSpecified = true; }
				if (argument.vatTaxRate >= 0) { argument.vatTaxRateSpecified = true; }
				if (argument.vatTaxAmount >= 0) { argument.vatTaxAmountSpecified = true; }
			}
		}
		public static void TransactionDetailsType(transactionDetailsType argument)
		{
			if (null != argument)
			{
				SubscriptionPaymentType(argument.subscription);
				if (null != argument.FDSFilters) { foreach (var value in argument.FDSFilters) { FDSFilterType(value); } }
				BatchDetailsType(argument.batch);
				OrderExType(argument.order);
				if (0 <= argument.requestedAmount) { argument.requestedAmountSpecified = true; }
				ExtendedAmountType(argument.tax);
				ExtendedAmountType(argument.shipping);
				ExtendedAmountType(argument.duty);
				if (null != argument.lineItems) { foreach (var value in argument.lineItems) { LineItemType(value); } }
				if (0 <= argument.prepaidBalanceRemaining) { argument.prepaidBalanceRemainingSpecified = true; }
				if (argument.taxExempt) { argument.taxExemptSpecified = true; }
				PaymentMaskedType(argument.payment);
				CustomerDataType(argument.customer);
				CustomerAddressType(argument.billTo);
				NameAndAddressType(argument.shipTo);
				if (argument.recurringBilling) { argument.recurringBillingSpecified = true; }
				if (null != argument.returnedItems) { foreach (var value in argument.returnedItems) { ReturnedItemType(value); } }
				SolutionType(argument.solution);

				if (null != argument.emvDetails) { foreach (var value in argument.emvDetails) { TransactionDetailsTypeTag(value); } }

				CustomerProfileIdType(argument.profile);
				ExtendedAmountType(argument.surcharge);
				ExtendedAmountType(argument.tip);
				OtherTaxType(argument.otherTax);
				NameAndAddressType(argument.shipFrom);
			}
		}
		public static void PaymentMaskedType(paymentMaskedType argument)
		{
			if (null != argument)
			{
				if (argument.Item is bankAccountMaskedType) { BankAccountMaskedType(argument.Item as bankAccountMaskedType); }
				if (argument.Item is creditCardMaskedType) { CreditCardMaskedType(argument.Item as creditCardMaskedType); }
				if (argument.Item is tokenMaskedType)
				{
					TokenMaskedType(argument.Item as tokenMaskedType);
				}

			}


		}
		public static void BankAccountMaskedType(bankAccountMaskedType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.accountType) { argument.accountTypeSpecified = true; }
				if (0 <= argument.echeckType) { argument.echeckTypeSpecified = true; }
			}
		}
		public static void CreditCardMaskedType(creditCardMaskedType argument)
		{
			if (null != argument)
			{
				CardArt(argument.cardArt);
				if (argument.isPaymentToken) { argument.isPaymentTokenSpecified = true; }
			}
		}
		public static void CardArt(cardArt argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TokenMaskedType(tokenMaskedType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TransactionResponse(transactionResponse argument)
		{
			if (null != argument)
			{
				TransactionResponsePrePaidCard(argument.prePaidCard);
				if (null != argument.messages) { foreach (var value in argument.messages) { TransactionResponseMessage(value); } }
				if (null != argument.errors) { foreach (var value in argument.errors) { TransactionResponseError(value); } }
				if (null != argument.splitTenderPayments) { foreach (var value in argument.splitTenderPayments) { TransactionResponseSplitTenderPayment(value); } }
				if (null != argument.userFields) { foreach (var value in argument.userFields) { UserField(value); } }
				NameAndAddressType(argument.shipTo);
				TransactionResponseSecureAcceptance(argument.secureAcceptance);
				TransactionResponseEmvResponse(argument.emvResponse);
				CustomerProfileIdType(argument.profile);
			}
		}

		public static void TransactionResponseEmvResponse(transactionResponseEmvResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.tags) { foreach (var value in argument.tags) { EmvTag(value); } }
			}
		}

		public static void TransactionResponsePrePaidCard(transactionResponsePrePaidCard argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TransactionResponseMessage(transactionResponseMessage argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TransactionResponseError(transactionResponseError argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TransactionResponseSplitTenderPayment(transactionResponseSplitTenderPayment argument)
		{
			if (null != argument)
			{
			}
		}
		public static void TransactionResponseSecureAcceptance(transactionResponseSecureAcceptance argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ProfileTransVoidType(profileTransVoidType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ProfileTransAmountType(profileTransAmountType argument)
		{
			if (null != argument)
			{
				ExtendedAmountType(argument.tax);
				ExtendedAmountType(argument.shipping);
				ExtendedAmountType(argument.duty);
				if (null != argument.lineItems) { foreach (var value in argument.lineItems) { LineItemType(value); } }
			}
		}
		public static void ProfileTransRefundType(profileTransRefundType argument)
		{
			if (null != argument)
			{
				ProfileTransAmountType(argument);
				OrderExType(argument.order);
			}
		}
		public static void ProfileTransPriorAuthCaptureType(profileTransPriorAuthCaptureType argument)
		{
			if (null != argument)
			{
				ProfileTransAmountType(argument);
			}
		}
		public static void ProfileTransOrderType(profileTransOrderType argument)
		{
			if (null != argument)
			{
				ProfileTransAmountType(argument);
				OrderExType(argument.order);
				if (argument.taxExempt) { argument.taxExemptSpecified = true; }
				if (argument.recurringBilling)
				{
					argument.recurringBillingSpecified = true;
					ProcessingOptions(argument.processingOptions);
					SubsequentAuthInformation(argument.subsequentAuthInformation);
					AuthorizationIndicatorType(argument.authorizationIndicatorType);
				}
			}
		}
		public static void ProcessingOptions(processingOptions argument)
		{
			if (null != argument)
			{
				if (argument.isFirstRecurringPayment) { argument.isFirstRecurringPaymentSpecified = true; }
				if (argument.isFirstSubsequentAuth) { argument.isFirstSubsequentAuthSpecified = true; }
				if (argument.isSubsequentAuth) { argument.isSubsequentAuthSpecified = true; }
				if (argument.isStoredCredentials) { argument.isStoredCredentialsSpecified = true; }
			}
		}
		public static void SubsequentAuthInformation(subsequentAuthInformation argument)
		{
			if (null != argument)
			{
				if (0 <= argument.reason) { argument.reasonSpecified = true; }
			}
		}
		public static void AuthorizationIndicatorType(authorizationIndicatorType argument)
		{
			if (null != argument)
			{

			}
		}
		public static void ProfileTransCaptureOnlyType(profileTransCaptureOnlyType argument)
		{
			if (null != argument)
			{
				ProfileTransOrderType(argument);
			}
		}
		public static void ProfileTransAuthOnlyType(profileTransAuthOnlyType argument)
		{
			if (null != argument)
			{
				ProfileTransOrderType(argument);
			}
		}
		public static void ProfileTransAuthCaptureType(profileTransAuthCaptureType argument)
		{
			if (null != argument)
			{
				ProfileTransOrderType(argument);
			}
		}
		public static void ProfileTransactionType(profileTransactionType argument)
		{
			if (null != argument)
			{
				if (argument.Item is profileTransAuthCaptureType) { ProfileTransAuthCaptureType(argument.Item as profileTransAuthCaptureType); }
				if (argument.Item is profileTransAuthOnlyType) { ProfileTransAuthOnlyType(argument.Item as profileTransAuthOnlyType); }
				if (argument.Item is profileTransCaptureOnlyType) { ProfileTransCaptureOnlyType(argument.Item as profileTransCaptureOnlyType); }
				if (argument.Item is profileTransPriorAuthCaptureType) { ProfileTransPriorAuthCaptureType(argument.Item as profileTransPriorAuthCaptureType); }
				if (argument.Item is profileTransRefundType) { ProfileTransRefundType(argument.Item as profileTransRefundType); }
				if (argument.Item is profileTransVoidType) { ProfileTransVoidType(argument.Item as profileTransVoidType); }
			}
		}
		public static void DriversLicenseMaskedType(driversLicenseMaskedType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerPaymentProfileBaseType(customerPaymentProfileBaseType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.customerType) { argument.customerTypeSpecified = true; }
				CustomerAddressType(argument.billTo);
			}
		}
		public static void CustomerPaymentProfileMaskedType(customerPaymentProfileMaskedType argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileBaseType(argument);
				if (argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified = true; }
				PaymentMaskedType(argument.payment);
				DriversLicenseMaskedType(argument.driversLicense);
			}
		}
		public static void CustomerPaymentProfileType(customerPaymentProfileType argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileBaseType(argument);
				PaymentType(argument.payment);
				DriversLicenseType(argument.driversLicense);
				if (argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified = true; }
				SubsequentAuthInformation(argument.subsequentAuthInformation);
			}
		}
		public static void CustomerPaymentProfileExType(customerPaymentProfileExType argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileType(argument);
			}
		}
		public static void CustomerProfileBaseType(customerProfileBaseType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerProfileExType(customerProfileExType argument)
		{
			if (null != argument)
			{
				CustomerProfileBaseType(argument);
			}
		}
		public static void CustomerProfileMaskedType(customerProfileMaskedType argument)
		{
			if (null != argument)
			{
				CustomerProfileExType(argument);
				if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { CustomerPaymentProfileMaskedType(value); } }
				if (null != argument.shipToList) { foreach (var value in argument.shipToList) { CustomerAddressExType(value); } }
				if (0 <= argument.profileType) { argument.profileTypeSpecified = true; }
			}
		}
		public static void CustomerProfileInfoExType(customerProfileInfoExType argument)
		{
			if (null != argument)
			{
				CustomerProfileExType(argument);
				if (0 <= argument.profileType) { argument.profileTypeSpecified = true; }
			}
		}
		public static void CustomerProfileType(customerProfileType argument)
		{
			if (null != argument)
			{
				CustomerProfileBaseType(argument);
				if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { CustomerPaymentProfileType(value); } }
				if (null != argument.shipToList) { foreach (var value in argument.shipToList) { CustomerAddressType(value); } }
				if (0 <= argument.profileType) { argument.profileTypeSpecified = true; }
			}
		}
		public static void ContactDetailType(ContactDetailType argument)
		{

		}
		public static void SecurePaymentContainerErrorType(securePaymentContainerErrorType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CustomerType(customerType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.type) { argument.typeSpecified = true; }
				DriversLicenseType(argument.driversLicense);
			}
		}
		public static void PaymentScheduleType(paymentScheduleType argument)
		{
			if (null != argument)
			{
				PaymentScheduleTypeInterval(argument.interval);
				if (DateTime.MinValue != argument.startDate) { argument.startDateSpecified = true; }
				if (0 <= argument.totalOccurrences) { argument.totalOccurrencesSpecified = true; }
				if (0 <= argument.trialOccurrences) { argument.trialOccurrencesSpecified = true; }
			}
		}

		public static void PaymentScheduleTypeInterval(paymentScheduleTypeInterval argument)
		{

		}

		public static void CustomerProfileIdType(customerProfileIdType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void ARBSubscriptionType(ARBSubscriptionType argument)
		{
			if (null != argument)
			{
				PaymentScheduleType(argument.paymentSchedule);
				if (0 < argument.amount) { argument.amountSpecified = true; }

				PaymentType(argument.payment);
				OrderType(argument.order);
				CustomerType(argument.customer);
				NameAndAddressType(argument.billTo);
				NameAndAddressType(argument.shipTo);
				CustomerProfileIdType(argument.profile);
			}
		}

		public static void ARBSubscriptionTypeSetTrialAmountSpecified(ARBSubscriptionType argument)
		{
			if (null != argument)
			{
				if (0 <= argument.trialAmount) { argument.trialAmountSpecified = true; }
			}
		}

		public static void PaymentDetails(paymentDetails argument)
		{
			if (null != argument)
			{
			}
		}
		public static void FingerPrintType(fingerPrintType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ImpersonationAuthenticationType(impersonationAuthenticationType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void MerchantAuthenticationType(merchantAuthenticationType argument)
		{
			if (null != argument)
			{
				if (argument.Item is fingerPrintType)
				{
					FingerPrintType(argument.Item as fingerPrintType);
				}
				if (argument.Item is impersonationAuthenticationType) { ImpersonationAuthenticationType(argument.Item as impersonationAuthenticationType); }
			}
		}
		public static void ANetApiRequest(ANetApiRequest argument)
		{
			if (null != argument)
			{
				MerchantAuthenticationType(argument.merchantAuthentication);
			}
		}
		public static void DecryptPaymentDataResponse(decryptPaymentDataResponse argument)
		{
			if (null != argument)
			{
				CustomerAddressType(argument.shippingInfo);
				CustomerAddressType(argument.billingInfo);
				CreditCardMaskedType(argument.cardInfo);
				PaymentDetails(argument.paymentDetails);
			}
		}
		public static void ANetApiResponse(ANetApiResponse argument)
		{
			if (null != argument)
			{
				MessagesType(argument.messages);
			}
		}
		public static void SecurePaymentContainerRequest(securePaymentContainerRequest argument)
		{
			if (null != argument)
			{
				WebCheckOutDataType(argument.data);
			}
		}
		public static void SecurePaymentContainerResponse(securePaymentContainerResponse argument)
		{
			if (null != argument)
			{
				OpaqueDataType(argument.opaqueData);
			}
		}
		public static void WebCheckOutDataType(webCheckOutDataType argument)
		{
			if (null != argument)
			{
				WebCheckOutDataTypeToken(argument.token);
			}
		}
		public static void WebCheckOutDataTypeToken(webCheckOutDataTypeToken argument)
		{
			if (null != argument)
			{
			}
		}
		public static void IsAliveRequest(isAliveRequest argument)
		{
			if (null != argument)
			{
			}
		}


		// Added this method because isAliveRequest take AnetApiRequest as argument.
		// AnetApiSchema.generated.cs - isAliveRequest is not the child class of AnetApiRequest class.
		public static void IsAliveRequest(ANetApiRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void IsAliveResponse(isAliveResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void AuthenticateTestRequest(authenticateTestRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void AuthenticateTestResponse(authenticateTestResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBCreateSubscriptionRequest(ARBCreateSubscriptionRequest argument)
		{
			if (null != argument)
			{
				ARBSubscriptionType(argument.subscription);
				ARBSubscriptionTypeSetTrialAmountSpecified(argument.subscription);
			}
		}
		public static void ARBCreateSubscriptionResponse(ARBCreateSubscriptionResponse argument)
		{
			if (null != argument)
			{
				CustomerProfileIdType(argument.profile);
			}
		}
		public static void ARBUpdateSubscriptionRequest(ARBUpdateSubscriptionRequest argument)
		{
			if (null != argument)
			{
				ARBSubscriptionType(argument.subscription);
			}
		}
		public static void ARBUpdateSubscriptionResponse(ARBUpdateSubscriptionResponse argument)
		{
			if (null != argument)
			{
				CustomerProfileIdType(argument.profile);
			}
		}
		public static void ARBCancelSubscriptionRequest(ARBCancelSubscriptionRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBCancelSubscriptionResponse(ARBCancelSubscriptionResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBGetSubscriptionStatusRequest(ARBGetSubscriptionStatusRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBGetSubscriptionStatusResponse(ARBGetSubscriptionStatusResponse argument)
		{
			if (null != argument)
			{
				if (0 <= argument.status) { argument.statusSpecified = true; }
			}
		}
		public static void CreateCustomerProfileRequest(createCustomerProfileRequest argument)
		{
			if (null != argument)
			{
				CustomerProfileType(argument.profile);
				if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
			}
		}
		public static void CreateCustomerProfileResponse(createCustomerProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreateCustomerPaymentProfileRequest(createCustomerPaymentProfileRequest argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileType(argument.paymentProfile);
				if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
			}
		}
		public static void CreateCustomerPaymentProfileResponse(createCustomerPaymentProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreateCustomerShippingAddressRequest(createCustomerShippingAddressRequest argument)
		{
			if (null != argument)
			{
				CustomerAddressType(argument.address);
			}
		}
		public static void CreateCustomerShippingAddressResponse(createCustomerShippingAddressResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreateCustomerProfileFromTransactionRequest(createCustomerProfileFromTransactionRequest argument)
		{
			if (null != argument)
			{
				CustomerProfileBaseType(argument.customer);
				if (argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified = true; }
				if (argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified = true; }
				if (0 <= argument.profileType) { argument.profileTypeSpecified = true; }
			}
		}
		public static void GetCustomerProfileRequest(getCustomerProfileRequest argument)
		{
			if (null != argument)
			{
				if (argument.unmaskExpirationDate) { argument.unmaskExpirationDateSpecified = true; }
				if (argument.includeIssuerInfo) { argument.includeIssuerInfoSpecified = true; }

			}
		}
		public static void GetCustomerProfileResponse(getCustomerProfileResponse argument)
		{
			if (null != argument)
			{
				CustomerProfileMaskedType(argument.profile);
			}
		}
		public static void GetCustomerPaymentProfileRequest(getCustomerPaymentProfileRequest argument)
		{
			if (null != argument)
			{
				if (argument.includeIssuerInfo) { argument.includeIssuerInfoSpecified = true; }

			}
		}
		public static void GetCustomerPaymentProfileResponse(getCustomerPaymentProfileResponse argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileMaskedType(argument.paymentProfile);
			}
		}
		public static void GetCustomerShippingAddressRequest(getCustomerShippingAddressRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetCustomerShippingAddressResponse(getCustomerShippingAddressResponse argument)
		{
			if (null != argument)
			{
				if (argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified = true; }
				CustomerAddressExType(argument.address);
			}
		}
		public static void UpdateCustomerProfileRequest(updateCustomerProfileRequest argument)
		{
			if (null != argument)
			{
				if (null != argument.profile && argument.profile.GetType() == typeof(customerProfileInfoExType))
				{
					CustomerProfileInfoExType((customerProfileInfoExType)argument.profile);
				}
				else
				{
					CustomerProfileExType(argument.profile);
				}
			}
		}
		public static void UpdateCustomerProfileResponse(updateCustomerProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void UpdateCustomerPaymentProfileRequest(UpdateCustomerPaymentProfileRequest argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileExType(argument.paymentProfile);
				if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
			}
		}
		public static void UpdateCustomerPaymentProfileResponse(UpdateCustomerPaymentProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void UpdateCustomerShippingAddressRequest(updateCustomerShippingAddressRequest argument)
		{
			if (null != argument)
			{
				CustomerAddressExType(argument.address);
				if (argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified = true; }
			}
		}
		public static void UpdateCustomerShippingAddressResponse(updateCustomerShippingAddressResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerProfileRequest(deleteCustomerProfileRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerProfileResponse(deleteCustomerProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerPaymentProfileRequest(deleteCustomerPaymentProfileRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerPaymentProfileResponse(deleteCustomerPaymentProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerShippingAddressRequest(deleteCustomerShippingAddressRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void DeleteCustomerShippingAddressResponse(deleteCustomerShippingAddressResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void CreateCustomerProfileTransactionRequest(createCustomerProfileTransactionRequest argument)
		{
			if (null != argument)
			{
				ProfileTransactionType(argument.transaction);
			}
		}
		public static void CreateCustomerProfileTransactionResponse(createCustomerProfileTransactionResponse argument)
		{
			if (null != argument)
			{
				TransactionResponse(argument.transactionResponse);
			}
		}
		public static void ValidateCustomerPaymentProfileRequest(validateCustomerPaymentProfileRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ValidateCustomerPaymentProfileResponse(validateCustomerPaymentProfileResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetCustomerProfileIdsRequest(getCustomerProfileIdsRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetCustomerProfileIdsResponse(getCustomerProfileIdsResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void UpdateSplitTenderGroupRequest(updateSplitTenderGroupRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void UpdateSplitTenderGroupResponse(updateSplitTenderGroupResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetTransactionDetailsRequest(getTransactionDetailsRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetTransactionDetailsResponse(getTransactionDetailsResponse argument)
		{
			if (null != argument)
			{
				TransactionDetailsType(argument.transaction);
			}
		}
		public static void CreateTransactionRequest(createTransactionRequest argument)
		{
			if (null != argument)
			{
				TransactionRequestType(argument.transactionRequest);
			}
		}
		public static void CreateTransactionResponse(createTransactionResponse argument)
		{
			if (null != argument)
			{
				TransactionResponse(argument.transactionResponse);
				CreateProfileResponse(argument.profileResponse);
			}
		}

		public static void UpdateHeldTransactionRequest(updateHeldTransactionRequest argument)
		{
			if (null != argument)
			{
				HeldTransactionRequestType(argument.heldTransactionRequest);
			}
		}
		public static void UpdateHeldTransactionResponse(updateHeldTransactionResponse argument)
		{
			if (null != argument)
			{
				TransactionResponse(argument.transactionResponse);
			}
		}

		public static void GetBatchStatisticsRequest(getBatchStatisticsRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetBatchStatisticsResponse(getBatchStatisticsResponse argument)
		{
			if (null != argument)
			{
				BatchDetailsType(argument.batch);
			}
		}
		public static void GetSettledBatchListRequest(getSettledBatchListRequest argument)
		{
			if (null != argument)
			{
				if (argument.includeStatistics) { argument.includeStatisticsSpecified = true; }
				if (DateTime.MinValue != argument.firstSettlementDate) { argument.firstSettlementDateSpecified = true; }
				if (DateTime.MinValue != argument.lastSettlementDate) { argument.lastSettlementDateSpecified = true; }
			}
		}
		public static void GetSettledBatchListResponse(getSettledBatchListResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.batchList) { foreach (var value in argument.batchList) { BatchDetailsType(value); } }
			}
		}
		public static void GetTransactionListRequest(getTransactionListRequest argument)
		{
			if (null != argument)
			{
				TransactionListSorting(argument.sorting);
				Paging(argument.paging);
			}
		}
		public static void GetTransactionListResponse(getTransactionListResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.transactions) { foreach (var value in argument.transactions) { TransactionSummaryType(value); } }
				if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
			}
		}
		public static void GetHostedProfilePageRequest(getHostedProfilePageRequest argument)
		{
			if (null != argument)
			{
				if (null != argument.hostedProfileSettings) { foreach (var value in argument.hostedProfileSettings) { SettingType(value); } }
			}
		}
		public static void GetHostedProfilePageResponse(getHostedProfilePageResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetUnsettledTransactionListRequest(getUnsettledTransactionListRequest argument)
		{
			if (null != argument)
			{
				if (0 <= argument.status) { argument.statusSpecified = true; }
				TransactionListSorting(argument.sorting);
				Paging(argument.paging);
			}
		}

		public static void GetHostedPaymentPageRequest(getHostedPaymentPageRequest argument)
		{
			if (null != argument)
			{
				TransactionRequestType(argument.transactionRequest);
				if (null != argument.hostedPaymentSettings) { foreach (var value in argument.hostedPaymentSettings) { SettingType(value); } }
			}
		}
		public static void GetHostedPaymentPageResponse(getHostedPaymentPageResponse argument)
		{
			if (null != argument)
			{
			}
		}

		public static void GetUnsettledTransactionListResponse(getUnsettledTransactionListResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.transactions) { foreach (var value in argument.transactions) { TransactionSummaryType(value); } }
				if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
			}
		}
		public static void MobileDeviceRegistrationRequest(mobileDeviceRegistrationRequest argument)
		{
			if (null != argument)
			{
				MobileDeviceType(argument.mobileDevice);
			}
		}
		public static void MobileDeviceRegistrationResponse(mobileDeviceRegistrationResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void MobileDeviceLoginRequest(mobileDeviceLoginRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void MobileDeviceLoginResponse(mobileDeviceLoginResponse argument)
		{
			if (null != argument)
			{
				MerchantContactType(argument.merchantContact);
				if (null != argument.userPermissions) { foreach (var value in argument.userPermissions) { PermissionType(value); } }
				TransRetailInfoType(argument.merchantAccount);
			}
		}
		public static void LogoutRequest(logoutRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void LogoutResponse(logoutResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void SendCustomerTransactionReceiptRequest(sendCustomerTransactionReceiptRequest argument)
		{
			if (null != argument)
			{
				EmailSettingsType(argument.emailSettings);
			}
		}
		public static void SendCustomerTransactionReceiptResponse(sendCustomerTransactionReceiptResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBGetSubscriptionListRequest(ARBGetSubscriptionListRequest argument)
		{
			if (null != argument)
			{
				ARBGetSubscriptionListSorting(argument.sorting);
				Paging(argument.paging);
			}
		}
		public static void ARBGetSubscriptionListResponse(ARBGetSubscriptionListResponse argument)
		{
			if (null != argument)
			{
				if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
				if (null != argument.subscriptionDetails) { foreach (var value in argument.subscriptionDetails) { SubscriptionDetail(value); } }
			}
		}

		public static void GetCustomerPaymentProfileListRequest(getCustomerPaymentProfileListRequest argument)
		{
			if (null != argument)
			{
				CustomerPaymentProfileSorting(argument.sorting);
				Paging(argument.paging);
			}
		}
		public static void GetCustomerPaymentProfileListResponse(getCustomerPaymentProfileListResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { CustomerPaymentProfileListItemType(value); } }
			}
		}
		public static void ARBGetSubscriptionRequest(ARBGetSubscriptionRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void ARBGetSubscriptionResponse(ARBGetSubscriptionResponse argument)
		{
			if (null != argument)
			{
				ARBSubscriptionMaskedType(argument.subscription);
			}
		}

		public static void EnumCollection(EnumCollection argument)
		{
			if (null != argument)
			{
				CustomerProfileSummaryType(argument.customerProfileSummaryType);
				PaymentSimpleType(argument.paymentSimpleType);
			}
		}

		public static void AuDetailsType(auDetailsType argument)
		{
			if (null != argument)
			{
			}
		}
		public static void AuDeleteType(auDeleteType argument)
		{
			if (null != argument)
			{
				AuDetailsType(argument);
				CreditCardMaskedType(argument.creditCard);
			}
		}

		public static void AuUpdateType(auUpdateType argument)
		{
			if (null != argument)
			{
				AuDetailsType(argument);
				CreditCardMaskedType(argument.newCreditCard);
				CreditCardMaskedType(argument.oldCreditCard);
			}
		}
		public static void AuResponseType(auResponseType argument)
		{
			if (null != argument)
			{
			}
		}

		public static void EmvTag(emvTag argument)
		{
			if (null != argument)
			{
			}
		}


		public static void GetAUJobSummaryRequest(getAUJobSummaryRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetAUJobSummaryResponse(getAUJobSummaryResponse argument)
		{
			if (null != argument)
			{
				if (null != argument.auSummary) { foreach (var value in argument.auSummary) { AuResponseType(value); } }
			}
		}
		public static void GetAUJobDetailsRequest(getAUJobDetailsRequest argument)
		{
			if (null != argument)
			{
				if (0 <= argument.modifiedTypeFilter) { argument.modifiedTypeFilterSpecified = true; }
				Paging(argument.paging);
			}
		}
		public static void GetAUJobDetailsResponse(getAUJobDetailsResponse argument)
		{
			if (null != argument)
			{
				if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
				if (null != argument.auDetails) { foreach (var value in argument.auDetails) { AuDetailsType(value); } }
			}
		}

		public static void GetMerchantDetailsRequest(getMerchantDetailsRequest argument)
		{

		}

		public static void GetMerchantDetailsResponse(getMerchantDetailsResponse argument)
		{
			if (null != argument)
			{
				if (argument.isTestMode) { argument.isTestModeSpecified = true; }
				if (null != argument.processors) { foreach (var value in argument.processors) { ProcessorType(value); } }
				CustomerAddressType(argument.businessInformation);
				if (null != argument.contactDetails) { foreach (var value in argument.contactDetails) { ContactDetailType(value); } }
			}
		}

		public static void GetTransactionListForCustomerRequest(getTransactionListForCustomerRequest argument)
		{
			if (null != argument)
			{
				TransactionListSorting(argument.sorting);
				Paging(argument.paging);
			}
		}
		public static void UpdateMerchantDetailsRequest(updateMerchantDetailsRequest argument)
		{
			if (null != argument)
			{
			}
		}
		public static void UpdateMerchantDetailsResponse(updateMerchantDetailsResponse argument)
		{
			if (null != argument)
			{
			}
		}
		public static void GetCustomerPaymentProfileNonceRequest(getCustomerPaymentProfileNonceRequest argument)
		{

		}
		public static void GetCustomerPaymentProfileNonceResponse(getCustomerPaymentProfileNonceResponse argument)
		{
			if (null != argument)
			{
				OpaqueDataType(argument.opaqueData);
			}
		}
	}


	// ReSharper restore InconsistentNaming
#pragma warning restore 169
}
/*
Requests
ARBCreateSubscriptionRequest
ARBUpdateSubscriptionRequest
createCustomerPaymentProfileRequest
createCustomerProfileRequest
createCustomerProfileTransactionRequest
createTransactionRequest
getSettledBatchListRequest
mobileDeviceRegistrationRequest
UpdateCustomerPaymentProfileRequest
XXDoNotUseDummyRequest
ECHO is off.
 */
/*
Objects
ECHO is off.
ARBSubscriptionType
bankAccountMaskedType
bankAccountType
batchDetailsType
batchStatisticType
customerDataType
customerPaymentProfileBaseType
customerPaymentProfileExType
customerPaymentProfileMaskedType
customerPaymentProfileType
customerProfileMaskedType
customerProfileType
customerType
lineItemType
mobileDeviceType
paymentMaskedType
paymentScheduleType
paymentSimpleType
paymentType
profileTransactionType
profileTransAmountType
profileTransAuthCaptureType
profileTransAuthOnlyType
profileTransCaptureOnlyType
profileTransOrderType
profileTransPriorAuthCaptureType
profileTransRefundType
transactionDetailsType
transactionRequestType
transactionSummaryType
ECHO is off.
 */
