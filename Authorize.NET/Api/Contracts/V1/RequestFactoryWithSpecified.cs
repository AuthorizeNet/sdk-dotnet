namespace AuthorizeNet.Api.Contracts.V1
{
    using System;
#pragma warning disable 169
#pragma warning disable 1591
    // ReSharper disable InconsistentNaming 
    /// <summary> 
    /// Special case handlers 
    /// 
    /// validated on ????/??/?? for objects listed at the end 
    /// should be validated after each update of AnetApiSchema.cs 
    /// for fields/properties that are minOccurs="0" since xsd.exe 
    /// generates "specified" property for such fields and requires 
    /// special handling to set them seamlessly 
    /// Make sure to update the respective controllers to call the respective request hand 
    ///  
    /// </summary> 
    public static class RequestFactoryWithSpecified
    {
        public static void decryptPaymentDataRequest(decryptPaymentDataRequest argument)
        {
            if (null != argument)
            {
                opaqueDataType(argument.opaqueData);
            }
        }
        public static void opaqueDataType(opaqueDataType argument)
        {
            if (null != argument)
            {
            }
        }
		
		public static void processorType(processorType argument) 
        {
            if(null != argument) 
            {
            }
        }

        public static void customerPaymentProfileListItemType(customerPaymentProfileListItemType argument)
        {
            if (null != argument)
            {
				if(argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified=true;}
                customerAddressType(argument.billTo);
                paymentMaskedType(argument.payment);
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
                paymentScheduleType(argument.paymentSchedule);
                if (0 <= argument.amount) { argument.amountSpecified = true; }
                if (0 <= argument.trialAmount) { argument.trialAmountSpecified = true; }
                if (0 <= argument.status) { argument.statusSpecified = true; }
                subscriptionCustomerProfileType(argument.profile);
                orderType(argument.order);
            }
        }

        public static void subscriptionCustomerProfileType(subscriptionCustomerProfileType argument)
        {
            if (null != argument)
            {
                customerProfileExType(argument);
                customerPaymentProfileMaskedType(argument.paymentProfile);
                customerAddressExType(argument.shippingProfile);
            }
        }

        public static void paymentSimpleType(paymentSimpleType argument)
        {
            if (null != argument)
            {
                if (argument.Item is bankAccountType) { bankAccountType(argument.Item as bankAccountType); }
                if (argument.Item is creditCardSimpleType) { creditCardSimpleType(argument.Item as creditCardSimpleType); }
            }
        }
        public static void bankAccountType(bankAccountType argument)
        {
            if (null != argument)
            {
                if (0 <= (int)argument.accountType) { argument.accountTypeSpecified = true; }
                if (0 <= (int)argument.echeckType) { argument.echeckTypeSpecified = true; }
            }
        }
        public static void creditCardSimpleType(creditCardSimpleType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void creditCardType(creditCardType argument)
        {
            if (null != argument)
            {
                creditCardSimpleType(argument);
                if (argument.isPaymentToken) { argument.isPaymentTokenSpecified = true; }
            }
        }
        public static void customerProfileSummaryType(customerProfileSummaryType argument)
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
            if(null != argument) 
            {
                //(argument.orderDescending);
            }
        }
        public static void heldTransactionRequestType(heldTransactionRequestType argument) 
        {
            if(null != argument) 
            {
            }
        }
		
        public static void ARBGetSubscriptionListSorting(ARBGetSubscriptionListSorting argument)
        {
            if (null != argument)
            {

            }
        }
        public static void permissionType(permissionType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void merchantContactType(merchantContactType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void mobileDeviceType(mobileDeviceType argument)
        {
            if (null != argument)
            {
                if (0 <= (int)argument.deviceActivation) { argument.deviceActivationSpecified = true; }
            }
        }
        public static void transactionSummaryType(transactionSummaryType argument)
        {
            if (null != argument)
            {
                subscriptionPaymentType(argument.subscription);
                if (argument.hasReturnedItems) { argument.hasReturnedItemsSpecified = true; }
                fraudInformationType(argument.fraudInformation); 
                customerProfileIdType(argument.profile);
			}
        }
        public static void subscriptionPaymentType(subscriptionPaymentType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void createProfileResponse(createProfileResponse argument)
        {
            if (null != argument)
            {
                messagesType(argument.messages);
            }
        }
        public static void messagesType(messagesType argument)
        {
            if (null != argument)
            {
                if (null != argument.message) { foreach (var value in argument.message) { messagesTypeMessage(value); } }
            }
        }
        public static void messagesTypeMessage(messagesTypeMessage argument)
        {
            if (null != argument)
            {
            }
        }
        public static void ArrayOfSetting(ArrayOfSetting argument)
        {
            if (null != argument)
            {
                if (null != argument.setting) { foreach (var value in argument.setting) { settingType(value); } }
            }
        }
        public static void settingType(settingType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void emailSettingsType(emailSettingsType argument)
        {
            if (null != argument)
            {
                ArrayOfSetting(argument);
            }
        }
		
        public static void fraudInformationType(fraudInformationType argument) 
        {
            if(null != argument) 
            {
            }
        }
		
        public static void transRetailInfoType(transRetailInfoType argument)
        {
            if (null != argument)
            {
                argument.marketType = "2";
            }
        }
        public static void ccAuthenticationType(ccAuthenticationType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void paymentProfile(paymentProfile argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerProfilePaymentType(customerProfilePaymentType argument)
        {
            if (null != argument)
            {
                if (argument.createProfile) { argument.createProfileSpecified = true; }
                paymentProfile(argument.paymentProfile);
            }
        }
        public static void transactionRequestType(transactionRequestType argument)
        {
            if (null != argument)
            {
                if (0 <= argument.amount) { argument.amountSpecified = true; }
                paymentType(argument.payment);
                customerProfilePaymentType(argument.profile);
                solutionType(argument.solution);
                orderType(argument.order);
                if (null != argument.lineItems) { foreach (var value in argument.lineItems) { lineItemType(value); } }
                extendedAmountType(argument.tax);
                extendedAmountType(argument.duty);
                extendedAmountType(argument.shipping);
                if (argument.taxExempt) { argument.taxExemptSpecified = true; }
                customerDataType(argument.customer);
                customerAddressType(argument.billTo);
                nameAndAddressType(argument.shipTo);
                ccAuthenticationType(argument.cardholderAuthentication);
                transRetailInfoType(argument.retail);
                if (null != argument.transactionSettings) { foreach (var value in argument.transactionSettings) { settingType(value); } }
                if (null != argument.userFields) { foreach (var value in argument.userFields) { userField(value); } }
            }
        }
        public static void paymentType(paymentType argument)
        {
            if (null != argument)
            {
                if (argument.Item is bankAccountType) { bankAccountType(argument.Item as bankAccountType); }
                if (argument.Item is creditCardType) { creditCardType(argument.Item as creditCardType); }
                if (argument.Item is encryptedTrackDataType)
                {
                    encryptedTrackDataType(argument.Item as encryptedTrackDataType);
                }
				
                if (argument.Item is paymentEmvType) { paymentEmvType(argument.Item as paymentEmvType); }
                if (argument.Item is opaqueDataType) { opaqueDataType(argument.Item as opaqueDataType); }
                if (argument.Item is payPalType) { payPalType(argument.Item as payPalType); }
                if (argument.Item is creditCardTrackType) { creditCardTrackType(argument.Item as creditCardTrackType); }
            }
        }
        public static void encryptedTrackDataType(encryptedTrackDataType argument)
        {
            if (null != argument)
            {
                KeyBlock(argument.FormOfPayment);
            }
        }
		
		public static void paymentEmvType(paymentEmvType argument) 
        {
            if(null != argument) 
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
        public static void payPalType(payPalType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void creditCardTrackType(creditCardTrackType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void solutionType(solutionType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void orderType(orderType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void orderExType(orderExType argument)
        {
            if (null != argument)
            {
                orderType(argument);
            }
        }
        public static void lineItemType(lineItemType argument)
        {
            if (null != argument)
            {
                if (argument.taxable) { argument.taxableSpecified = true; }
            }
        }
        public static void extendedAmountType(extendedAmountType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerDataType(customerDataType argument)
        {
            if (null != argument)
            {
                if (0 <= (int)argument.type) { argument.typeSpecified = true; }
                driversLicenseType(argument.driversLicense);
            }
        }
        public static void driversLicenseType(driversLicenseType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerAddressType(customerAddressType argument)
        {
            if (null != argument)
            {
                nameAndAddressType(argument);
            }
        }
        public static void nameAndAddressType(nameAndAddressType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerAddressExType(customerAddressExType argument)
        {
            if (null != argument)
            {
                customerAddressType(argument);
            }
        }
        public static void userField(userField argument)
        {
            if (null != argument)
            {
            }
        }
        public static void returnedItemType(returnedItemType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void batchStatisticType(batchStatisticType argument)
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
        public static void batchDetailsType(batchDetailsType argument)
        {
            if (null != argument)
            {
                if (DateTime.MinValue != argument.settlementTimeUTC) { argument.settlementTimeUTCSpecified = true; }
                if (DateTime.MinValue != argument.settlementTimeLocal) { argument.settlementTimeLocalSpecified = true; }
                if (null != argument.statistics) { foreach (var value in argument.statistics) { batchStatisticType(value); } }
            }
        }
        public static void FDSFilterType(FDSFilterType argument)
        {
            if (null != argument)
            {
            }
        }

        public static void transactionDetailsTypeTag(transactionDetailsTypeTag argument)
        {
            if (null != argument)
            {
            }
        }

        public static void transactionDetailsType(transactionDetailsType argument)
        {
            if (null != argument)
            {
                subscriptionPaymentType(argument.subscription);
                if (null != argument.FDSFilters) { foreach (var value in argument.FDSFilters) { FDSFilterType(value); } }
                batchDetailsType(argument.batch);
                orderExType(argument.order);
                if (0 <= argument.requestedAmount) { argument.requestedAmountSpecified = true; }
                extendedAmountType(argument.tax);
                extendedAmountType(argument.shipping);
                extendedAmountType(argument.duty);
                if (null != argument.lineItems) { foreach (var value in argument.lineItems) { lineItemType(value); } }
                if (0 <= argument.prepaidBalanceRemaining) { argument.prepaidBalanceRemainingSpecified = true; }
                if (argument.taxExempt) { argument.taxExemptSpecified = true; }
                paymentMaskedType(argument.payment);
                customerDataType(argument.customer);
                customerAddressType(argument.billTo);
                nameAndAddressType(argument.shipTo);
                if (argument.recurringBilling) { argument.recurringBillingSpecified = true; }
                if (null != argument.returnedItems) { foreach (var value in argument.returnedItems) { returnedItemType(value); } }
                solutionType(argument.solution);

				if(null != argument.emvDetails){ foreach( var value in argument.emvDetails) { transactionDetailsTypeTag(value);} } 
				
                customerProfileIdType(argument.profile);
			}
        }
        public static void paymentMaskedType(paymentMaskedType argument)
        {
            if (null != argument)
            {
                if (argument.Item is bankAccountMaskedType) { bankAccountMaskedType(argument.Item as bankAccountMaskedType); }
                if (argument.Item is creditCardMaskedType) { creditCardMaskedType(argument.Item as creditCardMaskedType); }
                if (argument.Item is tokenMaskedType)
                {
                    tokenMaskedType(argument.Item as tokenMaskedType);
                }

            }


        }
        public static void bankAccountMaskedType(bankAccountMaskedType argument)
        {
            if (null != argument)
            {
                if (0 <= argument.accountType) { argument.accountTypeSpecified = true; }
                if (0 <= argument.echeckType) { argument.echeckTypeSpecified = true; }
            }
        }
        public static void creditCardMaskedType(creditCardMaskedType argument)
        {
            if (null != argument)
            {
                cardArt(argument.cardArt);
            }
        }
        public static void cardArt(cardArt argument)
        {
            if (null != argument)
            {
            }
        }
        public static void tokenMaskedType(tokenMaskedType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void transactionResponse(transactionResponse argument)
        {
            if (null != argument)
            {
                transactionResponsePrePaidCard(argument.prePaidCard);
                if (null != argument.messages) { foreach (var value in argument.messages) { transactionResponseMessage(value); } }
                if (null != argument.errors) { foreach (var value in argument.errors) { transactionResponseError(value); } }
                if (null != argument.splitTenderPayments) { foreach (var value in argument.splitTenderPayments) { transactionResponseSplitTenderPayment(value); } }
                if (null != argument.userFields) { foreach (var value in argument.userFields) { userField(value); } }
                nameAndAddressType(argument.shipTo);
                transactionResponseSecureAcceptance(argument.secureAcceptance);
                transactionResponseEmvResponse(argument.emvResponse);
                customerProfileIdType(argument.profile);				
			}
        }

        public static void transactionResponseEmvResponse(transactionResponseEmvResponse argument)
        {
            if (null != argument)
            {
                if (null != argument.tags) { foreach (var value in argument.tags) { emvTag(value); } }
            }
        }

        public static void transactionResponsePrePaidCard(transactionResponsePrePaidCard argument)
        {
            if (null != argument)
            {
            }
        }
        public static void transactionResponseMessage(transactionResponseMessage argument)
        {
            if (null != argument)
            {
            }
        }
        public static void transactionResponseError(transactionResponseError argument)
        {
            if (null != argument)
            {
            }
        }
        public static void transactionResponseSplitTenderPayment(transactionResponseSplitTenderPayment argument)
        {
            if (null != argument)
            {
            }
        }
        public static void transactionResponseSecureAcceptance(transactionResponseSecureAcceptance argument)
        {
            if (null != argument)
            {
            }
        }
        public static void profileTransVoidType(profileTransVoidType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void profileTransAmountType(profileTransAmountType argument)
        {
            if (null != argument)
            {
                extendedAmountType(argument.tax);
                extendedAmountType(argument.shipping);
                extendedAmountType(argument.duty);
                if (null != argument.lineItems) { foreach (var value in argument.lineItems) { lineItemType(value); } }
            }
        }
        public static void profileTransRefundType(profileTransRefundType argument)
        {
            if (null != argument)
            {
                profileTransAmountType(argument);
                orderExType(argument.order);
            }
        }
        public static void profileTransPriorAuthCaptureType(profileTransPriorAuthCaptureType argument)
        {
            if (null != argument)
            {
                profileTransAmountType(argument);
            }
        }
        public static void profileTransOrderType(profileTransOrderType argument)
        {
            if (null != argument)
            {
                profileTransAmountType(argument);
                orderExType(argument.order);
                if (argument.taxExempt) { argument.taxExemptSpecified = true; }
                if (argument.recurringBilling) { argument.recurringBillingSpecified = true; }
            }
        }
        public static void profileTransCaptureOnlyType(profileTransCaptureOnlyType argument)
        {
            if (null != argument)
            {
                profileTransOrderType(argument);
            }
        }
        public static void profileTransAuthOnlyType(profileTransAuthOnlyType argument)
        {
            if (null != argument)
            {
                profileTransOrderType(argument);
            }
        }
        public static void profileTransAuthCaptureType(profileTransAuthCaptureType argument)
        {
            if (null != argument)
            {
                profileTransOrderType(argument);
            }
        }
        public static void profileTransactionType(profileTransactionType argument)
        {
            if (null != argument)
            {
                if (argument.Item is profileTransAuthCaptureType) { profileTransAuthCaptureType(argument.Item as profileTransAuthCaptureType); }
                if (argument.Item is profileTransAuthOnlyType) { profileTransAuthOnlyType(argument.Item as profileTransAuthOnlyType); }
                if (argument.Item is profileTransCaptureOnlyType) { profileTransCaptureOnlyType(argument.Item as profileTransCaptureOnlyType); }
                if (argument.Item is profileTransPriorAuthCaptureType) { profileTransPriorAuthCaptureType(argument.Item as profileTransPriorAuthCaptureType); }
                if (argument.Item is profileTransRefundType) { profileTransRefundType(argument.Item as profileTransRefundType); }
                if (argument.Item is profileTransVoidType) { profileTransVoidType(argument.Item as profileTransVoidType); }
            }
        }
        public static void driversLicenseMaskedType(driversLicenseMaskedType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerPaymentProfileBaseType(customerPaymentProfileBaseType argument)
        {
            if (null != argument)
            {
                if (0 <= argument.customerType) { argument.customerTypeSpecified = true; }
                customerAddressType(argument.billTo);
            }
        }
        public static void customerPaymentProfileMaskedType(customerPaymentProfileMaskedType argument)
        {
            if (null != argument)
            {
                customerPaymentProfileBaseType(argument);
				if(argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified=true;}
                paymentMaskedType(argument.payment);
                driversLicenseMaskedType(argument.driversLicense);
            }
        }
        public static void customerPaymentProfileType(customerPaymentProfileType argument)
        {
            if (null != argument)
            {
                customerPaymentProfileBaseType(argument);
                paymentType(argument.payment);
                driversLicenseType(argument.driversLicense);
				if(argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified=true;}
            }
        }
        public static void customerPaymentProfileExType(customerPaymentProfileExType argument)
        {
            if (null != argument)
            {
                customerPaymentProfileType(argument);
            }
        }
        public static void customerProfileBaseType(customerProfileBaseType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void customerProfileExType(customerProfileExType argument)
        {
            if (null != argument)
            {
                customerProfileBaseType(argument);
            }
        }
        public static void customerProfileMaskedType(customerProfileMaskedType argument)
        {
            if (null != argument)
            {
                customerProfileExType(argument);
                if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { customerPaymentProfileMaskedType(value); } }
                if (null != argument.shipToList) { foreach (var value in argument.shipToList) { customerAddressExType(value); } }
            }
        }
        public static void customerProfileType(customerProfileType argument)
        {
            if (null != argument)
            {
                customerProfileBaseType(argument);
                if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { customerPaymentProfileType(value); } }
                if (null != argument.shipToList) { foreach (var value in argument.shipToList) { customerAddressType(value); } }
            }
        }
        public static void customerType(customerType argument)
        {
            if (null != argument)
            {
                if (0 <= argument.type) { argument.typeSpecified = true; }
                driversLicenseType(argument.driversLicense);
            }
        }
        public static void paymentScheduleType(paymentScheduleType argument)
        {
            if (null != argument)
            {
                paymentScheduleTypeInterval(argument.interval);
                if (DateTime.MinValue != argument.startDate) { argument.startDateSpecified = true; }
                if (0 <= argument.totalOccurrences) { argument.totalOccurrencesSpecified = true; }
                if (0 <= argument.trialOccurrences) { argument.trialOccurrencesSpecified = true; }
            }
        }

        public static void paymentScheduleTypeInterval(paymentScheduleTypeInterval argument)
        {
            if (null != argument)
            {
            }
        }

        public static void customerProfileIdType(customerProfileIdType argument)
        {
            if (null != argument)
            {
            }
        }

        public static void ARBSubscriptionType(ARBSubscriptionType argument)
        {
            if (null != argument)
            {
                paymentScheduleType(argument.paymentSchedule);
                if (0 < argument.amount) { argument.amountSpecified = true; }

                paymentType(argument.payment);
                orderType(argument.order);
                customerType(argument.customer);
                nameAndAddressType(argument.billTo);
                nameAndAddressType(argument.shipTo);
                customerProfileIdType(argument.profile);
            }
        }

        public static void ARBSubscriptionTypeSetTrialAmountSpecified(ARBSubscriptionType argument)
        {
            if (null != argument)
            {
                if (0 <= argument.trialAmount) { argument.trialAmountSpecified = true; }
            }
        }

        public static void paymentDetails(paymentDetails argument)
        {
            if (null != argument)
            {
            }
        }
        public static void fingerPrintType(fingerPrintType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void impersonationAuthenticationType(impersonationAuthenticationType argument)
        {
            if (null != argument)
            {
            }
        }
        public static void merchantAuthenticationType(merchantAuthenticationType argument)
        {
            if (null != argument)
            {
                if (argument.Item is fingerPrintType)
                {
                    fingerPrintType(argument.Item as fingerPrintType);
                }
                if (argument.Item is impersonationAuthenticationType) { impersonationAuthenticationType(argument.Item as impersonationAuthenticationType); }
            }
        }
        public static void ANetApiRequest(ANetApiRequest argument)
        {
            if (null != argument)
            {
                merchantAuthenticationType(argument.merchantAuthentication);
            }
        }
        public static void decryptPaymentDataResponse(decryptPaymentDataResponse argument)
        {
            if (null != argument)
            {
                customerAddressType(argument.shippingInfo);
                customerAddressType(argument.billingInfo);
                creditCardMaskedType(argument.cardInfo);
                paymentDetails(argument.paymentDetails);
            }
        }
        public static void ANetApiResponse(ANetApiResponse argument)
        {
            if (null != argument)
            {
                messagesType(argument.messages);
            }
        }
        public static void isAliveRequest(isAliveRequest argument)
        {
            if (null != argument)
            {
            }
        }


        // Added this method because isAliveRequest take AnetApiRequest as argument.
        // AnetApiSchema.generated.cs - isAliveRequest is not the child class of AnetApiRequest class.
        public static void isAliveRequest(ANetApiRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void isAliveResponse(isAliveResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void authenticateTestRequest(authenticateTestRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void authenticateTestResponse(authenticateTestResponse argument)
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
                customerProfileIdType(argument.profile);
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
                customerProfileIdType(argument.profile);
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
        public static void createCustomerProfileRequest(createCustomerProfileRequest argument)
        {
            if (null != argument)
            {
                customerProfileType(argument.profile);
                if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
            }
        }
        public static void createCustomerProfileResponse(createCustomerProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void createCustomerPaymentProfileRequest(createCustomerPaymentProfileRequest argument)
        {
            if (null != argument)
            {
                customerPaymentProfileType(argument.paymentProfile);
                if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
            }
        }
        public static void createCustomerPaymentProfileResponse(createCustomerPaymentProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void createCustomerShippingAddressRequest(createCustomerShippingAddressRequest argument)
        {
            if (null != argument)
            {
                customerAddressType(argument.address);
            }
        }
        public static void createCustomerShippingAddressResponse(createCustomerShippingAddressResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void createCustomerProfileFromTransactionRequest(createCustomerProfileFromTransactionRequest argument)
        {
            if (null != argument)
            {				
                customerProfileBaseType(argument.customer);
                if(argument.defaultPaymentProfile) { argument.defaultPaymentProfileSpecified=true;}
                if(argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified=true;}
            }
        }
        public static void getCustomerProfileRequest(getCustomerProfileRequest argument)
        {
            if (null != argument)
            {
				if(argument.unmaskExpirationDate) { argument.unmaskExpirationDateSpecified=true;}
            }
        }
        public static void getCustomerProfileResponse(getCustomerProfileResponse argument)
        {
            if (null != argument)
            {
                customerProfileMaskedType(argument.profile);
            }
        }
        public static void getCustomerPaymentProfileRequest(getCustomerPaymentProfileRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getCustomerPaymentProfileResponse(getCustomerPaymentProfileResponse argument)
        {
            if (null != argument)
            {
                customerPaymentProfileMaskedType(argument.paymentProfile);
            }
        }
        public static void getCustomerShippingAddressRequest(getCustomerShippingAddressRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getCustomerShippingAddressResponse(getCustomerShippingAddressResponse argument)
        {
            if (null != argument)
            {
				if(argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified=true;}
                customerAddressExType(argument.address);
            }
        }
        public static void updateCustomerProfileRequest(updateCustomerProfileRequest argument)
        {
            if (null != argument)
            {
                customerProfileExType(argument.profile);
            }
        }
        public static void updateCustomerProfileResponse(updateCustomerProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void updateCustomerPaymentProfileRequest(updateCustomerPaymentProfileRequest argument)
        {
            if (null != argument)
            {
                customerPaymentProfileExType(argument.paymentProfile);
                if (0 <= argument.validationMode) { argument.validationModeSpecified = true; }
            }
        }
        public static void updateCustomerPaymentProfileResponse(updateCustomerPaymentProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void updateCustomerShippingAddressRequest(updateCustomerShippingAddressRequest argument)
        {
            if (null != argument)
            {
                customerAddressExType(argument.address);
                if(argument.defaultShippingAddress) { argument.defaultShippingAddressSpecified=true;}
			}
        }
        public static void updateCustomerShippingAddressResponse(updateCustomerShippingAddressResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerProfileRequest(deleteCustomerProfileRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerProfileResponse(deleteCustomerProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerPaymentProfileRequest(deleteCustomerPaymentProfileRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerPaymentProfileResponse(deleteCustomerPaymentProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerShippingAddressRequest(deleteCustomerShippingAddressRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void deleteCustomerShippingAddressResponse(deleteCustomerShippingAddressResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void createCustomerProfileTransactionRequest(createCustomerProfileTransactionRequest argument)
        {
            if (null != argument)
            {
                profileTransactionType(argument.transaction);
            }
        }
        public static void createCustomerProfileTransactionResponse(createCustomerProfileTransactionResponse argument)
        {
            if (null != argument)
            {
                transactionResponse(argument.transactionResponse);
            }
        }
        public static void validateCustomerPaymentProfileRequest(validateCustomerPaymentProfileRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void validateCustomerPaymentProfileResponse(validateCustomerPaymentProfileResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getCustomerProfileIdsRequest(getCustomerProfileIdsRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getCustomerProfileIdsResponse(getCustomerProfileIdsResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void updateSplitTenderGroupRequest(updateSplitTenderGroupRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void updateSplitTenderGroupResponse(updateSplitTenderGroupResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getTransactionDetailsRequest(getTransactionDetailsRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getTransactionDetailsResponse(getTransactionDetailsResponse argument)
        {
            if (null != argument)
            {
                transactionDetailsType(argument.transaction);
            }
        }
        public static void createTransactionRequest(createTransactionRequest argument)
        {
            if (null != argument)
            {
                transactionRequestType(argument.transactionRequest);
            }
        }
        public static void createTransactionResponse(createTransactionResponse argument)
        {
            if (null != argument)
            {
                transactionResponse(argument.transactionResponse);
                createProfileResponse(argument.profileResponse);
            }
        }
		
        public static void updateHeldTransactionRequest(updateHeldTransactionRequest argument) 
        {
            if(null != argument) 
            {
                heldTransactionRequestType(argument.heldTransactionRequest);
            }
        }
        public static void updateHeldTransactionResponse(updateHeldTransactionResponse argument) 
        {
            if(null != argument) 
            {
                transactionResponse(argument.transactionResponse);
            }
        }
		
        public static void getBatchStatisticsRequest(getBatchStatisticsRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getBatchStatisticsResponse(getBatchStatisticsResponse argument)
        {
            if (null != argument)
            {
                batchDetailsType(argument.batch);
            }
        }
        public static void getSettledBatchListRequest(getSettledBatchListRequest argument)
        {
            if (null != argument)
            {
                if (argument.includeStatistics) { argument.includeStatisticsSpecified = true; }
                if (DateTime.MinValue != argument.firstSettlementDate) { argument.firstSettlementDateSpecified = true; }
                if (DateTime.MinValue != argument.lastSettlementDate) { argument.lastSettlementDateSpecified = true; }
            }
        }
        public static void getSettledBatchListResponse(getSettledBatchListResponse argument)
        {
            if (null != argument)
            {
                if (null != argument.batchList) { foreach (var value in argument.batchList) { batchDetailsType(value); } }
            }
        }
        public static void getTransactionListRequest(getTransactionListRequest argument)
        {
            if (null != argument)
            {
				TransactionListSorting(argument.sorting);
                Paging(argument.paging);
            }
        }
        public static void getTransactionListResponse(getTransactionListResponse argument)
        {
            if (null != argument)
            {
                if (null != argument.transactions) { foreach (var value in argument.transactions) { transactionSummaryType(value); } }
                if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
			}
        }
        public static void getHostedProfilePageRequest(getHostedProfilePageRequest argument)
        {
            if (null != argument)
            {
                if (null != argument.hostedProfileSettings) { foreach (var value in argument.hostedProfileSettings) { settingType(value); } }
            }
        }
        public static void getHostedProfilePageResponse(getHostedProfilePageResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void getUnsettledTransactionListRequest(getUnsettledTransactionListRequest argument)
        {
            if (null != argument)
            {
                if (0 <= argument.status) { argument.statusSpecified = true; }
                TransactionListSorting(argument.sorting);
                Paging(argument.paging);
            }
        }
		
		public static void getHostedPaymentPageRequest(getHostedPaymentPageRequest argument) 
        {
            if(null != argument) 
            {
                transactionRequestType(argument.transactionRequest);
                if(null != argument.hostedPaymentSettings){ foreach( var value in argument.hostedPaymentSettings) { settingType(value);} } 
            }
        }
        public static void getHostedPaymentPageResponse(getHostedPaymentPageResponse argument) 
        {
            if(null != argument) 
            {
            }
        }
		
        public static void getUnsettledTransactionListResponse(getUnsettledTransactionListResponse argument)
        {
            if (null != argument)
            {
                if (null != argument.transactions) { foreach (var value in argument.transactions) { transactionSummaryType(value); } }
                if (0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified = true; }
			}
        }
        public static void mobileDeviceRegistrationRequest(mobileDeviceRegistrationRequest argument)
        {
            if (null != argument)
            {
                mobileDeviceType(argument.mobileDevice);
            }
        }
        public static void mobileDeviceRegistrationResponse(mobileDeviceRegistrationResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void mobileDeviceLoginRequest(mobileDeviceLoginRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void mobileDeviceLoginResponse(mobileDeviceLoginResponse argument)
        {
            if (null != argument)
            {
                merchantContactType(argument.merchantContact);
                if (null != argument.userPermissions) { foreach (var value in argument.userPermissions) { permissionType(value); } }
                transRetailInfoType(argument.merchantAccount);
            }
        }
        public static void logoutRequest(logoutRequest argument)
        {
            if (null != argument)
            {
            }
        }
        public static void logoutResponse(logoutResponse argument)
        {
            if (null != argument)
            {
            }
        }
        public static void sendCustomerTransactionReceiptRequest(sendCustomerTransactionReceiptRequest argument)
        {
            if (null != argument)
            {
                emailSettingsType(argument.emailSettings);
            }
        }
        public static void sendCustomerTransactionReceiptResponse(sendCustomerTransactionReceiptResponse argument)
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

        public static void getCustomerPaymentProfileListRequest(getCustomerPaymentProfileListRequest argument)
        {
            if (null != argument)
            {
                CustomerPaymentProfileSorting(argument.sorting);
                Paging(argument.paging);
            }
        }
        public static void getCustomerPaymentProfileListResponse(getCustomerPaymentProfileListResponse argument)
        {
            if (null != argument)
            {
                if (null != argument.paymentProfiles) { foreach (var value in argument.paymentProfiles) { customerPaymentProfileListItemType(value); } }
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
                customerProfileSummaryType(argument.customerProfileSummaryType);
                paymentSimpleType(argument.paymentSimpleType);
            }
        }
		
		public static void auDetailsType(auDetailsType argument) 
        {
            if(null != argument) 
            {
            }
        }
        public static void auDeleteType(auDeleteType argument) 
        {
            if(null != argument) 
            {
         auDetailsType (argument);
                creditCardMaskedType(argument.creditCard);
            }
        }

		public static void auUpdateType(auUpdateType argument) 
        {
            if(null != argument) 
            {
				auDetailsType (argument);
                creditCardMaskedType(argument.newCreditCard);
                creditCardMaskedType(argument.oldCreditCard);
            }
        }
        public static void auResponseType(auResponseType argument) 
        {
            if(null != argument) 
            {
            }
        }

        public static void emvTag(emvTag argument) 
        {
            if(null != argument) 
            {
            }
        }


        public static void getAUJobSummaryRequest(getAUJobSummaryRequest argument) 
        {
            if(null != argument) 
            {
            }
        }
        public static void getAUJobSummaryResponse(getAUJobSummaryResponse argument) 
        {
            if(null != argument) 
            {
                if(null != argument.auSummary){ foreach( var value in argument.auSummary) { auResponseType(value);} } 
            }
        }
        public static void getAUJobDetailsRequest(getAUJobDetailsRequest argument) 
        {
            if(null != argument) 
            {
                if(0 <= argument.modifiedTypeFilter) { argument.modifiedTypeFilterSpecified=true;}
                Paging(argument.paging);
            }
        }
        public static void getAUJobDetailsResponse(getAUJobDetailsResponse argument) 
        {
            if(null != argument) 
            {
                if(0 <= argument.totalNumInResultSet) { argument.totalNumInResultSetSpecified=true;}
                if(null != argument.auDetails){ foreach( var value in argument.auDetails) { auDetailsType(value);} } 
            }
        }

		public static void getMerchantDetailsRequest(getMerchantDetailsRequest argument) 
        {
            if(null != argument) 
            {
            }
        }

        public static void getMerchantDetailsResponse(getMerchantDetailsResponse argument) 
        {
            if(null != argument) 
            {
                if(argument.isTestMode) { argument.isTestModeSpecified=true;}
                if(null != argument.processors){ foreach( var value in argument.processors) { processorType(value);} }
            }
        }
		
        public static void getTransactionListForCustomerRequest(getTransactionListForCustomerRequest argument) 
        {
            if(null != argument) 
            {
                TransactionListSorting(argument.sorting);
                Paging(argument.paging);
            }
        }
	}
	
	
    // ReSharper restore InconsistentNaming 
#pragma warning restore 1591
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
updateCustomerPaymentProfileRequest  
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
