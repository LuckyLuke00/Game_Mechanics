// Fill out your copyright notice in the Description page of Project Settings.


#include "FlashLightComponent.h"
#include "Components/LightComponent.h"

// Sets default values for this component's properties
UFlashLightComponent::UFlashLightComponent()
{
	// Set this component to be initialized when the game starts, and to be ticked every frame.  You can turn these features
	// off to improve performance if you don't need them.
	PrimaryComponentTick.bCanEverTick = true;
}


// Called when the game starts
void UFlashLightComponent::BeginPlay()
{
	Super::BeginPlay();

	// ...

}


// Called every frame
void UFlashLightComponent::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);
}

void UFlashLightComponent::Initialize(class AActor* Player)
{
	// Set m_Player;
	m_Player = Player;

	// Get the lightcomponent from the player
	m_Light = m_Player->FindComponentByClass<ULightComponent>();
}

void UFlashLightComponent::ToggleFlashLight()
{
	// Check if the current battery level is grater than min battery level
	if (CurrentBatteryLevel <= MinBatteryLevel)
	{
		return;
	}
	
	// Check if the light is on
	if (m_Light->IsVisible())
	{
		// Print debug message
		UE_LOG(LogTemp, Warning, TEXT("Flashlight is off"));

		// Turn off the light
		m_Light->SetVisibility(false);

		// Clear the timer
		GetWorld()->GetTimerManager().ClearTimer(m_TimerHandle);
		return;
	}

	SetIntensity();

	// Turn on the light
	m_Light->SetVisibility(true);
	// Print debug message
	UE_LOG(LogTemp, Warning, TEXT("Flashlight is on"));

	// Set a timer
	GetWorld()->GetTimerManager().SetTimer(m_TimerHandle, this, &UFlashLightComponent::DecreaseBattery, DepletionSpeed, true);
}

void UFlashLightComponent::IncreaseBattery(float Amount)
{
	CurrentBatteryLevel = FMath::Clamp(CurrentBatteryLevel + Amount, MinBatteryLevel, MaxBatteryLevel);
}

void UFlashLightComponent::DecreaseBattery()
{
	// Clamp
	CurrentBatteryLevel = FMath::Clamp(CurrentBatteryLevel - DepletionAmount, MinBatteryLevel, MaxBatteryLevel);

	SetIntensity();

	if (CurrentBatteryLevel > .0f)
		return;

	// Turn off the light
	m_Light->SetVisibility(false);

	// Clear the timer
	GetWorld()->GetTimerManager().ClearTimer(m_TimerHandle);
}

void UFlashLightComponent::SetIntensity()
{
	const float intensityScale{ FMath::GetMappedRangeValueClamped(FVector2D(MinBatteryLevel, FadeAtPercent), FVector2D(.0f, MaxIntensity), CurrentBatteryLevel) };

	m_Light->SetIESBrightnessScale(intensityScale);
}

