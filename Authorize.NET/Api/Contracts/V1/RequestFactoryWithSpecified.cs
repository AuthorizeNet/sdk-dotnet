namespace AuthorizeNet.Api.Contracts.V1
{
    using System;

#pragma warning disable 169
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
    /// <summary>
    /// Special case handlers
    /// 
    /// validated on 2014/07/09 for objects listed at the end 
    /// should be validated after each update of AnetApiSchema.cs 
    /// for fields/properties that are minOccurs="0" since xsd.exe 
    /// generates "specified" property for such fields and requires 
    /// special handling to set them seamlessly
    /// Make sure to update the respective controllers to call the respective request hand
    /// 
    /// </summary>
    public static class RequestFactoryWithSpecified
    {
        public static void decryptPaymentDataType(decryptPaymentDataRequest request) { }

        public static void messagesType ( messagesType request) { }

        public static void messagesTypeMessage ( messagesTypeMessage request) { }

        public static void paymentSimpleType(paymentSimpleType request)
        {
            if (null != request)
            {
                if (request.Item is bankAccountType) { bankAccountType(request.Item as bankAccountType); }
                if (request.Item is creditCardSimpleType) { creditCardSimpleType(request.Item as creditCardSimpleType); }
            }
        }

        public static void bankAccountType(bankAccountType request)
        {
            if (null != request)
            {
                if (0 <= (int)request.accountType) { request.accountTypeSpecified = true; }
                if (0 <= (int)request.echeckType) { request.echeckTypeSpecified = true; }
            }
        }

        public static void creditCardSimpleType ( creditCardSimpleType request) { }

        public static void creditCardType(creditCardType request) 
        { 
           if (request != null)
           {
               if (request.isPaymentToken)
               {
                   request.isPaymentTokenSpecified = true;
               }    
           }
            
        }

        public static void searchCriteriaCustomerProfileType ( searchCriteriaCustomerProfileType request) { }

        public static void customerProfileSummaryType ( customerProfileSummaryType request) { }
    
        public static void subscriptionDetail ( SubscriptionDetail request) {}
    
        public static void paging ( Paging request) { }
    
        public static void ARBGetSubscriptionListSorting ( ARBGetSubscriptionListSorting request) { }
    
        public static void permissionType ( permissionType request) { }
    
        public static void merchantContactType ( merchantContactType request) { }

        public static void mobileDeviceType(mobileDeviceType request)
        {
            if (null != request)
            {
                if (0 <= (int)request.deviceActivation) { request.deviceActivationSpecified = true; }
            }
        }

        public static void transactionSummaryType(transactionSummaryType request)
        {
            if (null != request)
            {
                if (request.hasReturnedItems) { request.hasReturnedItemsSpecified = true; }
            }
        }

        public static void subscriptionPaymentType ( subscriptionPaymentType request) {}
    
        public static void ArrayOfSetting ( ArrayOfSetting request) { }
    
        public static void settingType ( settingType request) { }
    
        public static void emailSettingsType ( emailSettingsType request) {}
    
        public static void transRetailInfoType ( transRetailInfoType request) {}
    
        public static void ccAuthenticationType ( ccAuthenticationType request) { }
    
        public static void paymentProfile ( paymentProfile request) { }
    
        public static void customerProfilePaymentType(customerProfilePaymentType request)
        {
            if (request.createProfile)
            {
                request.createProfileSpecified = true;
            }
         }

        public static void transactionRequestType(transactionRequestType request)
        {
            if (null != request)
            {
                if (0 <= request.amount) { request.amountSpecified = true; }
                if (request.taxExempt) { request.taxExemptSpecified = true; }
                customerProfilePaymentType(request.profile);
                lineItemType(request.lineItems);
            }
        }

        public static void paymentType(paymentType request)
        {
            if (null != request)
            {
                if (request.Item is bankAccountType) { bankAccountType(request.Item as bankAccountType); }
                if (request.Item is creditCardType) { creditCardType(request.Item as creditCardType); }
            }
        }
    
        public static void encryptedTrackDataType ( encryptedTrackDataType request) { }
    
        public static void KeyBlock ( KeyBlock request) { }
    
        public static void KeyValue ( KeyValue request) { }
    
        public static void KeyManagementScheme ( KeyManagementScheme request) { }
    
        public static void KeyManagementSchemeDUKPT ( KeyManagementSchemeDUKPT request) { }
    
        public static void KeyManagementSchemeDUKPTMode ( KeyManagementSchemeDUKPTMode request) { }
    
        public static void KeyManagementSchemeDUKPTDeviceInfo ( KeyManagementSchemeDUKPTDeviceInfo request) { }
    
        public static void KeyManagementSchemeDUKPTEncryptedData ( KeyManagementSchemeDUKPTEncryptedData request) { }
    
        public static void payPalType ( payPalType request) { }
    
        public static void creditCardTrackType ( creditCardTrackType request) { }
    
        public static void orderType ( orderType request) { }
    
        public static void orderExType ( orderExType request) { }

        public static void lineItemType(lineItemType request)
        {
            if (null != request)
            {
                if (request.taxable) { request.taxableSpecified = true; }
            }
        }

        public static void extendedAmountType ( extendedAmountType request) { }

        public static void customerDataType(customerDataType request)
        {
            if (null != request)
            {
                if (0 <= (int)request.type) { request.typeSpecified = true; }
            }
        }

        public static void driversLicenseType ( driversLicenseType request) { }
    
        public static void customerAddressType ( customerAddressType request) { }
    
        public static void nameAndAddressType ( nameAndAddressType request) { }
    
        public static void customerAddressExType ( customerAddressExType request) { }
    
        public static void userField ( userField request) { }
    
        public static void solutionType ( solutionType request) { }
    
        public static void returnedItemType ( returnedItemType request) { }
    
        public static void batchStatisticType ( batchStatisticType request) 
        {
            if (null != request)
            {
                if (0 <= request.returnedItemAmount) { request.returnedItemAmountSpecified = true; }
                if (0 <= request.returnedItemCount) { request.returnedItemCountSpecified = true; }
                if (0 <= request.chargebackAmount) { request.chargebackAmountSpecified = true; }
                if (0 <= request.chargebackCount) { request.chargebackCountSpecified = true; }
                if (0 <= request.correctionNoticeCount) { request.correctionNoticeCountSpecified = true; }
                if (0 <= request.chargeChargeBackAmount) { request.chargeChargeBackAmountSpecified = true; }
                if (0 <= request.chargeChargeBackCount) { request.chargeChargeBackCountSpecified = true; }
                if (0 <= request.refundChargeBackAmount) { request.refundChargeBackAmountSpecified = true; }
                if (0 <= request.refundChargeBackCount) { request.refundChargeBackCountSpecified = true; }
                if (0 <= request.chargeReturnedItemsAmount) { request.chargeReturnedItemsAmountSpecified = true; }
                if (0 <= request.chargeReturnedItemsCount) { request.chargeReturnedItemsCountSpecified = true; }
                if (0 <= request.refundReturnedItemsAmount) { request.refundReturnedItemsAmountSpecified = true; }
                if (0 <= request.refundReturnedItemsCount) { request.refundReturnedItemsCountSpecified = true; }
            }
        }
    
        public static void batchDetailsType ( batchDetailsType request) 
        {
            if (null != request)
            {
                if (DateTime.MinValue != request.settlementTimeUTC) { request.settlementTimeUTCSpecified = true; }
                if (DateTime.MinValue != request.settlementTimeLocal) { request.settlementTimeLocalSpecified = true; }
            }
        }

        public static void FDSFilterType ( FDSFilterType request) { }
    
        public static void transactionDetailsType ( transactionDetailsType request) 
        {
            if (null != request)
            {
                if (0 <= request.requestedAmount) { request.requestedAmountSpecified = true; }
                if (0 <= request.prepaidBalanceRemaining) { request.prepaidBalanceRemainingSpecified = true; }
                if (request.taxExempt) { request.taxExemptSpecified = true; }
                if (request.recurringBilling) { request.recurringBillingSpecified = true; }
                lineItemType(request.lineItems);
            }
        }

        public static void paymentMaskedType ( paymentMaskedType request)
        {
            if (null != request)
            {
                if (request.Item is bankAccountMaskedType) { bankAccountMaskedType(request.Item as bankAccountMaskedType); }
                if (request.Item is creditCardMaskedType) { creditCardMaskedType(request.Item as creditCardMaskedType); }
            }
        }

        public static void bankAccountMaskedType ( bankAccountMaskedType request) 
        {
            if (null != request)
            {
                if (0 <= request.accountType) { request.accountTypeSpecified = true; }
                if (0 <= request.echeckType) { request.echeckTypeSpecified = true; }
            }
        }
    
        public static void creditCardMaskedType ( creditCardMaskedType request) { }
    
        public static void profileTransVoidType ( profileTransVoidType request) { }
    
        public static void profileTransAmountType(profileTransAmountType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransRefundType(profileTransRefundType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransPriorAuthCaptureType(profileTransPriorAuthCaptureType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransOrderType ( profileTransOrderType request) 
        { 
            if (null != request)
            {
                if (request.taxExempt) { request.taxExemptSpecified = true; }
                if (request.recurringBilling) { request.recurringBillingSpecified = true; }
                lineItemType(request.lineItems);
            }
        }

        public static void profileTransCaptureOnlyType(profileTransCaptureOnlyType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransAuthOnlyType(profileTransAuthOnlyType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransAuthCaptureType(profileTransAuthCaptureType request)
        {
            if (null != request)
            {
                lineItemType(request.lineItems);
            }
        }
    
        public static void profileTransactionType(profileTransactionType request)
        {
            if (null != request)
            {
                if (request.Item is profileTransAuthCaptureType) { profileTransAuthCaptureType(request.Item as profileTransAuthCaptureType); }
                if (request.Item is profileTransAuthOnlyType) { profileTransAuthOnlyType(request.Item as profileTransAuthOnlyType); }            
                if (request.Item is profileTransCaptureOnlyType) { profileTransCaptureOnlyType(request.Item as profileTransCaptureOnlyType); }            
                if (request.Item is profileTransPriorAuthCaptureType) { profileTransPriorAuthCaptureType(request.Item as profileTransPriorAuthCaptureType); }            
                if (request.Item is profileTransRefundType) { profileTransRefundType(request.Item as profileTransRefundType); }            
                if (request.Item is profileTransVoidType) { profileTransVoidType(request.Item as profileTransVoidType); }            
            }
        }
    
        public static void driversLicenseMaskedType ( driversLicenseMaskedType request) { }
    
        public static void customerPaymentProfileBaseType ( customerPaymentProfileBaseType request) 
        { 
            if (null != request)
            {
                if (0 <= request.customerType) { request.customerTypeSpecified = true; }
            }
        }
    
        public static void customerPaymentProfileMaskedType ( customerPaymentProfileMaskedType request)
        {
            if (null != request)
            {
                paymentMaskedType(request.payment);
            }
        }
    
        public static void customerPaymentProfileType(customerPaymentProfileType request)
        {
            if (null != request)
            {
                    paymentType(request.payment);
            }
        }
    
        public static void customerPaymentProfileExType(customerPaymentProfileExType request) 
        {
            if (null != request)
            {
                customerPaymentProfileType(request);
                paymentType(request.payment);
            }
        }
    
        public static void customerProfileBaseType ( customerProfileBaseType request) { }
    
        public static void customerProfileExType ( customerProfileExType request) { }
    
        public static void customerProfileMaskedType(customerProfileMaskedType request)
        {
            if (null != request)
            {
                if (null != request.paymentProfiles)
                {
                    foreach (var paymentProfile in request.paymentProfiles)
                    {
                        customerPaymentProfileMaskedType(paymentProfile);
                    }
                }
            }
        }
    
        public static void customerProfileType(customerProfileType request)
        {
            if (null != request)
            {
                if (null != request.paymentProfiles)
                {
                    foreach (var paymentProfile in request.paymentProfiles)
                    {
                        customerPaymentProfileType(paymentProfile);
                    }
                }
            }
        }
    
        public static void customerType ( customerType request) 
        { 
            if (null != request)
            {
                if (0 <= request.type) { request.typeSpecified = true; }
            }
        }    

        public static void paymentScheduleType ( paymentScheduleType request) 
        {
            if ( null != request)
            {
                if (DateTime.MinValue != request.startDate) { request.startDateSpecified = true; }
                if (0 <= request.totalOccurrences) { request.totalOccurrencesSpecified = true; }
                if (0 <= request.trialOccurrences) { request.trialOccurrencesSpecified = true; }
            }
        }    

        public static void paymentScheduleTypeInterval ( paymentScheduleTypeInterval request) { }
    
        public static void ARBSubscriptionType ( ARBSubscriptionType request) 
        {
            if (null != request)
            {
                if (0 <= request.amount) { request.amountSpecified = true; }
                if (0 <= request.trialAmount) { request.trialAmountSpecified = true; }
                paymentType(request.payment);
                paymentScheduleType(request.paymentSchedule);
            }
        }
    
        public static void impersonationAuthenticationType ( impersonationAuthenticationType request) { }
    
        public static void merchantAuthenticationType ( merchantAuthenticationType request) { }
    
        public static void ANetApiRequest ( ANetApiRequest request) { }
    
        public static void isAliveRequest ( isAliveRequest request) { }
    
        public static void authenticateTestRequest ( authenticateTestRequest request) { }
    
        public static void ARBCreateSubscriptionRequest(ARBCreateSubscriptionRequest request)
        {
            if (null != request)
            {
                ARBSubscriptionType(request.subscription);
            }
        }
    
        public static void ARBUpdateSubscriptionRequest(ARBUpdateSubscriptionRequest request)
        {
            if (null != request)
            {
                ARBSubscriptionType(request.subscription);
            }

        }
    
        public static void ARBCancelSubscriptionRequest ( ARBCancelSubscriptionRequest request) { }
    
        public static void ARBGetSubscriptionStatusRequest ( ARBGetSubscriptionStatusRequest request) { }
    
        public static void createCustomerProfileRequest ( createCustomerProfileRequest request) 
        {
            if (null != request)
            {
                if (0 <= (int)request.validationMode) { request.validationModeSpecified = true; }
            }
        }
    
        public static void createCustomerPaymentProfileRequest ( createCustomerPaymentProfileRequest request) 
        {
            if (null != request)
            {
                if (0 <= (int)request.validationMode) { request.validationModeSpecified = true; }
            }
        }
    
        public static void createCustomerShippingAddressRequest ( createCustomerShippingAddressRequest request) { }
    
        public static void getCustomerProfileRequest ( getCustomerProfileRequest request) { }
    
        public static void getCustomerPaymentProfileRequest ( getCustomerPaymentProfileRequest request) { }
    
        public static void getCustomerShippingAddressRequest ( getCustomerShippingAddressRequest request) { }
    
        public static void updateCustomerProfileRequest ( updateCustomerProfileRequest request) { }
    
        public static void updateCustomerPaymentProfileRequest ( updateCustomerPaymentProfileRequest request) 
        {
            if (null != request)
            {
                if (0 <= (int)request.validationMode) { request.validationModeSpecified = true; }
            }
        }
    
        public static void updateCustomerShippingAddressRequest ( updateCustomerShippingAddressRequest request) { }
    
        public static void deleteCustomerProfileRequest ( deleteCustomerProfileRequest request) { }
    
        public static void deleteCustomerPaymentProfileRequest ( deleteCustomerPaymentProfileRequest request) { }
    
        public static void deleteCustomerShippingAddressRequest ( deleteCustomerShippingAddressRequest request) { }
    
        public static void createCustomerProfileTransactionRequest ( createCustomerProfileTransactionRequest request)
        {
            if (null != request)
            {
                profileTransactionType(request.transaction);
            }
        }
    
        public static void validateCustomerPaymentProfileRequest ( validateCustomerPaymentProfileRequest request) { }
    
        public static void getCustomerProfileIdsRequest ( getCustomerProfileIdsRequest request) { }
    
        public static void updateSplitTenderGroupRequest ( updateSplitTenderGroupRequest request) { }
    
        public static void getTransactionDetailsRequest ( getTransactionDetailsRequest request) { }
    
        public static void createTransactionRequest(createTransactionRequest request)
        {
            if (null != request)
            {
                transactionRequestType(request.transactionRequest);
            }
        }
    
        public static void getBatchStatisticsRequest ( getBatchStatisticsRequest request) { }

        public static void getSettledBatchListRequest(getSettledBatchListRequest request) 
        {
            if (null != request)
            {
                if (request.includeStatistics) { request.includeStatisticsSpecified = true; }
                if (DateTime.MinValue != request.firstSettlementDate) { request.firstSettlementDateSpecified = true; }
                if (DateTime.MinValue != request.lastSettlementDate) { request.lastSettlementDateSpecified = true; }
            }
        }

        public static void getTransactionListRequest(getTransactionListRequest request) { }

        public static void getHostedProfilePageRequest(getHostedProfilePageRequest request) { }

        public static void getUnsettledTransactionListRequest(getUnsettledTransactionListRequest request) { }

        public static void mobileDeviceRegistrationRequest(mobileDeviceRegistrationRequest request)
        {
            if (null != request)
            {
                mobileDeviceType(request.mobileDevice);
            }
        }

        public static void mobileDeviceLoginRequest(mobileDeviceLoginRequest request) { }

        public static void logoutRequest(logoutRequest request) { }

        public static void sendCustomerTransactionReceiptRequest(sendCustomerTransactionReceiptRequest request) { }

        public static void ARBGetSubscriptionListRequest(ARBGetSubscriptionListRequest request) { }

        public static void authenticateTestType(authenticateTestRequest request) { }

        public static void isAliveType(isAliveRequest request) { }

        public static void isAliveType(ANetApiRequest request) { }

        public static void logoutType(logoutRequest request) { }

        public static void EnumCollection(EnumCollection enumCollectionRequest)
        {
            if (null != enumCollectionRequest)
            {
                paymentSimpleType(enumCollectionRequest.paymentSimpleType);
            }
        }

        #region custom helper
        public static void lineItemType(lineItemType[] lineItems)
        {
            if (null != lineItems)
            {
                foreach (var lineItem in lineItems)
                {
                    lineItemType(lineItem);
                }
            }
        }

        #endregion custom helper
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

 */
/*
Objects
 
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

 */