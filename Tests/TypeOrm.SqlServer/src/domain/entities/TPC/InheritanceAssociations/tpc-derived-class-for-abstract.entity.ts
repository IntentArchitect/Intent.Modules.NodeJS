import { TpcAbstractBaseClass } from './tpc-abstract-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpc_derived_class_for_abstract')
export class TpcDerivedClassForAbstract extends TpcAbstractBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}