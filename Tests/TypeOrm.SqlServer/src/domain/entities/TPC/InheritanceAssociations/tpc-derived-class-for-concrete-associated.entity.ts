import { TpcDerivedClassForConcrete } from './tpc-derived-class-for-concrete.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpc_derived_class_for_concrete_associated')
export class TpcDerivedClassForConcreteAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForConcreteId: string;

  @ManyToOne(() => TpcDerivedClassForConcrete, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForConcrete: TpcDerivedClassForConcrete;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}